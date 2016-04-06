using System;
namespace CSIRC
{
    public sealed class IrcMessageEventArgs : EventArgs
    {
        public IrcClient Client { get; private set; }
        public IrcMessage Message { get; private set; }
        public IrcMessageEventArgs(IrcClient client, IrcMessage message)
        {
            Client = client;
            Message = message;
        }
    }
}

