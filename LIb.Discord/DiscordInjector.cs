using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace LIb.Discord
{
    public static class DiscordInjector
    {
        public class DiscordChannels : IDiscordChannels
        {
            private readonly IDiscordClient _discordClient;

            public DiscordChannels(IDiscordClient discordClient)
            {
                this._discordClient = discordClient;
            }
            public IMessageChannel Animes => _discordClient.GetChannelAsync(970885515597475840).Result as IMessageChannel;

        }
        public static void InjectBot(this IServiceCollection services)
        {
            services.AddSingleton<IDiscordClient>(x =>
            {
                DiscordSocketClient client = new DiscordSocketClient();
                var token = "OTcwODUyNzg2NjUwNjQ0NDgx.YnB_HQ.fouzfLmYKIUUxMPCysuqnN1sjic";

                // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
                // var token = File.ReadAllText("token.txt");
                // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

                client.LoginAsync(TokenType.Bot, token).Wait();
                client.StartAsync().Wait();
                client.Log += DiscordCommands.Log;
                client.MessageReceived += DiscordCommands.ClientOnMessageReceived;

                // Block this task until the program is closed.
                return client;
            });

            services.AddSingleton<IDiscordChannels, DiscordChannels>();

        }

    }
}