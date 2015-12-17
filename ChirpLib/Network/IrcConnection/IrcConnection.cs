using System;
using System.Threading.Tasks;

namespace ChirpLib
{
    public class IrcConnection
    {
        public IrcClient Client;

        private static IrcConnectionSettings connSettings;

        public IrcConnection(IrcConnectionSettings connectionSettings)
        {
            connSettings = connectionSettings;
            Client = new IrcClient(connectionSettings.Hostname, connectionSettings.Port, connectionSettings.EnableSSL, connectionSettings.IgnoreInvalidCertificate);
        }
        public async Task Connect()
        {
            await Client.Connect();
        }
        public async Task SendMessage(string rawMessage)
        {
            await Client.SendAsync(rawMessage);
        }
        public void Authenticate(string username, string realname)
        {
            Client.SendAsync(string.Format("USER {0} 8 * :{1}", username, realname));
        }
        internal void HandleMessage(string rawMessage)
        {
            IrcMessage message = IrcParser.ParseRawMessage(rawMessage);
        }
    }
}

