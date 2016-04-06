using System;
using System.Threading;
using System.Collections.Generic;
namespace CSIRC
{
    public class IrcMessageFactory
    {
        private static Dictionary<string, Action<IrcClient, IrcMessage>> MessageHandler = new Dictionary<string, Action<IrcClient, IrcMessage>>();
        private static Mutex handlerMutex = new Mutex();

        internal void LoadHandlers()
        {
            MessageHandler.Add("PING", (IrcClient client, IrcMessage message) =>
            {
                if (!String.IsNullOrWhiteSpace(message.Trail))
                {
                    client.Send($"PONG {message.Trail}");
                }
            });
            MessageHandler.Add("PONG", (IrcClient client, IrcMessage message) =>
            {
                client.pingStopwatch.Stop();
                client.pingStopwatch.Reset();
                client.lastPongTimestamp = DateTime.Now;
                client.Send($"PRIVMSG Alon {client.lastPongTimestamp}");
            });
            //MessageHandler.Add("376", (IrcClient client, IrcMessage message) =>
            //{
            //    client.Send("JOIN #pdgn");
            //});
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
