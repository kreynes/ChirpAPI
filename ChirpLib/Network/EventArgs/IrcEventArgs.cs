using System;

namespace ChirpLib
{
    public sealed class IrcEventArgs : EventArgs
    {
        public IrcClient Client { get; private set; }
        public IrcMessage Message { get; private set; }

        public IrcEventArgs(IrcClient client, IrcMessage message)
        {
            this.Client = client;
            this.Message = message;
        }
    }
}

