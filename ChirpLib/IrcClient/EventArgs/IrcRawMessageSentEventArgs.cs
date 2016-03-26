namespace ChirpLib.IrcClient.EventArgs
{
    public class IrcRawMessageEventArgs : System.EventArgs
    {
        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>The client.</value>
        public IrcClient Client { get; private set; }
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="IrcRawMessageEventArgs"/> class.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="message">Message.</param>
        public IrcRawMessageEventArgs(IrcClient client, string message)
        {
            Client = client;
            Message = message;
        }
    }
}

