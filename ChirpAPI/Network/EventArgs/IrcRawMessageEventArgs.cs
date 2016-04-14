using System;
namespace ChirpAPI
{
    public class IrcRawMessageEventArgs : EventArgs
    {
        public IrcClient Client { get; private set; }
        public string Message { get; private set; }

        public IrcRawMessageEventArgs(IrcClient client, string message)
        {
            Client = client;
            Message = message;
        }
    }
}

