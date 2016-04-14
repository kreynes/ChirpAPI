using System;

namespace ChirpAPI
{

    public static class IrcClientExtensions
    {
        public static void SendPassword(this IrcEndpoint client, string password)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            client.Send($"PASS {password}");
        }
        public static void SendNickname(this IrcEndpoint client, string nickname)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentNullException(nameof(nickname));

            client.Send($"NICK {nickname}");
        }
        public static void SendUsername(this IrcEndpoint client, string username, int mode, string realname)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrWhiteSpace(realname))
                throw new ArgumentNullException(nameof(realname));

            client.Send($"USER {username} {mode} * :{realname}");
        }
        public static void SendOper(this IrcEndpoint client, string username, string password)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            client.Send($"OPER {username} {password}");
        }
        public static void SendUserMode(this IrcEndpoint client, string nickname, string mode)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentNullException(nameof(nickname));
            if (string.IsNullOrWhiteSpace(mode))
                throw new ArgumentNullException(nameof(mode));

            client.Send($"MODE {nickname} {mode}");
        }
        public static void SendService(this IrcEndpoint client, string nickname, string reserved, string distirbution, string type, string reserved_, string info)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentNullException(nameof(nickname));
            if (string.IsNullOrWhiteSpace(distirbution))
                throw new ArgumentNullException(nameof(distirbution));
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrWhiteSpace(info))
                throw new ArgumentNullException(nameof(info));

            client.Send($"SERVICE {nickname} {reserved} {distirbution} {type} {reserved_} {info}");
        }
        public static void SendQuit(this IrcEndpoint client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            client.Send($"QUIT");
        }
        public static void SendQuit(this IrcEndpoint client, string message)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            client.Send($"QUIT {message}");
        }
        public static void SendSQuit(this IrcEndpoint client, string server, string comment)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(server))
                throw new ArgumentNullException(nameof(server));
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentNullException(nameof(comment));

            client.Send($"SQUIT {server} {comment}");
        }
        public static void SendJoin(this IrcEndpoint client, string channel)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));

            client.Send($"JOIN {channel}");
        }
        public static void SendJoin(this IrcEndpoint client, string channel, string key)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            client.Send($"JOIN {channel} {key}");
        }
        public static void SendJoin(this IrcEndpoint client, string[] channels, string[] keys)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (channels.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(channels));
            if (keys.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(keys));

            client.Send($"JOIN {string.Join(",", channels)} {string.Join(",", keys)}");
        }
        public static void SendPart(this IrcEndpoint client, string channel)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));

            client.Send($"PART {channel}");
        }
        public static void SendPart(this IrcEndpoint client, string channel, string message)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            client.Send($"PART {channel} {message}");
        }
        public static void SendPart(this IrcEndpoint client, string[] channels, string message)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (channels.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(channels));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            client.Send($"PART {string.Join(",", channels)} {message}");
        }
        public static void SendChannelMode(this IrcEndpoint client, string channel, string mode, string modeParameters)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));
            if (string.IsNullOrWhiteSpace(mode))
                throw new ArgumentNullException(nameof(mode));
            if (string.IsNullOrWhiteSpace(modeParameters))
                throw new ArgumentNullException(nameof(modeParameters));

            client.Send($"MODE {channel} {mode} {modeParameters}");
        }
        public static void SendTopic(this IrcEndpoint client, string channel)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));

            client.Send($"TOPIC {channel}");
        }
        public static void SendTopic(this IrcEndpoint client, string channel, string topic)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));
            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentNullException(nameof(topic));

            client.Send($"TOPIC {channel} {topic}");
        }
        public static void SendNames(this IrcEndpoint client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            client.Send($"NAMES");
        }
        public static void SendNames(this IrcEndpoint client, string channel)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));

            client.Send($"NAMES {channel}");
        }
        public static void SendNames(this IrcEndpoint client, string channel, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"NAMES {channel} {target}");
        }
        public static void SendNames(this IrcEndpoint client, string[] channels)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (channels.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(channels));

            client.Send($"NAMES {string.Join(",", channels)}");
        }
        public static void SendNames(this IrcEndpoint client, string[] channels, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (channels.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(channels));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"NAMES {string.Join(",", channels)} {target}");
        }
        public static void SendList(this IrcEndpoint client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            client.Send($"LIST");
        }
        public static void SendList(this IrcEndpoint client, string channel)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));

            client.Send($"LIST {channel}");
        }
        public static void SendList(this IrcEndpoint client, string channel, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"LIST {channel} {target}");
        }
        public static void SendList(this IrcEndpoint client, string[] channels)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (channels.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(channels));

            client.Send($"LIST {string.Join(",", channels)}");
        }
        public static void SendList(this IrcEndpoint client, string[] channels, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (channels.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(channels));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"LIST {string.Join(",", channels)} {target}");
        }
        public static void SendInvite(this IrcEndpoint client, string nickname, string channel)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentNullException(nameof(nickname));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));

            client.Send($"INVITE {nickname} {channel}");
        }
        public static void SendKick(this IrcEndpoint client, string channel, string nickname)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));
            if (string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentNullException(nameof(nickname));

            client.Send($"KICK {channel} {nickname}");
        }
        public static void SendKick(this IrcEndpoint client, string channel, string nickname, string comment)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));
            if (string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentNullException(nameof(nickname));
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentNullException(nameof(comment));

            client.Send($"KICK {channel} {nickname} {comment}");
        }
        public static
        void SendPrivateMessage(this IrcEndpoint client, string target, string message)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            client.Send($"PRIVMSG {target} :{message}");
        }
        public static void SendNoticeMessage(this IrcEndpoint client, string target, string message)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            client.Send($"NOTICE {target} :{message}");
        }
        public static void SendMotd(this IrcEndpoint client, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"MOTD {target}");
        }
        public static void SendLusers(this IrcEndpoint client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            client.Send($"LUSERS");
        }
        public static void SendLusers(this IrcEndpoint client, string mask)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(mask))
                throw new ArgumentNullException(nameof(mask));

            client.Send($"LUSERS {mask}");
        }
        public static void SendLusers(this IrcEndpoint client, string mask, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(mask))
                throw new ArgumentNullException(nameof(mask));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"LUSERS {mask} {target}");
        }
        public static void SendVersion(this IrcEndpoint client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            client.Send($"VERSION");
        }
        public static void SendVersion(this IrcEndpoint client, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"VERSION {target}");
        }
        public static void SendStats(this IrcEndpoint client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            client.Send($"STATS");
        }
        public static void SendStats(this IrcEndpoint client, string query)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query));

            client.Send($"STATS {query}");
        }
        public static void SendStats(this IrcEndpoint client, string query, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"STATS {query} {target}");
        }
        public static void SendLinks(this IrcEndpoint client, string remoteServer)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(remoteServer))
                throw new ArgumentNullException(nameof(remoteServer));

            client.Send($"LINKS {remoteServer}");
        }
        public static void SendLinks(this IrcEndpoint client, string remoteServer, string serverMask)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(remoteServer))
                throw new ArgumentNullException(nameof(remoteServer));
            if (string.IsNullOrWhiteSpace(serverMask))
                throw new ArgumentNullException(nameof(serverMask));

            client.Send($"LINKS {remoteServer} {serverMask}");
        }
        public static void SendTime(this IrcEndpoint client, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"TIME {target}");
        }
        public static void SendConnect(this IrcEndpoint client, string target, int port)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));
            if (port == 0)
                throw new ArgumentException(nameof(port));

            client.Send($"CONNECT {target} {port}");
        }
        public static void SendConnect(this IrcEndpoint client, string target, int port, string remoteServer)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));
            if (port == 0)
                throw new ArgumentException(nameof(port));
            if (string.IsNullOrWhiteSpace(remoteServer))
                throw new ArgumentNullException(nameof(remoteServer));

            client.Send($"CONNECT {target} {port} {remoteServer}");
        }
        public static void SendTrace(this IrcEndpoint client, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"TRACE {target}");
        }
        public static void SendAdmin(this IrcEndpoint client, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"ADMIN {target}");
        }
        public static void SendInfo(this IrcEndpoint client, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"INFO {target}");
        }
        public static void SendServlist(this IrcEndpoint client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            client.Send($"SERVLIST");
        }
        public static void SendServlist(this IrcEndpoint client, string mask)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(mask))
                throw new ArgumentNullException(nameof(mask));

            client.Send($"SERVLIST {mask}");
        }
        public static void SendServlist(this IrcEndpoint client, string mask, string type)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(mask))
                throw new ArgumentNullException(nameof(mask));
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));

            client.Send($"SERVLIST {mask} {type}");
        }
        public static void SendSquery(this IrcEndpoint client, string serviceName, string message)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(serviceName))
                throw new ArgumentNullException(nameof(serviceName));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            client.Send($"SQUERY {serviceName} :{message}");
        }
        public static void SendWho(this IrcEndpoint client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            client.Send($"WHO");
        }
        public static void SendWho(this IrcEndpoint client, string mask, bool onlyOperators = false)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(mask))
                throw new ArgumentNullException(nameof(mask));

            if (!onlyOperators)
                client.Send($"WHO {mask}");
            else
                client.Send($"WHO {mask} o");
        }
        public static void SendWhoIs(this IrcEndpoint client, string mask)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(mask))
                throw new ArgumentNullException(nameof(mask));

            client.Send($"WHOIS {mask}");
        }
        public static void SendWhoIs(this IrcEndpoint client, string target, string mask)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));
            if (string.IsNullOrWhiteSpace(mask))
                throw new ArgumentNullException(nameof(mask));

            client.Send($"WHOIS {target} {mask}");
        }
        public static void SendWhoWas(this IrcEndpoint client, string nickname)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentNullException(nameof(nickname));

            client.Send($"WHOWAS {nickname}");
        }
        public static void SendWhoWas(this IrcEndpoint client, string nickname, int count)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentNullException(nameof(nickname));
            if (count <= 0)
                throw new ArgumentException(nameof(count));

            client.Send($"WHOWAS {nickname} {count}");
        }
        public static void SendWhoWas(this IrcEndpoint client, string nickname, int count, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentNullException(nameof(nickname));
            if (count <= 0)
                throw new ArgumentException(nameof(count));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"WHOWAS {nickname} {count} {target}");
        }
        public static void SendKill(this IrcEndpoint client, string nickname, string comment)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentNullException(nameof(nickname));
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentNullException(nameof(comment));

            client.Send($"KILL {nickname} {comment}");
        }
        public static void SendPing(this IrcEndpoint client, string server)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(server))
                throw new ArgumentNullException(nameof(server));

            client.Send($"PING {server}");
        }
        public static void SendPing(this IrcEndpoint client, string server, string forwardTo)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(server))
                throw new ArgumentNullException(nameof(server));
            if (string.IsNullOrWhiteSpace(forwardTo))
                throw new ArgumentNullException(nameof(forwardTo));

            client.Send($"PING {server} {forwardTo}");
        }
        public static void SendPong(this IrcEndpoint client, string server)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(server))
                throw new ArgumentNullException(nameof(server));

            client.Send($"PONG {server}");
        }
        public static void SendPong(this IrcEndpoint client, string server, string forwardTo)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(server))
                throw new ArgumentNullException(nameof(server));
            if (string.IsNullOrWhiteSpace(forwardTo))
                throw new ArgumentNullException(nameof(forwardTo));

            client.Send($"PONG {server} {forwardTo}");
        }

        public static void SendError(this IrcEndpoint client, string errorMessage)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentNullException(nameof(errorMessage));

            client.Send($"ERROR {errorMessage}");
        }
        public static void SendAway(this IrcEndpoint client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            client.Send($"AWAY");
        }
        public static void SendAway(this IrcEndpoint client, string message)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            client.Send($"AWAY :{message}");
        }
        public static void SendDie(this IrcEndpoint client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            client.Send($"DIE");
        }
        public static void SendRestart(this IrcEndpoint client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            client.Send($"RESTART");
        }
        public static void SendSummon(this IrcEndpoint client, string user)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(user))
                throw new ArgumentNullException(nameof(user));

            client.Send($"SUMMON {user}");
        }
        public static void SendSummon(this IrcEndpoint client, string user, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(user))
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"SUMMON {user} {target}");
        }
        public static void SendSummon(this IrcEndpoint client, string user, string target, string channel)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(user))
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));

            client.Send($"SUMMON {user} {target} {channel}");
        }
        public static void SendUsers(this IrcEndpoint client, string target)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(nameof(target));

            client.Send($"USERS {target}");
        }
        public static void SendWallops(this IrcEndpoint client, string message)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            client.Send($"WALLOPS {message}");
        }
        public static void SendUserhost(this IrcEndpoint client, string nickname)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentNullException(nameof(nickname));

            client.Send($"USERHOST {nickname}");
        }
        public static void SendUserhost(this IrcEndpoint client, string[] nicknames)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (nicknames.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(nicknames));

            client.Send($"USERHOST {string.Join(" ", nicknames)}");
        }
        public static void SendIsOn(this IrcEndpoint client, string nickname)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentNullException(nameof(nickname));

            client.Send($"ISON {nickname}");
        }
        public static void SendIsOn(this IrcEndpoint client, string[] nicknames)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (nicknames.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(nicknames));
            if (nicknames.Length > 5)
                throw new ArgumentException("Nickname list should not be contain more than 5 elements.", nameof(nicknames));

            client.Send($"ISON {string.Join(" ", nicknames)}");
        }
    }
}

