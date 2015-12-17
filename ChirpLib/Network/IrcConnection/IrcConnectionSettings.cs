using System;

namespace ChirpLib
{
    public class IrcConnectionSettings
    {
        private string _hostname;
        private int _port = 6667;
        private bool _useSsl = false;
        private bool _ignoreInvalidSslCertificate = true;
        private string _nickname = "Chirp";
        private string _username = "Chirp";
        private string _realname = "Chirp";

        public string Hostname
        {
            get { return _hostname; }
        }

        public int Port
        {
            get { return _port; }
        }

        public bool EnableSSL
        {
            get { return _useSsl; }
        }

        public bool IgnoreInvalidCertificate
        {
            get { return _ignoreInvalidSslCertificate; }
        }

        public string Nickname
        {
            get { return _nickname; }
            set { _nickname = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Realname
        {
            get { return _realname; }
            set { _realname = value; }
        }

        public IrcConnectionSettings(string hostname, int port)
        {
            if (string.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException("hostname");
            
            _hostname = hostname;
            _port = port;
        }
    }
}

