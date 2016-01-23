using System;
using System.Collections.Generic;

namespace ChirpLib
{
    public class IrcMessageHandler
    {
        public event EventHandler<IrcRawMessageEventArgs> OnPingMessageReceived;
        public static Dictionary<string, Action<IrcClient, IrcMessage>> MessageFactory = new Dictionary<string, Action<IrcClient, IrcMessage>>();

        /// <summary>
        /// Loads the handlers.
        /// </summary>
        internal void LoadHandlers()
        {
            MessageFactory.Add("PING", OnPingMessage);
        }
        /// <summary>
        /// Execute the specified handler.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="client">Client.</param>
        /// <param name="message">Message.</param>
        internal void Execute(string key, IrcClient client, IrcMessage message)
        {
            Action<IrcClient, IrcMessage> action;
            if (MessageFactory.TryGetValue(key, out action))
            {
                action.Invoke(client, message);
            }
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
        private void OnPrivateMessage(IrcClient client, IrcMessage message)
        {
            if (!string.IsNullOrWhiteSpace(message.Trail))
            {
                client.Send("PONG {0}", message.Trail);
            }
            OnPingMessageReceived?.ParallelInvoke(this, new IrcRawMessageEventArgs(client, message));
        }
    }
}

