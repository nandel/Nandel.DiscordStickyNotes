﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nandel.Modules;
using Nandel.StikyNotes.Provider.EntityFramework;
using Nandel.StikyNotes.Provider.EntityFramework.Context;
using Nandel.StikyNotes.Providers.Sqlite.Context;

namespace Nandel.StikyNotes.Providers.Sqlite
{
    [DependsOn(
        typeof(EntityFrameworkModule)
        )]
    public class SqliteModule : IModule, IHasStart
    {
        private readonly IConfiguration _configuration;

        public SqliteModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SitkyNotesDbContext, SqliteStikyNotesDbContext>(options =>
            {
                var connectionString = _configuration.GetConnectionString("Sqlite");
                options.UseSqlite(connectionString);
            });
        }

        public async Task StartAsync(IServiceProvider services, CancellationToken cancellationToken)
        {
            using var scope = services.CreateScope();
            await using var db = scope.ServiceProvider.GetRequiredService<SitkyNotesDbContext>();
            await db.Database.MigrateAsync(cancellationToken: cancellationToken);
        }

        public static bool HasValidConfiguration(IConfiguration configuration)
        {
            return configuration.GetSection("ConnectionStrings:Sqlite").Exists();
        }
    }
}