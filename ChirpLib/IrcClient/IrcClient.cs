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
        private string hostname;
        private int port = 6667;
        private bool useSsl = false;
        private bool ignoreInvalidSsl = false;
        private bool isDisposed;
        private bool isHandlingReceive = false;

        public event EventHandler<EventArgs> OnConnecting;
        public event EventHandler<EventArgs> OnConnected;
        public event EventHandler<EventArgs> OnDisconnecting;
        public event EventHandler<EventArgs> OnDisconnected;
        public event EventHandler<IrcRawMessageEventArgs> OnRawMessageReceived;
        public event EventHandler<IrcRawMessageEventArgs> OnRawMessageSent;

        public IrcClient(string hostname, int port)
        {
            this.hostname = hostname;
            this.port = port;
            IrcMessageHandler.LoadHandlers();
        }
        /// <summary>
        /// Creates a new instance of <c>IrcClient</c>.</summary>
        /// <param name="hostname">string representation of the remote hostname.</param>
        /// <param name="port">port of the remote hostname</param> 
        public IrcClient(string hostname, int port, bool useSsl, bool ignoreInvalidCertificate)
        {
            this.hostname = hostname;
            this.port = port;
            this.useSsl = useSsl;
            this.ignoreInvalidSsl = ignoreInvalidCertificate;
            IrcMessageHandler.LoadHandlers();
        }
        /// <summary>
        /// Connects to the 
        /// remote hostname.</summary>
        public async Task ConnectAsync()
        {
            OnConnecting?.ParallelInvoke(this, EventArgs.Empty);
            tcpClient = new TcpClient();

            await Task.Factory.FromAsync(
                (cb, obj) => tcpClient.BeginConnect(hostname, port, cb, null),
                iar => tcpClient.EndConnect(iar), null);
            
            if (useSsl)
            {
                SslStream sslStream = new SslStream(tcpClient.GetStream(), false, ((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
                    {
                        if (sslPolicyErrors.HasFlag(SslPolicyErrors.None) || 
                            sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateNameMismatch) ||
                            ignoreInvalidSsl) return true;
                        else return false;
                    }));
                
                sslStream.AuthenticateAsClient(hostname, new X509CertificateCollection()
                    , SslProtocols.Default | SslProtocols.Tls12, true);
                
                clientReader = new StreamReader(sslStream);
                clientWriter = new StreamWriter(sslStream) { AutoFlush = true };
            }
            else
            {
                clientReader = new StreamReader(tcpClient.GetStream());
                clientWriter = new StreamWriter(tcpClient.GetStream());
                clientWriter.AutoFlush = true;
            }

            if (tcpClient.Connected)
            {
                OnConnected?.ParallelInvoke(this, EventArgs.Empty);
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
                isHandlingReceive = true;
            }
            while (isHandlingReceive)
            {
                string rawMessage = await clientReader.ReadLineAsync();
                if (rawMessage != null)
                {
                    OnRawMessageReceived?.ParallelInvoke(this, new IrcRawMessageEventArgs(this, rawMessage));
                    IrcMessage parsedMessage = IrcParser.ParseRawMessage(rawMessage);
                    //IrcMessageHandler.Execute(parsedMessage.Command, this, parsedMessage);
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
                throw new ArgumentNullException("Message is Null or empty");
            await clientWriter.WriteLineAsync(rawMessage);
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
            if (isDisposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (disposing)
            {
                tcpClient.Close();
                clientReader.Dispose();
                clientWriter.Dispose();
            }
            isDisposed = true;
        }
    }
}

