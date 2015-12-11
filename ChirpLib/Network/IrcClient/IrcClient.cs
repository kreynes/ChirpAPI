using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net.Security;
using System.Threading.Tasks;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace ChirpLib
{
    /// <summary>
    /// Provides client connection
    /// for IRC network services
    /// </summary>
    public class IrcClient : IDisposable
    {
        private TcpClient tcpClient;
        private StreamReader clientReader;
        private StreamWriter clientWriter;
        private SslStream sslStream;
        private string _hostname;
        private string[] _hostnames;
        private int _port = 6667;
        private bool _useSsl = false;
        private bool _ignoreInvalidSsl = false;
        private bool _isDisposed;
        private bool _isHandlingReceive = false;

        public event EventHandler<EventArgs> OnConnecting;
        public event EventHandler<EventArgs> OnConnected;
        public event EventHandler<EventArgs> OnDisconnecting;
        public event EventHandler<EventArgs> OnDisconnected;
        public event EventHandler<IrcRawMessageEventArgs> OnRawMessageReceived;
        public event EventHandler<IrcRawMessageEventArgs> OnRawMessageSent;

        /// <summary>
        /// Gets the remote IRC hostname.
        /// </summary>
        /// <value>
        /// Hostname.
        /// </value>
        public string Hostname
        {
            get { return _hostname; }
        }
        public string[] Hostnames
        {
            get { return _hostnames; }
        }
        /// <summary>
        /// Gets the remote IRC port.
        /// </summary>
        public int Port
        {
            get { return _port; }
        }
        /// <summary>
        /// Gets or sets if SSL
        /// connection enabled.
        /// false by default
        /// </summary>
        public bool EnableSsl
        {
            get { return _useSsl; }
            set { _useSsl = value; }
        }
        /// <summary>
        /// Gets or sets if invalid SSL 
        /// certificate is ignored.
        /// false by default.
        /// </summary>
        public bool IgnoreInvalidSslCertificate
        {
            get { return _ignoreInvalidSsl; }
            set { _ignoreInvalidSsl = value; }
        }
        /// <summary>
        /// Creates a new instance of <c>IrcClient</c>.</summary>
        /// <param name="hostname">string representation of the remote hostname.</param>
        /// <param name="port">port of the remote hostname</param> 
        public IrcClient(string hostname, int port)
        {
            if (string.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException("hostname");
            _hostname = hostname;
            _port = port;
        }
        /// <summary>
        /// Connects to the 
        /// remote hostname.</summary>
        public async Task Connect()
        {
            tcpClient = new TcpClient();
            await Task.Factory.FromAsync(
                (cb, obj) => tcpClient.BeginConnect(_hostname, _port, cb, null),
                iar => tcpClient.EndConnect(iar), null);
            OnConnected?.ParallelInvoke(this, EventArgs.Empty);
            if (_useSsl)
            {
                sslStream = new SslStream(tcpClient.GetStream(), false, ((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
                    {
                        if (sslPolicyErrors.HasFlag(SslPolicyErrors.None) | sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateNameMismatch))
                        {
                            return true;
                        }
                        if (_ignoreInvalidSsl)
                            return true;
                        return false;
                    }));
                sslStream.AuthenticateAsClient(_hostname, new X509CertificateCollection()
                    , SslProtocols.Default | SslProtocols.Tls12, true);
            }
            if (_useSsl)
            {
                clientReader = new StreamReader(sslStream);
                clientWriter = new StreamWriter(sslStream) { AutoFlush = true };
            }
            else
            {
                clientReader = new StreamReader(tcpClient.GetStream());
                clientWriter = new StreamWriter(tcpClient.GetStream()) { AutoFlush = true };
            }
            if (tcpClient.Connected)
            {
                await BeginReceive();
            }
        }
        /// <summary>
        /// Starts receiving new messages after
        /// successfully connecting.</summary>    
        private async Task BeginReceive()
        {
            if (tcpClient.Connected)
            {
                _isHandlingReceive = true;
            }
            while (_isHandlingReceive)
            {
                string rawMessage = await clientReader.ReadLineAsync();
                if (rawMessage != null)
                {
                    OnRawMessage(rawMessage);
                }
            }
        }
        /// <summary>
        /// Sends a raw message asynchronously.</summary>
        /// <param name="rawMessage">Raw message to send.
        /// CL/RF appended automatically.</param>
        public async Task SendAsync(string rawMessage)
        {
            if (!tcpClient.Connected)
                throw new InvalidOperationException();
            if (string.IsNullOrWhiteSpace(rawMessage))
                throw new ArgumentNullException("message");
            
            await clientWriter.WriteLineAsync(string.Format("{0}\r\n", rawMessage));
            OnRawMessageSent?.ParallelInvoke(this, new IrcRawMessageEventArgs(this, rawMessage));
        }
        /// <summary>
        /// Sends a raw message asynchronously.</summary>
        /// <param name="rawMessage">Formatted raw message.
        /// CL/RF appended automatically.</param>
        /// <param name="args">string representation
        /// of objects to replace in rawMessage</param>
        public async Task SendAsync(string rawMessage, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(args.ToString()))
                throw new ArgumentNullException("format");
            await SendAsync(string.Format(rawMessage, args));
        }
        /// <summary>
        /// Disconnects IrcClient
        /// by closing the socket.</summary>
        public void Disconnect()
        {
            if (tcpClient.Connected)
            {
                Disconnect(false);
            }

        }
        /// <summary>
        /// Disconnects IrcClient
        /// by closing the socket
        /// for further reuse.</summary>
        public void Disconnect(bool reuse)
        {
            OnDisconnecting?.ParallelInvoke(this, EventArgs.Empty);
            if (tcpClient.Connected)
            {
                tcpClient.Client.Disconnect(reuse);
            }
            OnDisconnected?.ParallelInvoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Releases all resources used by
        /// <c>IrcClient</c>.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Release all resources used by
        /// <c>IrcClient</c>.</summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (disposing)
            {
                tcpClient.Close();
                sslStream.Dispose();
                clientReader.Dispose();
                clientWriter.Dispose();
            }
            _isDisposed = true;
        }

        public void OnRawMessage(string rawMessage)
        {
            OnRawMessageReceived?.ParallelInvoke(this, new IrcRawMessageEventArgs(this, rawMessage));
        }
    }
}

