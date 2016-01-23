using System;

namespace ChirpLib
{
    public sealed class IrcMessageEventArgs : EventArgs
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
        public IrcMessage Message { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChirpLib.IrcRawMessageEventArgs"/> class.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="message">Message.</param>
        public IrcMessageEventArgs(IrcClient client, IrcMessage message)
        {
            this.Client = client;
            this.Message = message;
        }
    }
}
