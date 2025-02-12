﻿using System;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Nandel.Modules;
using Nandel.StikyNotes.Application;
using Nandel.StikyNotes.Providers.Discord.Services;

namespace Nandel.StikyNotes.Providers.Discord
{
    [DependsOn(
        typeof(ApplicationModule)
        )]
    public class DiscordProviderModule : IModule
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDiscordLogger, DiscordLogger>();
            services.AddSingleton<IDiscordMessageHandler, DiscordMessageHandler>();
            services.AddSingleton<DiscordSocketClient>(CreateClient);
            services.AddSingleton<CommandService>(CreateCommands);
        }
        
        private static DiscordSocketClient CreateClient(IServiceProvider services)
        {
            var logger = services.GetRequiredService<IDiscordLogger>();
            var client = new DiscordSocketClient();

            client.Log += logger.LogAsync;

            return client;
        }

        private static CommandService CreateCommands(IServiceProvider services)
        {
            var logger = services.GetRequiredService<IDiscordLogger>();
            var commands = new CommandService();

            commands.Log += logger.LogAsync;

            return commands;
        }
    }
}