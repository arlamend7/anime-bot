using Discord;
using Discord.WebSocket;

namespace LIb.Discord
{
    public class DiscordBot
    {
        public readonly DiscordSocketClient _client;
        public DiscordBot(string token)
        {
            DiscordSocketClient client = new DiscordSocketClient();

            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            client.LoginAsync(TokenType.Bot, token).Wait();
            client.StartAsync().Wait();
            _client = client;
        }


        public async Task SendMessage(string channel, string message)
        {
            var channelId = _client.GetChannel(ulong.Parse(channel)) as SocketTextChannel;
            await channelId.SendMessageAsync(message);
        }

        public async Task SendMessage(ulong channel, string message)
        {
            var channelId = _client.GetChannel(channel) as SocketTextChannel;
            await channelId.SendMessageAsync(message);
        }

        public async Task SendMessage(ulong channel, Embed embed)
        {
            var channelId = _client.GetChannel(channel) as SocketTextChannel;
            await channelId.SendMessageAsync(embed: embed);
        }

        public async Task SendMessage(string channel, Embed embed)
        {
            var channelId = _client.GetChannel(ulong.Parse(channel)) as SocketTextChannel;
            await channelId.SendMessageAsync(embed: embed);
        }

        public async Task SendMessage(ulong channel, EmbedBuilder embed)
        {
            var channelId = _client.GetChannel(channel) as SocketTextChannel;
            await channelId.SendMessageAsync(embed: embed.Build());
        }

        public async Task SendMessage(string channel, EmbedBuilder embed)
        {
            var channelId = _client.GetChannel(ulong.Parse(channel)) as SocketTextChannel;
            await channelId.SendMessageAsync(embed: embed.Build());
        }
    }
}
