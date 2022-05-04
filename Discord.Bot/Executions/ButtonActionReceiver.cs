using System;
using Animes.Domain.Animes.Entities;
using Animes.Domain.Subscriptions.Entityies;
using Discord.Bot.Executions.Base;
using Discord.WebSocket;
using Libs.Base.Injectors.Interfaces;
using Libs.Base.Serivces.Interfaces;

namespace Discord.Bot.Executions
{
    public class ButtonExecution : DiscordAction
    {
        private readonly IApplicationService<Subscriber> _subscriberService;
        public ButtonExecution(IDiscordClient discordClient,  IServiceProvider serviceProvider): base(discordClient, serviceProvider)
        {


        }

        protected override async Task Execute(SocketMessageComponent message, IChannel channel, IUser author, string customId)
        {
            var args = customId.Split("-");
            Task action = args[0] switch
            {
                "assistir" => Assistir(message, customId),
                "subscription" => Subscription(message, author, customId),
                _ => throw new Exception("nao mapeado")
            };

            await action;
        }

        public async Task Assistir(SocketMessageComponent message, string animeId)
        {
            var modal = new ModalBuilder()
                                    .WithTitle("Qual episodio voce gostaria de assistir?")
                                    .WithCustomId($"{animeId}-episodio")
                                    .AddTextInput("Episode", "episode-number", placeholder: "00")
                                    .Build();

            await message.RespondWithModalAsync(modal);
        }

        public async Task Subscription(SocketMessageComponent message, IUser author, string animeId)
        {
            ExecuteAsync(() =>
            {
                var entity = _queryService.Get<Anime>(int.Parse(animeId));
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

                author.SendMessageAsync(embed: embed, components: button).Wait();

            });

            await message.RespondAsync();
        }
    }
}

