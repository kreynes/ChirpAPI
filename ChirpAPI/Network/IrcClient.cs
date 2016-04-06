using System;
using System.IO;
using System.Net.Sockets;
using System.Net.Security;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Timers;

namespace CSIRC
{
    public interface IrcEndpoint
    {
        Task Send(string rawMessage);
    }

    public class IrcClient : IDisposable, IrcEndpoint
    {
        internal Stopwatch pingStopwatch;
        internal DateTime lastPongTimestamp;

        Socket clientSocket;
        NetworkStream networkStream;
        StreamReader streamReader;
        StreamWriter streamWriter;
        BlockingCollection<string> sendCollection;
        int reconnectCounter;
        bool isHandlingSending;
        bool isHandlingReceive;
        bool disposedValue;

        public event EventHandler<EventArgs> OnConnected;
        public event EventHandler<EventArgs> OnTryReconnect;
        public event EventHandler<EventArgs> OnReconnectFailed;
        public event EventHandler<IrcMessageEventArgs> OnMessageReceived;
        public event EventHandler<IrcRawMessageEventArgs> OnMessageSent;

        public IrcConnectionSettings Settings { get; }
        public IrcMessageFactory MessageFactory { get; }
        public bool Connected { get; private set; }


        public IrcClient(IrcConnectionSettings connectionSettings)
        {
            if (connectionSettings == null)
                throw new ArgumentNullException(nameof(connectionSettings));

            Settings = connectionSettings;
            sendCollection = new BlockingCollection<string>();
            MessageFactory = new IrcMessageFactory();
            MessageFactory.LoadHandlers();
        }

        public async Task ConnectAsync()
        {
            if (clientSocket != null && Connected)
                throw new InvalidOperationException("Socket already connected.");

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            await Task.Factory.FromAsync(
                (cb, obj) => clientSocket.BeginConnect(Settings.Hostname, Settings.Port, cb, null),
                iar => clientSocket.EndConnect(iar), null);
            Connected = true;
            OnConnected.Invoke(this, EventArgs.Empty);
            networkStream = new NetworkStream(clientSocket);

            if (Settings.UseSSL)
            {
                SslStream sslStream = new SslStream(networkStream, false, (
                    (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
                {
                    if (sslPolicyErrors.HasFlag(SslPolicyErrors.None) ||
                            sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateNameMismatch) ||
                        Settings.IgnoreInvalidSslCertificate)
                        return true;
                    return false;
                }
                ));

                sslStream.AuthenticateAsClient(Settings.Hostname, new X509CertificateCollection(),
                                               SslProtocols.Default |
                                               SslProtocols.Tls12, true);

                streamReader = new StreamReader(sslStream);
                streamWriter = new StreamWriter(sslStream) { AutoFlush = true };
            }
            else
            {
                streamReader = new StreamReader(networkStream);
                streamWriter = new StreamWriter(networkStream) { AutoFlush = true };
            }
            if (Connected)
            {
                isHandlingReceive = true;
                isHandlingSending = true;
                Connected = true;

                await Task.WhenAll(Task.Run(BeginReceiveAsync), Task.Run(SendCollectionConsumer), Task.Run(SendPing));
            }


        }

        public Task Send(string rawMessage)
        {
            if (!Connected)
                throw new InvalidOperationException("Client not connected.");
            if (String.IsNullOrWhiteSpace(rawMessage))
                throw new ArgumentNullException(nameof(rawMessage));

            sendCollection.Add(rawMessage);
            return Task.CompletedTask;
        }

        public Task Send(string rawMessage, params object[] format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));
            Send(String.Format(rawMessage, format));
            return Task.CompletedTask;
        }

        public Task Send(IrcMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            Send(message.ToString());
            return Task.CompletedTask;
        }

        public async Task DisconnectAsync()
        {
            if (!Connected)
                throw new InvalidOperationException("Client already disconnected");
            await Task.Factory.FromAsync(clientSocket.BeginDisconnect, clientSocket.EndDisconnect, false, null);
            Connected = false;
        }

        private async Task AutoReconnectAsync()
        {
            if (Settings.AutoReconnect)
            {
                isHandlingSending = false;
                isHandlingReceive = false;
                sendCollection = new BlockingCollection<string>();
                Timer reconnectTimer = new Timer(Settings.RetryInterval);
                await DisconnectAsync();
                while (reconnectCounter < Settings.RetryAttempts || !Connected)
                {

                    reconnectTimer.Start();
                    reconnectTimer.Elapsed += async (object sender, System.Timers.ElapsedEventArgs e) =>
                    {
                        OnTryReconnect.Invoke(this, EventArgs.Empty);
                        await ConnectAsync();
                        reconnectCounter++;
                    };
                }
                if (reconnectCounter >= Settings.RetryAttempts && !Connected)
                {
                    reconnectTimer.Stop();
                    OnReconnectFailed.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private async Task SendPing()
        {
            while (true)
            {
                await Send($"PING {new Random().Next(0, 100).ToString()}");
                await Task.Delay(10000);
                TimeSpan pingSpan = DateTime.Now - lastPongTimestamp;

                if (Math.Ceiling(pingSpan.TotalSeconds) >= 20)
                {
                    await AutoReconnectAsync();
                    break;
                }
            }
        }

        private Task SendCollectionConsumer()
        {
            while (isHandlingSending)
            {
                foreach (string message in sendCollection.GetConsumingEnumerable())
                {
                    try
                    {
                        streamWriter.WriteLine(message);
                        OnMessageSent.Invoke(this, new IrcRawMessageEventArgs(this, message));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return Task.CompletedTask;
        }

        private async Task BeginReceiveAsync()
        {
            while (isHandlingReceive)
            {
                string rawMessage = await streamReader.ReadLineAsync();
                IrcMessage message = IrcMessage.Parse(rawMessage);

                if (rawMessage != null)
                {
                    OnMessageReceived.Invoke(this, new IrcMessageEventArgs(this, message));
                    MessageFactory.Execute(message.Command, this, message);
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (clientSocket.Connected)
                        clientSocket.Disconnect(true);
                    networkStream.Close();
                    streamReader.Dispose();
                    streamWriter.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // GC.SuppressFinalize(this);
        }

    }
}

