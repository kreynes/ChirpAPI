using System;
namespace ChirpAPI
{
    public class IrcConnectionSettings
    {

        public string Hostname { get; set; }
        public int Port { get; set; } = 6667;
        public string Password { get; set; }
        public bool UseSSL { get; set; }
        public bool IgnoreInvalidSslCertificate { get; set; }
        public string Nickname { get; set; }
        public string Username { get; set; }
        public string Realname { get; set; }
        public bool AutoReconnect { get; set; }
        public int RetryAttempts { get; set; }
        public int RetryInterval { get; set; } = 10;

        public IrcConnectionSettings(string hostname)
        {
            if (String.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException(nameof(hostname));
            Hostname = hostname;
        }

        public IrcConnectionSettings(string hostname, int port)
            : this(hostname)
        {
            if (port == 0 || port > 65535)
                throw new ArgumentException(nameof(port));
            Port = port;
        }

        public IrcConnectionSettings(string hostname, int port, bool useSsl, bool ignoreInvalidSslCertificate)
            : this(hostname, port)
        {
            UseSSL = useSsl;
            IgnoreInvalidSslCertificate = ignoreInvalidSslCertificate;
        }
    }
}

