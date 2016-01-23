using System;
using System.Collections.Generic;

namespace ChirpLib
{
    public class IrcMessageHandler
    {
        public event EventHandler<IrcMessageEventArgs> OnPingMessageReceived;
        public event EventHandler<IrcMessageEventArgs> OnReplyWelcomeReceived;
        public event EventHandler<IrcMessageEventArgs> OnReplyYourHostReceived;
        public event EventHandler<IrcMessageEventArgs> OnReplyCreatedReceived;
        public event EventHandler<IrcMessageEventArgs> OnReplyMyInfoReceived;
        public event EventHandler<IrcMessageEventArgs> OnReplyMapReceived;
        public event EventHandler<IrcMessageEventArgs> OnReplyEndOfMapReceived;
        public event EventHandler<IrcMessageEventArgs> OnReplyMotdStartReceived;
        public event EventHandler<IrcMessageEventArgs> OnReplyMotdReceived;
        public event EventHandler<IrcMessageEventArgs> OnReplyMotdAltReceived;
        public event EventHandler<IrcMessageEventArgs> OnReplyMotdAlt2Received;
        public event EventHandler<IrcMessageEventArgs> OnReplyMotdEndReceived;
        public event EventHandler<IrcMessageEventArgs> OnReplyUserModeIsReceived;

        public static Dictionary<string, Action<IrcClient, IrcMessage>> MessageFactory = new Dictionary<string, Action<IrcClient, IrcMessage>>();

        /// <summary>
        /// Loads the handlers.
        /// </summary>
        internal void LoadHandlers()
        {
            MessageFactory.Add("PING", OnPingMessage);
            MessageFactory.Add("001", OnReplyWelcome);
            MessageFactory.Add("002", OnReplyYourHost);
            MessageFactory.Add("003", OnReplyCreated);
            MessageFactory.Add("004", OnReplyMyInfo);
            MessageFactory.Add("005", OnReplyMap);
            MessageFactory.Add("007", OnReplyMapEnd);
            MessageFactory.Add("375", OnReplyMotdStart);
            MessageFactory.Add("372", OnReplyMotd);
            MessageFactory.Add("377", OnReplyMotdAlt);
            MessageFactory.Add("378", OnReplyMotdAlt);
            MessageFactory.Add("376", OnReplyMotdEnd);
            MessageFactory.Add("221", OnReplyUserModeIs);
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
        private void OnPingMessage(IrcClient client, IrcMessage message)
        {
            if (!String.IsNullOrWhiteSpace(message.Trail))
            {
                client.Send(string.Format("PONG {0}", message.Trail));
            }
            OnPingMessageReceived?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }
        private void OnReplyWelcome(IrcClient client, IrcMessage message)
        {
            OnReplyWelcomeReceived?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }
        private void OnReplyYourHost(IrcClient client, IrcMessage message)
        {
            OnReplyYourHostReceived?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }
        private void OnReplyCreated(IrcClient client, IrcMessage message)
        {
            OnReplyCreatedReceived?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }
        private void OnReplyMyInfo(IrcClient client, IrcMessage message)
        {
            OnReplyMyInfoReceived?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }
        private void OnReplyMap(IrcClient client, IrcMessage message)
        {
            OnReplyMapReceived?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }
        private void OnReplyMapEnd(IrcClient client, IrcMessage message)
        {
            OnReplyEndOfMapReceived?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }
        private void OnReplyMotdStart(IrcClient client, IrcMessage message)
        {
            OnReplyMotdStartReceived?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }
        private void OnReplyMotd(IrcClient client, IrcMessage message)
        {
            OnReplyMotdReceived?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }
        private void OnReplyMotdAlt(IrcClient client, IrcMessage message)
        {
            OnReplyMotdAltReceived?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }
        private void OnReplyMotdAlt2(IrcClient client, IrcMessage message)
        {
            OnReplyMotdAlt2Received?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }
        private void OnReplyMotdEnd(IrcClient client, IrcMessage message)
        {
            List<string> channelsWithKeys = new List<string>();
            List<string> channels = new List<string>();
            List<string> keys = new List<string>();
            if (client.Settings.AutoJoin)
            {
                foreach (string channel in client.Settings.Channels)
                {
                    if (channel.Contains(":"))
                    {
                        string[] split = channel.Split(':');
                        channelsWithKeys.Add(split[0]);
                        keys.Add(split[1]);
                    }
                    else
                    {
                        channels.Add(channel);
                    }
                }
            }
            string channelsWithKeysParsed = string.Join(",", channelsWithKeys.ToArray());
            string keysParsed = string.Join(",", keys.ToArray());
            string channelsParsed = string.Join(",", channels.ToArray());
            client.Send(string.Format("JOIN {0},{1} {2}", channelsWithKeysParsed, channelsParsed, keysParsed));
            /*if (client.Settings.AutoJoin)
            {
                string joined = string.Join(",", client.Settings.Channels);
                client.Send("JOIN {0}", joined);
            }*/
            OnReplyMotdEndReceived?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }
        private void OnReplyUserModeIs(IrcClient client, IrcMessage message)
        {
            OnReplyUserModeIsReceived?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }
        #endregion
    }
}

