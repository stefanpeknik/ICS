﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actie.App.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Actie.DAL;
using Actie.DAL.Factories;
using Actie.DAL.Mappers;

namespace Actie.App;
public static class DALInstaller
{
    public static IServiceCollection AddDALServices(this IServiceCollection services, IConfiguration configuration)
    {
        DALOptions dalOptions = new();
     //   configuration.GetSection("Actie:DAL").Bind(dalOptions);

        services.AddSingleton<DALOptions>(dalOptions);

        if (dalOptions.LocalDb is null && dalOptions.Sqlite is null)
        {
            throw new InvalidOperationException("No persistence provider configured");
        }

        if (dalOptions.LocalDb?.Enabled == false && dalOptions.Sqlite?.Enabled == false)
        {
            throw new InvalidOperationException("No persistence provider enabled");
        }

        if ((dalOptions.LocalDb?.Enabled == true) && (dalOptions.Sqlite?.Enabled == true))
        {
            throw new InvalidOperationException("Both persistence providers enabled");
        }

        if (dalOptions.LocalDb?.Enabled == true)
        {
            services.AddSingleton<IDbContextFactory<ActieDbContext>>(provider => new SqlServerDbContextFactory(dalOptions.LocalDb.ConnectionString));
            services.AddSingleton<IDbMigrator, NoneDbMigrator>();
        }

        if (dalOptions.Sqlite?.Enabled == true)
        {
            if (dalOptions.Sqlite.DatabaseName is null)
            {
                throw new InvalidOperationException($"{nameof(dalOptions.Sqlite.DatabaseName)} is not set");

            }
            string databaseFilePath = Path.Combine(FileSystem.AppDataDirectory, dalOptions.Sqlite.DatabaseName!);
            services.AddSingleton<IDbContextFactory<ActieDbContext>>(provider => new DbContextSQLiteFactory(databaseFilePath, dalOptions?.Sqlite?.SeedDemoData ?? false));
            services.AddSingleton<IDbMigrator, SqliteDbMigrator>();
        }

        services.AddSingleton<ActivityEntityMapper>();
        services.AddSingleton<ActivityTagEntityMapper>();
        services.AddSingleton<ProjectEntityMapper>();
        services.AddSingleton<TagEntityMapper>();
        services.AddSingleton<UserEntityMapper>();
        services.AddSingleton<UserProjectEntityMapper>();

        return services;
    }
}
