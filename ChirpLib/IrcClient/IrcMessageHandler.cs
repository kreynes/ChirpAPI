using System;
using System.Collections.Generic;

namespace ChirpLib
{
    public class IrcMessageHandler
    {
        public static Dictionary<string, Action<IrcClient, IrcMessage>> MessageFactory = new Dictionary<string, Action<IrcClient, IrcMessage>>();

        public static void LoadHandlers()
        {
            MessageFactory.Add("PRIVMSG", OnPrivateMessage);
        }

        public static void Execute(string key, IrcClient client, IrcMessage message)
        {
            if (MessageFactory.ContainsKey(key))
            {
                MessageFactory[key].Invoke(client, message);
            }
            else
                throw new NotImplementedException("Unexpected command.");
        }
        private static void OnPrivateMessage(IrcClient client, IrcMessage message)
        {
            client.SendAsync("PRIVMSG #vana MessageFactory works!");
        }
    }
}

