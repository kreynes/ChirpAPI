using System;
using System.Text;

namespace ChirpLib
{
    public class IrcConnectionSettings
    {
        private string hostname;
        private int port;
        private string nickname = "Chirp";
        private string username = "Chirp";
        private string realname = "Chirp";
        private Encoding encoding = Encoding.UTF8;
        private string password;
        private string[] channels = new string[] {};
        private bool useSsl = false;
        private bool ignoreInvalidSslCertificate = false;

        public IrcConnectionSettings(string hostname, int port)
        {
            if (String.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException("hostname", "Null, empty or whitespace.");
            if (port == 0)
                throw new ArgumentNullException("port", "No port specified.");
            this.hostname = hostname;
            this.port = port;
        }

        public string Hostname
        {
            get { return hostname; }
        }

        public int Port
        {
            get { return port; }
        }

        public string Nickname
        {
            get { return nickname; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value", "String is null, empty or consists of White space.");
                else
                    nickname = value;
            }
        }

        public string Username
        {
            get { return username; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value", "String is null, empty or consists of White space.");
                else
                    username = value;
            }
        }

        public string Realname
        {
            get { return realname; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value", "String is null, empty or consists of White space.");
                else
                    realname = value;
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value", "String is null, empty or consists of White space.");
                else
                    password = value;
            }
        }

        public string[] Channels
        {
            get { return channels; }
            set
            {
                if (value.IsNullOrEmpty())
                    throw new ArgumentNullException("value", "Channel array is null, or empty");
            }
        }

        public bool UseSsl
        {
            get { return useSsl; }
            set { useSsl = value; }
        }

        public bool IgnoreInvalidSslCertificate
        {
            get { return ignoreInvalidSslCertificate; }
            set { ignoreInvalidSslCertificate = value; }
        }

        public Encoding Encoding
        {
            get { return encoding; }
            set { encoding = value; }
        }
    }
}

