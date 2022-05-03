using Animes.Domain.Animes.Entities;
using LIb.Discord;
using Libs.Base.Triggers;
using Libs.Base.Triggers.Enums;

namespace Animes.Domain.Animes.Triggers
{
    public class DiscordAnimeEvent : InsertTrigger<Anime>
    {
        private readonly IDiscordChannels discordChannels;
        public override TriggerOnResult OnResult => TriggerOnResult.Continue;

        public DiscordAnimeEvent(IDiscordChannels discordChannels)
        {
            this.discordChannels = discordChannels;
        }

        protected override void OnTrigger(Anime entity)
        {
            string message = $"{entity.Name} - {entity.Link}";
            discordChannels.Animes.SendMessageAsync(message).Wait();
        }
    }
}
