using System.Collections.Generic;
using System.Linq;

namespace ChirpLib.IrcClient
{
    public class IrcMessage
    {
        /// <summary>
        /// Gets the tags (IRCv3).
        /// </summary>
        /// <value>Tags.</value>
        public Dictionary<string, string> Tags { get; }

        /// <summary>
        /// Gets the prefix.
        /// </summary>
        /// <value>The prefix.</value>
        public string Prefix { get; }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        public string Command { get; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public string[] Parameters { get; }

        /// <summary>
        /// Gets the trail.
        /// </summary>
        /// <value>The trail.</value>
        public string Trail { get; }

        public string Nickname { get; }

        public string Username { get; }

        public string Hostmask { get; }

        public string Server { get; }

        public bool IsIRCv3Message { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IrcMessage"/> class.
        /// </summary>
        /// <param name="prefix">Prefix.</param>
        /// <param name="command">Command.</param>
        /// <param name="parameters">Parameters.</param>
        /// <param name="trail">Trail.</param>
        public IrcMessage(string prefix, string command, string[] parameters, string trail)
        {
            IsIRCv3Message = false;
            Prefix = prefix;
            Command = command;
            Parameters = parameters;
            Trail = trail;
            if (!string.IsNullOrEmpty(prefix) && prefix.Contains('!') && prefix.Contains('@'))
            {
                string[] parsedPrefix = prefix.Split('!', '@');
                Nickname = parsedPrefix[0];
                Username = parsedPrefix[1];
                Hostmask = parsedPrefix[2];
            }
            else
            {
                Server = prefix;
            }
        }

        public IrcMessage(Dictionary<string, string> tags, string prefix, string command, string[] parameters, string trail)
        {
            IsIRCv3Message = true;
            Tags = tags;
            Prefix = prefix;
            Command = command;
            Parameters = parameters;
            Trail = trail;
            if (!string.IsNullOrEmpty(prefix) && prefix.Contains('!') && prefix.Contains('@'))
            {
                string[] parsedPrefix = prefix.Split('!', '@');
                Nickname = parsedPrefix[0];
                Username = parsedPrefix[1];
                Hostmask = parsedPrefix[2];
            }
            else
            {
                Server = prefix;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="IrcMessage"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="IrcMessage"/>.</returns>
        public override string ToString() {
            return IsIRCv3Message ?
                $"{string.Join(";", Tags.Select(x => $"{x.Key}={x.Value}"))} {Prefix} {Command} {string.Join(" ", Parameters, Trail)}" :
                $"{Prefix} {Command} {string.Join(" ", Parameters, Trail)}";
        }
    }
}


