using System;
using System.Linq;

namespace ChirpLib
{
    public class IrcMessage
    {
        private string prefix;
        private string command;
        private string[] parameters;
        private string trail;
        private string senderNick;
        private string senderUser;
        private string senderHost;
        private string serverName;

        /// <summary>
        /// Gets the prefix.
        /// </summary>
        /// <value>The prefix.</value>
        public string Prefix
        {
            get { return prefix; }
        }
        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        public string Command
        {
            get { return command; }
        }
        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public string[] Parameters
        {
            get { return parameters; }
        }
        /// <summary>
        /// Gets the trail.
        /// </summary>
        /// <value>The trail.</value>
        public string Trail
        {
            get { return trail; }
        }

        public string Nickname
        {
            get { return senderNick; }
        }

        public string Username
        {
            get { return senderUser; }
        }

        public string Hostmask
        {
            get { return senderHost; }
        }
        public string Server
        {
            get { return serverName; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ChirpLib.IrcMessage"/> class.
        /// </summary>
        /// <param name="prefix">Prefix.</param>
        /// <param name="command">Command.</param>
        /// <param name="parameters">Parameters.</param>
        /// <param name="trail">Trail.</param>
        public IrcMessage(string prefix, string command, string[] parameters, string trail)
        {
            this.prefix = prefix;
            this.command = command;
            this.parameters = parameters;
            this.trail = trail;
            if (!string.IsNullOrEmpty(prefix) && prefix.Contains('!') && prefix.Contains('@'))
            {
                string[] parsedPrefix = prefix.Split(new char[] { '!', '@' });
                senderNick = parsedPrefix[0];
                senderUser = parsedPrefix[1];
                senderHost = parsedPrefix[2];
            }
            else
            {
                serverName = prefix;
            }
        }
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="ChirpLib.IrcMessage"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="ChirpLib.IrcMessage"/>.</returns>
        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", Prefix, Command, string.Join(" ", Parameters, Trail));
        }
    }
}


