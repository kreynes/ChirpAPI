using System;
using ChirpAPI;
using System.Threading.Tasks;
using System.Linq;

namespace ChirpAPI.Example
{
    class MainClass
    {
        private static IrcClient client;
        static void Main(string[] args)
        {
            IrcConnectionSettings connSettings = new IrcConnectionSettings("irc.fyrechat.net", 6669, false, false)
            {
                AutoReconnect = true,
                RetryAttempts = 3
            };
            client = new IrcClient(connSettings);
            //client.OnMessageReceived += Client_OnMessageReceived;
            client.OnMessageSent += Client_OnMessageSent;
            client.OnConnected += Client_OnConnected;
            client.Server.OnReplyWelcome += Client_OnReplyWelcome;
            client.Server.OnMotd += Client_OnMotd;

            Initialize().Wait();
        }

        static async void Client_OnConnected(object sender, EventArgs e)
        {
            Console.WriteLine($"Connected");
            await client.Send("USER ChirpAPI 8 * ChirpAPI");
            await client.Send("NICK ChirpAPI");
            await client.Send("JOIN #vana");

        }

        static void Client_OnTryReconnect(object sender, EventArgs e)
        {
            Console.WriteLine("Trying to reconnect");
        }

        static void Client_OnReconnectFailed(object sender, EventArgs e)
        {
            Console.WriteLine("Failed to reconnect. Disposing!");
        }

        static void Client_OnMessageReceived(object sender, IrcMessageEventArgs e)
        {
            //if (e.Message.Command.Contains("PING"))
            //{
            //    await client.Send("PONG " + e.Message.Trail);
            //}

            string prm = String.Join(" ", e.Message.Parameters.ToArray());
            Console.WriteLine($"p: {e.Message.Prefix} c: {e.Message.Command} params: {prm} trail:{e.Message.Trail}");
        }

        static void Client_OnMessageSent(object sender, IrcRawMessageEventArgs e)
        {
            Console.WriteLine($"[SENT] {e.Message}");
        }

        static void Client_OnReplyWelcome(object sender, IrcMessageEventArgs e)
        {
            Console.WriteLine(e.Message.Trail);
        }

        static void Client_OnMotd(object sender, IrcMotdEventArgs e)
        {
            Console.WriteLine($"[{e.Status}] {e.Message}");
        }

        private static async Task Initialize()
        {
            try
            {
                await client.ConnectAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}