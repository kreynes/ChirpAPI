using System;

namespace ChirpLib
{
    public class IrcRawMessageSentEventArgs : EventArgs
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
        /// Initializes a new instance of the <see cref="ChirpLib.IrcRawMessageSentEventArgs"/> class.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="message">Message.</param>
        public IrcRawMessageSentEventArgs(IrcClient client, string message)
        {
            this.Client = client;
            this.Message = message;
        }
    }
}

