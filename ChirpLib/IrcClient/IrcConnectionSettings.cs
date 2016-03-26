using System;
using System.Text;

namespace ChirpLib.IrcClient
{
    public class IrcConnectionSettings
    {
        private string nickname = "Chirp";
        private string username = "Chirp";
        private string realname = "Chirp";
        private string password;

        public IrcConnectionSettings(string hostname, int port)
        {
            if (String.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException(nameof(hostname), "Null, empty or whitespace.");
            if (port == 0)
                throw new ArgumentNullException(nameof(port), "No port specified.");
            Hostname = hostname;
            Port = port;
        }

        public IrcConnectionSettings(string hostname, int port, string[] channels)
        {
            if (String.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException(nameof(hostname), "Null, empty or whitespace.");
            if (port == 0)
                throw new ArgumentNullException(nameof(port), "No port specified.");
            Hostname = hostname;
            Port = port;
            Channels = channels;
        }

        public IrcConnectionSettings(string hostname, int port, string password)
        {
            if (String.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException(nameof(hostname), "Null, empty or whitespace.");
            if (String.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "Null, empty or whitespace.");
            if (port == 0)
                throw new ArgumentNullException(nameof(port), "No port specified.");
            Hostname = hostname;
            Port = port;
            this.password = password;
        }

        public IrcConnectionSettings(string hostname, int port, string[] channels, string password)
        {
            if (String.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException(nameof(hostname), "Null, empty or whitespace.");
            if (port == 0)
                throw new ArgumentNullException(nameof(port), "No port specified.");
            Hostname = hostname;
            Port = port;
            Channels = channels;
            this.password = password;
        }
        public string Hostname { get; }

        public int Port { get; }

        public string Nickname
        {
            get { return nickname; }
            set {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(value), "String is null, empty or consists of White space.");
                nickname = value;
            }
        }

        public string Username
        {
            get { return username; }
            set {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(value), "String is null, empty or consists of White space.");
                username = value;
            }
        }

        public string Realname
        {
            get { return realname; }
            set {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(value), "String is null, empty or consists of White space.");
                realname = value;
            }
        }

        public string Password
        {
            get { return password; }
            set {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(value), "String is null, empty or consists of White space.");
                password = value;
            }
        }
        public string[] Channels { get; }

        public bool UseSsl { get; set; }

        public bool IgnoreInvalidSslCertificate { get; set; }

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public bool AutoLogin { get; set; } = true;

        public bool AutoJoin { get; set; } = true;
    }
}

