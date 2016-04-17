using System;
using System.Collections.Concurrent;

namespace ChirpAPI
{
    public class IrcChannelCollection
    {
        ConcurrentDictionary<string, IrcChannel> channelCollection;
        public IrcChannelCollection()
        {
            channelCollection = new ConcurrentDictionary<string, IrcChannel>();
        }

        public void Add(IrcChannel channel)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));

            channelCollection.TryAdd(channel.Name, channel);
        }

        public void Remove(IrcChannel channel)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));

            channelCollection.TryRemove(channel.Name);
        }

        public IrcChannel GetChannel(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            IrcChannel channel;
            channelCollection.TryGetValue(name, out channel);
            return channel;
        }
    }
}

