using System;
namespace ChirpAPI
{
    public class IrcUser
    {
        public IrcUser(string nickname, string username, string realname)
        {
            Nickname = nickname;
            Username = username;
            Realname = realname;
        }

        public string Nickname { get; internal set; }
        public string Username { get; internal set; }
        public string Realname { get; internal set; }
        public string Hostname { get; internal set; }
        public bool IsOnline { get; internal set; }
        public bool IsAway { get; internal set; }
        public string AwayMessage { get; internal set; }

    }
}