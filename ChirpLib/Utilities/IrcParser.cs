using System;
using System.Collections.Generic;
using System.Linq;
using ChirpLib.IrcClient;

namespace ChirpLib.Utilities
{
    internal static class IrcParser
    {
        private static readonly char[] TAG_SEPERATOR = { ';' };
        private static readonly char[] TAG_VALUE_SEPERATOR = { '=' };
        /// <summary>
        /// Parses the raw message.
        /// </summary>
        /// <returns>a new instace of <see cref="IrcMessage"/></returns>
        /// <param name="rawMessage">Raw message.</param>
        public static IrcMessage ParseRawMessage(string rawMessage)
        {
            if (string.IsNullOrWhiteSpace(rawMessage))
                throw new ArgumentNullException(nameof(rawMessage));

            bool isTagPrefix = false;
            string prefix = string.Empty;
            string command = string.Empty;
            string trailing = string.Empty;
            string[] parameters = { };
            Dictionary<string, string> tags = new Dictionary<string, string>();

            if (rawMessage.StartsWith("@"))
            {
                isTagPrefix = true;
                int spaceToPrefix = rawMessage.IndexOf(' ');
                string[] unparsedTags = rawMessage.Substring(1, spaceToPrefix).Split(TAG_SEPERATOR);
                rawMessage = rawMessage.Substring(spaceToPrefix + 1);
                foreach (string tag in unparsedTags)
                {
                    if (tag.Contains(';'))
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

            if (rawMessage.StartsWith(":"))
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
                parameters = commandAndParameters.Skip(1).ToArray();
            
            return isTagPrefix ? new IrcMessage(tags, prefix, command, parameters, trailing) : new IrcMessage(prefix, command, parameters, trailing);
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
    }
}

