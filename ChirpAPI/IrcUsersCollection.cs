using System;
using System.Collections;
using System.Reflection;
using System.Collections.Concurrent;

namespace ChirpAPI
{
    class IrcUsersCollection
    {
        ConcurrentDictionary<string, IrcUser> userCollection;

        public IrcUsersCollection()
        {
            userCollection = new ConcurrentDictionary<string, IrcUser>();
        }

        public void Add(IrcUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            userCollection.TryAdd(user.Nickname, user);
        }

        public void Remove(IrcUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            userCollection.TryRemove(user.Username);
        }

        public IrcUser GetUser(string nickname)
        {
            if (String.IsNullOrWhiteSpace(nickname))
                throw new ArgumentException(nameof(nickname));

            IrcUser user;
            userCollection.TryGetValue(nickname, out user);
            return user;
        }
    }
}

