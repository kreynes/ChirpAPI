using System;
using System.Threading;
using System.Collections.Generic;
namespace ChirpAPI
{
    public class IrcMessageFactory
    {
        private static Dictionary<string, Action<IrcClient, IrcMessage>> MessageHandler = new Dictionary<string, Action<IrcClient, IrcMessage>>();
        private static Mutex handlerMutex = new Mutex();

        internal void LoadHandlers()
        {
            MessageHandler.Add("PING", async (IrcClient client, IrcMessage message) =>
            {
                if (!String.IsNullOrWhiteSpace(message.Trail))
                {
                    await client.Send($"PONG {message.Trail}");
                }
            });
            MessageHandler.Add("PONG", async (IrcClient client, IrcMessage message) =>
            {
                client.pingStopwatch.Stop();
                client.pingStopwatch.Reset();
                client.lastPongTimestamp = DateTime.Now;
                await client.Send($"PRIVMSG Alon {client.lastPongTimestamp}");
            });
            MessageHandler.Add("001", (IrcClient client, IrcMessage message) =>
            {
                client.Server.HandleWelcomeMessage(client, message);
            });
            MessageHandler.Add("002", (IrcClient client, IrcMessage message) =>
            {
                client.Server.HandleYourHostMessage(client, message);
            });
            MessageHandler.Add("003", (IrcClient client, IrcMessage message) =>
            {
                client.Server.HandleServerCreationDateMessage(client, message);
            });
            MessageHandler.Add("004", (IrcClient client, IrcMessage message) =>
            {
                client.Server.HandleMyInfoMessage(client, message);
            });
            MessageHandler.Add("005", (IrcClient client, IrcMessage message) =>
            {
                client.Server.HandleServerBounceMessage(client, message);
            });
            MessageHandler.Add("375", (IrcClient client, IrcMessage message) =>
            {
                client.Server.HandleMotd(client, message);
            });
            MessageHandler.Add("372", (IrcClient client, IrcMessage message) =>
            {
                client.Server.HandleMotd(client, message);
            });
            MessageHandler.Add("376", (IrcClient client, IrcMessage message) =>
            {
                client.Server.HandleMotd(client, message);
            });
        }

        internal void Execute(string key, IrcClient client, IrcMessage message)
        {
            Action<IrcClient, IrcMessage> action;
            if (MessageHandler.TryGetValue(key, out action))
            {
                action.Invoke(client, message);

            }
        }

        public void Add(string key, Action<IrcClient, IrcMessage> handler)
        {
            if (String.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "Null, empty or whitespace.");
            if (handler == null)
                throw new ArgumentNullException(nameof(handler), "Null handler");
            if (MessageHandler.ContainsKey(key))
                throw new InvalidOperationException("Handler with this key already exists.");
            handlerMutex.WaitOne();
            MessageHandler.Add(key, handler);
            handlerMutex.ReleaseMutex();
        }

        public void Remove(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "Null, empty or whitespace.");
            if (!MessageHandler.ContainsKey(key))
                throw new InvalidOperationException("No handler with this key found.");
            handlerMutex.WaitOne();
            MessageHandler.Remove(key);
            handlerMutex.ReleaseMutex();
        }
    }
}
