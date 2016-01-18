using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net.Security;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace ChirpLib
{
    /// <summary>
    /// Irc client.
    /// </summary>
    public class IrcClient : IDisposable
    {
        private TcpClient tcpClient;
        private StreamReader clientReader;
        private StreamWriter clientWriter;
        private bool isDisposed;
        private bool isHandlingReceive = false;
        private static BlockingCollection<string> writerCollection = new BlockingCollection<string>();
        private IrcConnectionSettings settings;
        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);

        #region EventHandlers
        public event EventHandler<EventArgs> OnConnecting;
        public event EventHandler<EventArgs> OnConnected;
        public event EventHandler<EventArgs> OnDisconnecting;
        public event EventHandler<EventArgs> OnDisconnected;
        public event EventHandler<IrcRawMessageEventArgs> OnRawMessageReceived;
        public event EventHandler<IrcRawMessageSentEventArgs> OnRawMessageSent;
        public event EventHandler<IrcRawMessageEventArgs> OnPingMessageReceived;
        #endregion


        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public IrcConnectionSettings Settings
        {
            get { return settings; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChirpLib.IrcClient"/> class.
        /// </summary>
        /// <param name="settings">Settings.</param>
        public IrcClient(IrcConnectionSettings settings)
        {
            this.settings = settings;
            IrcMessageHandler.LoadHandlers();
        }

        /// <summary>
        /// Asynchronously connects to the specified hostname.
        /// </summary>
        public async Task ConnectAsync()
        {
            OnConnecting?.ParallelInvoke(this, EventArgs.Empty);
            tcpClient = new TcpClient();

            await Task.Factory.FromAsync(
                (cb, obj) => tcpClient.BeginConnect(settings.Hostname, settings.Port, cb, null),
                iar => tcpClient.EndConnect(iar), null);
            
            if (settings.UseSsl)
            {
                SslStream sslStream = new SslStream(tcpClient.GetStream(), false, (
                    (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
                    {
                        if (sslPolicyErrors.HasFlag(SslPolicyErrors.None) ||
                            sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateNameMismatch) ||
                            settings.IgnoreInvalidSslCertificate)
                            return true;
                        else
                            return false;
                    }
                ));
                
                sslStream.AuthenticateAsClient(settings.Hostname, new X509CertificateCollection(),
                    SslProtocols.Default | 
                    SslProtocols.Tls12, true);
                
                clientReader = new StreamReader(sslStream);
                clientWriter = new StreamWriter(sslStream) { AutoFlush = true };
            }
            else
            {
                if (settings.Encoding == Encoding.UTF8)
                {
                    clientReader = new StreamReader(tcpClient.GetStream());
                    clientWriter = new StreamWriter(tcpClient.GetStream());
                }
                else
                {
                    clientReader = new StreamReader(tcpClient.GetStream(), settings.Encoding);
                    clientWriter = new StreamWriter(tcpClient.GetStream(), settings.Encoding);
                }
                clientWriter.AutoFlush = true;
            }

            if (tcpClient.Connected)
            {
                //Task.Factory.StartNew(MessageConsumer);
                OnConnected?.ParallelInvoke(this, EventArgs.Empty);

                await BeginReceive();
            }
        }

        /// <summary>
        /// Begins the receive.
        /// </summary>
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
                    OnRawMessageReceived?.ParallelInvoke(this, new IrcRawMessageEventArgs(this, IrcParser.ParseRawMessage(rawMessage)));
                    IrcMessage parsedMessage = IrcParser.ParseRawMessage(rawMessage);
                    await Task.Run(() => IrcMessageHandler.Execute(parsedMessage.Command, this, parsedMessage));
                }
            }
        }

        public void Send(string rawMessage)
        {
            if (!tcpClient.Connected)
                throw new InvalidOperationException("Could not send message. No open connection");
            if (string.IsNullOrWhiteSpace(rawMessage))
                throw new ArgumentNullException("rawMessage", "String is null, empty or consists of White space.");
            //await clientWriter.WriteLineAsync(rawMessage).ConfigureAwait(false);
            writerCollection.Add(rawMessage);
            OnRawMessageSent?.ParallelInvoke(this, new IrcRawMessageSentEventArgs(this, rawMessage));
        }

        public void Send(string rawMessage, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(rawMessage))
                throw new ArgumentNullException("rawMessage", "String is null, empty or consists of White space."); 
            if (string.IsNullOrWhiteSpace(args.ToString()))
                throw new ArgumentNullException("args", "Invalid arguments.");
            Send(string.Format(rawMessage, args));
        }

        /// <summary>
        /// Writer Thread that reads from BlockingCollection
        /// and writes it to the streamwriter(sync).
        /// </summary>
        private void MessageConsumer()
        {
            while (true)
            {
                foreach (string message in writerCollection.GetConsumingEnumerable())
                {
                    clientWriter.WriteLine(message);
                }
            }
        }

        /// <summary>
        /// Disconnect this instace of <see cref="ChirpLib.IrcClient"/>.
        /// </summary>
        /// <param name="reuse">If set to <c>true</c> reuse the socket.</param>
        public void Disconnect()
        {
            OnDisconnecting?.ParallelInvoke(this, EventArgs.Empty);
            if (!tcpClient.Connected)
                throw new InvalidOperationException("The underlying socket is not connected");
            else
                tcpClient.Close();
            OnDisconnected?.ParallelInvoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Releases all resource used by the <see cref="ChirpLib.IrcClient"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="ChirpLib.IrcClient"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="ChirpLib.IrcClient"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the <see cref="ChirpLib.IrcClient"/> so the
        /// garbage collector can reclaim the memory that the <see cref="ChirpLib.IrcClient"/> was occupying.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the specified disposing.
        /// </summary>
        /// <param name="disposing">If set to <c>true</c> disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
                throw new ObjectDisposedException(this.GetType().FullName, "Instace is already disposed");
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

