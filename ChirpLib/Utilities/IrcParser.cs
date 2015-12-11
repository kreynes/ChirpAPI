using System;
using System.Linq;

namespace ChirpLib
{
    internal static class IrcParser
    {
        public static IrcMessage ParseRawMessage(string rawMessage)
        {
            if (string.IsNullOrWhiteSpace(rawMessage))
                throw new ArgumentNullException("rawMessage");
            
            string prefix;
            string command;
            string trailing;

            string[] parameters = new string[] { };



            prefix = command = trailing = String.Empty;

            int prefixEnd = -1;
            int trailingStart = rawMessage.Length;

            if (rawMessage.StartsWith(":"))
            {
                prefixEnd = rawMessage.IndexOf(" ");
                prefix = rawMessage.Substring(1, prefixEnd - 1);
            }

            trailingStart = rawMessage.IndexOf(" :");
            if (trailingStart >= 0)
                trailing = rawMessage.Substring(trailingStart + 2);
            else
                trailingStart = rawMessage.Length;

            string[] commandAndParameters = rawMessage.Substring(prefixEnd + 1, trailingStart - prefixEnd - 1).Split(' ');

            command = commandAndParameters[0];
            if (commandAndParameters.Length > 1)
                parameters = commandAndParameters.Skip(1).ToArray();



            return new IrcMessage(prefix, command, parameters, trailing);
        }
    }
}

