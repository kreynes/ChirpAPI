using System;
using System.Collections.Generic;

namespace ChirpLib
{
    public class IrcMessageHandler
    {
        
        public static Dictionary<string, Action<IrcClient, IrcMessage>> MessageFactory = new Dictionary<string, Action<IrcClient, IrcMessage>>();

        /// <summary>
        /// Loads the handlers.
        /// </summary>
        public static void LoadHandlers()
        {
            MessageFactory.Add("PING", OnPingMessage);
            MessageFactory.Add("PRIVMSG", OnPrivateMessage);
            MessageFactory.Add("001", OnReplyWelcome);
            MessageFactory.Add("002", OnReplyYourHost);
            MessageFactory.Add("003", OnReplyCreated);
            MessageFactory.Add("004", OnReplyMyInfo);
            MessageFactory.Add("005", OnReplyMap);
            MessageFactory.Add("007", OnReplyMapEnd);
            MessageFactory.Add("375", OnReplyMotdStart);
            MessageFactory.Add("372", OnReplyMotd);
            MessageFactory.Add("377", OnReplyMotdAlt);
            MessageFactory.Add("378", OnReplyMotdAlt2);
        }
        /// <summary>
        /// Execute the specified handler.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="client">Client.</param>
        /// <param name="message">Message.</param>
        public static void Execute(string key, IrcClient client, IrcMessage message)
        {
            Action<IrcClient, IrcMessage> action;
            if (MessageFactory.TryGetValue(key, out action))
            {
                action.Invoke(client, message);
            }
            else
                throw new NotImplementedException("Unknown command");
        }
        #region Initial
        public static void OnPingMessage(IrcClient client, IrcMessage message)
        {
            if (!String.IsNullOrWhiteSpace(message.Trail))
            {
                client.Send(string.Format("PONG {0}", message.Trail));
            }
            //client.OnPingMessageReceived?.ParallelInvoke(client, new IrcRawMessageEventArgs(client, message));
        }
        private static void OnReplyWelcome(IrcClient client, IrcMessage message)
        {
            
        }
        private static void OnReplyYourHost(IrcClient client, IrcMessage message)
        {
        }
        private static void OnReplyCreated(IrcClient client, IrcMessage message)
        {
        }
        private static void OnReplyMyInfo(IrcClient client, IrcMessage message)
        {
            
        }
        private static void OnReplyMap(IrcClient client, IrcMessage message)
        {
        }
        private static void OnReplyMapEnd(IrcClient client, IrcMessage message)
        {
        }
        private static void OnReplyMotdStart(IrcClient client, IrcMessage message)
        {
        }
        private static void OnReplyMotd(IrcClient client, IrcMessage message)
        {
        }
        private static void OnReplyMotdAlt(IrcClient client, IrcMessage message)
        {
        }
        private static void OnReplyMotdAlt2(IrcClient client, IrcMessage message)
        {
        }
        private static void OnReplyMotdEnd(IrcClient client, IrcMessage message)
        {
        }
        private static void OnReplyUserModeIs(IrcClient client, IrcMessage message)
        {
        }
        #endregion
        private static void OnPrivateMessage(IrcClient client, IrcMessage message)
        {
        }
    }
}

