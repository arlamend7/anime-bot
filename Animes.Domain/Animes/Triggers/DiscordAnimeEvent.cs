using Animes.Domain.Animes.Entities;
using Discord;
using LIb.Discord;
using Libs.Base.Triggers;
using Libs.Base.Triggers.Enums;

namespace Animes.Domain.Animes.Triggers
{
    public class DiscordAnimeEvent : InsertTrigger<Anime>
    {
        private readonly IDiscordChannels discordChannels;
        public override TriggerOnResult OnResult => TriggerOnResult.Stop;

        public DiscordAnimeEvent(IDiscordChannels discordChannels)
        {
            this.discordChannels = discordChannels;
        }

        protected override void OnTrigger(Anime entity)
        {
            Embed embed = new EmbedBuilder()
                .WithTitle(entity.Name)
                .WithImageUrl(entity.ImageUrl)
                .WithColor(Color.Purple)
                .WithUrl(entity.Link)
                .WithAuthor("Saiko", "https://saikoanimes.net/wp-content/themes/Saiko/images/logo.png", "https://saikoanimes.net/")
                .Build();

            MessageComponent button = new ComponentBuilder()
                                        .WithButton($"Subscribe", $"subscribe-{entity.Id}", ButtonStyle.Success)
                                        .WithButton($"Assistir", $"assistir-{entity.Id}", ButtonStyle.Secondary)
                                        .Build();

            discordChannels.Animes.SendMessageAsync(embed: embed, components: button).Wait();
        }
    }
}
