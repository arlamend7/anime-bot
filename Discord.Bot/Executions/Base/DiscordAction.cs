using System;
using Discord.WebSocket;
using Libs.Base.Serivces.Interfaces;

namespace Discord.Bot.Executions.Base
{
    public abstract class DiscordAction
    {
        protected readonly IDiscordClient _discordClient;
        protected readonly IServiceProvider _serviceProvider;

        public DiscordAction(IDiscordClient discordClient, IServiceProvider serviceProvider)
        {
            _discordClient = discordClient;
            this._serviceProvider = serviceProvider;
        }

        public Task Execution<T>(T message)
                where T : SocketMessageComponent
        {
            return Execute(message, message.Channel, message.User, message.Data.CustomId);
        }

        protected abstract Task Execute(SocketMessageComponent message, IChannel channel, IUser author, string customId);

        protected void ExecuteAsync(Action execution)
        {
            Task.Run(execution);
        }
    }
}

