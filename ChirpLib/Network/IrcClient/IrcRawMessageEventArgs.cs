using System;

namespace ChirpLib
{
    public sealed class IrcRawMessageEventArgs : EventArgs
    {
        public IrcClient Client { get; private set; }
        public string Message { get; private set; }

        public IrcRawMessageEventArgs(IrcClient client, string message)
        {
            this.Client = client;
            this.Message = message;
        }
    }
}
