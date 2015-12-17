using System;

namespace ChirpLib
{
    public class ConnectionSettings
    {
        private string _hostname;
        private int _port;
        private bool _useSsl;
        private bool _ignoreInvalidSslCertificate;


        public ConnectionSettings()
        {
        }
    }
}

