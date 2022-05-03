using Discord;
using Discord.WebSocket;
using static LIb.Discord.DiscordInjector;

namespace LIb.Discord
{
    public static class DiscordCommands
    {
        public static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public static Task ClientOnMessageReceived(SocketMessage arg)
        {
            if (arg.Content.StartsWith("!helloworld"))
            {
                arg.Channel.SendMessageAsync($"User '{arg.Author.Username}' successfully ran helloworld!");
            }
            return Task.CompletedTask;
        }

        public static async Task MyButtonHandler(SocketMessageComponent component)
        {

                var button = new TextInputBuilder()
                                        .WithLabel("My Text")
                                        .WithCustomId("text_input")
                                        .Build();


                var modal = new ModalBuilder()
                                    .WithTitle("Anime que voce escolheu")
                                    .WithCustomId("assistir-episodio")
                                    .AddTextInput("Episode", "episode_number", placeholder: "00")
                                    .Build();
                
               await component.RespondWithModalAsync(modal);
        
        }
    }
}
