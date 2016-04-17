using System;
namespace ChirpAPI
{
    public sealed class IrcMotdEventArgs : EventArgs
    {
        public IrcClient Client { get; private set; }
        public string Status { get; private set; }
        public string Message { get; private set; }
        public IrcMotdEventArgs(IrcClient client, string status, string message)
        {
            Client = client;
            Status = status;
            Message = message;
        }
    }
}

