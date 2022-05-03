using Discord.WebSocket;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIb.Discord
{
    public static class DiscordBotBuilder
    {
        public static DiscordBot Build(string token)
        {
            DiscordSocketClient client = new();

            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            client.LoginAsync(TokenType.Bot, token).Wait();
            client.StartAsync().Wait();

            // Block this task until the program is closed.
            return new DiscordBot(token);
        }
        public static void AddListeners(this DiscordSocketClient client)
        {
           
        }

    }
}
