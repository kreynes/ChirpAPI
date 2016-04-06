using System;
using System.Collections.Generic;
using System.Linq;

namespace CSIRC
{
    public class IrcMessage
    {
        static readonly char[] TAG_SEPERATOR = { ';' };
        static readonly char[] TAG_VALUE_SEPERATOR = { '=' };

        public string Prefix { get; private set; }
        public string Command { get; private set; }
        public IList<string> Parameters { get; private set; }
        public string Trail { get; private set; }
        public IDictionary<string, string> Tags { get; private set; }

        public bool IsHostmaskPrefix
        {
            get
            {
                return !String.IsNullOrWhiteSpace(Prefix)
                              && Prefix.Contains("@")
                              && Prefix.Contains("!");
            }
        }

        public bool IsServerPrefix
        {
            get
            {
                return !String.IsNullOrEmpty(Prefix)
                              && !IsHostmaskPrefix
                              && Prefix.Contains(".");
            }
        }


        public IrcMessage(string prefix, string command, IList<string> parameters, string trail)
        {
            Prefix = prefix;
            Command = command;
            Parameters = parameters;
            Trail = trail;
        }

        public IrcMessage(Dictionary<string, string> tags, string prefix, string command, IList<string> parameters, string trail)
            : this(prefix, command, parameters, trail)
        {
            Tags = tags;
        }

        public static IrcMessage Parse(string rawMessage)
        {
            if (string.IsNullOrWhiteSpace(rawMessage))
                throw new ArgumentNullException(nameof(rawMessage));

            bool isTagPrefix = false;
            string prefix = string.Empty;
            string command;
            string trailing = string.Empty;
            IList<string> parameters = new string[] { };
            Dictionary<string, string> tags = new Dictionary<string, string>();

            if (rawMessage.StartsWith("@", StringComparison.Ordinal))
            {
                isTagPrefix = true;
                int spaceToPrefix = rawMessage.IndexOf(' ');
                string[] unparsedTags = rawMessage.Substring(1, spaceToPrefix).Split(TAG_SEPERATOR);
                rawMessage = rawMessage.Substring(spaceToPrefix + 1);
                foreach (string tag in unparsedTags)
                {
                    if (tag.Contains(";"))
                    {
                        string[] tempTag = tag.Split(TAG_VALUE_SEPERATOR);
                        tags.Add(RemoveEscape(tempTag[0]), RemoveEscape(tempTag[1]));
                    }
                    else
                    {
                        tags.Add(RemoveEscape(tag), string.Empty);
                    }
                }
            }

            int prefixEnd = -1;
            int trailingStart = rawMessage.Length;

            if (rawMessage.StartsWith(":", StringComparison.Ordinal))
            {
                prefixEnd = rawMessage.IndexOf(" ", StringComparison.Ordinal);
                prefix = rawMessage.Substring(1, prefixEnd - 1);
            }

            trailingStart = rawMessage.IndexOf(" :", StringComparison.Ordinal);
            if (trailingStart >= 0)
                trailing = rawMessage.Substring(trailingStart + 2);
            else
                trailingStart = rawMessage.Length;

            string[] commandAndParameters = rawMessage.Substring(prefixEnd + 1, trailingStart - prefixEnd - 1).Split(' ');

            command = commandAndParameters[0];
            if (commandAndParameters.Length > 1)
                parameters = commandAndParameters.Skip(1).ToList();

            if (isTagPrefix)
                return new IrcMessage(tags, prefix, command, parameters, trailing);
            else
                return new IrcMessage(prefix, command, parameters, trailing);
        }

        public Hostname GetHostmask()
        {
            if (!IsHostmaskPrefix)
                return null;
            string[] splitPrefix = Prefix.Split(new char[] { '@', ':' });
            return new Hostname(splitPrefix[0], splitPrefix[1], splitPrefix[2]);
        }

        public string GetHostname()
        {
            if (!IsServerPrefix)
                return null;
            return Prefix;
        }

        public override string ToString()
        {
            if (Tags.Count > 0)
                return string.Format("{0} {1} {2} {3} {4}", string.Join(";", Tags.Select(x => string.Format("{0}={1}", x.Key, x.Value))), Prefix, Command, string.Join(" ", Parameters.ToArray(), Trail));
            else
                return string.Format("{0} {1} {2}", Prefix, Command, string.Join(" ", Parameters.ToArray(), Trail));
        }

        private static string RemoveEscape(string tag)
        {
            return tag
                .Replace(@"\:", ";")
                .Replace(@"\s", " ")
                .Replace(@"\\", @"\")
                .Replace(@"\r", "\r")
                .Replace(@"\n", "\n");
        }

        public class Hostname
        {
            public string Nickname { get; private set; }
            public string Username { get; private set; }
            public string Hostmask { get; private set; }

            public Hostname(string nickname, string username, string hostmask)
            {
                Nickname = nickname;
                Username = username;
                Hostmask = hostmask;
            }
        }
    }
}

