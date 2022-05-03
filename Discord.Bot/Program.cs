// See https://aka.ms/new-console-template for more information
using Animes.Domain.Animes.Entities;
using Animes.Domain.Episodios.Entities;
using Animes.IOC;
using Discord;
using Discord.Bot;
using Discord.WebSocket;
using Libs.Base.Serivces.Interfaces;
using Microsoft.Extensions.DependencyInjection;


FactoryExecute factory = new FactoryExecute();
Console.WriteLine("Hello, World!");
DiscordSocketClient client = new();


var services = new ServiceCollection();
services.AddInternalDependencies();
var provider = services.AddSingleton(client).BuildServiceProvider();


var queryService = provider.GetService<IQueryService>();
client.MessageReceived += async arg =>
        {
            if (arg.Content.StartsWith("!helloworld"))
            {
                await arg.Channel.SendMessageAsync($"User '{arg.Author.Username}' successfully ran helloworld!");
            }
            await Task.CompletedTask;
        };

client.ButtonExecuted += async (component) =>
        {
            var args = component.Data.CustomId.Split("-");
            string animeId = args[1];
            if (args[0] == "assistir")
            {
                
                var modal = new ModalBuilder()
                                    .WithTitle("Qual episodio voce gostaria de assistir?")
                                    .WithCustomId($"{animeId}-episodio")
                                    .AddTextInput("Episode", "episode-number", placeholder: "00")
                                    .Build();

                component.RespondWithModalAsync(modal).Wait();
            }
            else
            {
                factory.Add(() =>
                {
                    var entity = queryService.Get<Anime>(int.Parse(animeId));

                    if (args[0] == "subscribe")
                    {
                        Embed embed = new EmbedBuilder()
                                            .WithTitle(entity.Name)
                                            .WithImageUrl(entity.ImageUrl)
                                            .WithColor(Color.Purple)
                                            .WithUrl(entity.Link)
                                            .WithAuthor("Saiko", "https://saikoanimes.net/wp-content/themes/Saiko/images/logo.png", "https://saikoanimes.net/")
                                            .Build();

                        MessageComponent button = new ComponentBuilder()
                                           .WithButton($"Unsubscribe", $"subscribe-{entity.Id}", ButtonStyle.Secondary)
                                           .WithButton($"Assistir", $"assistir-{entity.Id}", ButtonStyle.Success)
                                           .Build();
                        component.User.SendMessageAsync(embed: embed, components: button).Wait();
                    }
                });
                await component.RespondAsync();
            }
        };


client.ModalSubmitted += async modal =>
{
    // Get the values of components.
    List<SocketMessageComponentData> components =
        modal.Data.Components.ToList();
    string number = components
        .First(x => x.CustomId == "episode-number").Value;
    var args = modal.Data.CustomId.Split("-");
    long animeId = long.Parse(args[0]);

    factory.Add(() =>
    {
        var entity = queryService.Get<Anime>(animeId);
        var episode = queryService.Query<Episode>().Where(x => x.Anime.Id == animeId);
        Embed embed = new EmbedBuilder()
                        .WithTitle(entity.Name + " - ep " + episode.FirstOrDefault().Name)
                        .WithImageUrl(entity.ImageUrl)
                        .WithColor(Color.Purple)
                        .WithDescription("Just click to watch")
                        .WithUrl("https://download.saikoanimes.net/secure/uploads/77294?shareable_link=2623")
                        .WithAuthor("Saiko", "https://saikoanimes.net/wp-content/themes/Saiko/images/logo.png", "https://saikoanimes.net/")
                        .Build();

         
        var message = modal.User.SendMessageAsync(embed: embed).Result;
        Task.Delay(5000).Wait();
        message.DeleteAsync().Wait();
    });
    await modal.RespondAsync();
};

client.LoginAsync(TokenType.Bot, "OTcwODUyNzg2NjUwNjQ0NDgx.YnB_HQ.fouzfLmYKIUUxMPCysuqnN1sjic").Wait();
client.StartAsync().Wait();

await Task.Delay(-1);