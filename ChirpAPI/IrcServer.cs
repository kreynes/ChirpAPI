using System;

namespace ChirpAPI
{
    public class IrcServer
    {
        public IrcServer(string servername)
        {
            Servername = servername;
        }

        public event EventHandler<IrcMessageEventArgs> OnReplyWelcome;
        public event EventHandler<IrcMessageEventArgs> OnReplyYourHost;
        public event EventHandler<IrcMessageEventArgs> OnReplyServerCreationDate;
        public event EventHandler<IrcMessageEventArgs> OnReplyMyInfo;
        public event EventHandler<IrcMessageEventArgs> OnReplyBounce;

        public string Servername { get; private set; }
        public string Hostname { get; private set; }


        internal void HandleWelcomeMessage(IrcClient client, IrcMessage message)
        {
            OnReplyWelcome?.Invoke(this, new IrcMessageEventArgs(client, message));
        }

        internal void HandleYourHostMessage(IrcClient client, IrcMessage message)
        {
            OnReplyYourHost?.Invoke(this, new IrcMessageEventArgs(client, message));
        }

        internal void HandleServerCreationDateMessage(IrcClient client, IrcMessage message)
        {
            OnReplyServerCreationDate?.Invoke(this, new IrcMessageEventArgs(client, message));
        }

        internal void HandleMyInfoMessage(IrcClient client, IrcMessage message)
        {
            OnReplyMyInfo?.Invoke(this, new IrcMessageEventArgs(client, message));
        }

        internal void HandleServerBounceMessage(IrcClient client, IrcMessage message)
        {
            OnReplyBounce?.Invoke(this, new IrcMessageEventArgs(client, message));
        }
    }
}

