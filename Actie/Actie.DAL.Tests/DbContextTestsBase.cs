// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Actie.Common.Tests;
using CookBook.Common.Tests.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;
using Xunit.Abstractions;

namespace Actie.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
        // DbContextFactory = new DbContextLocalDbTestingFactory(GetType().FullName!, seedTestingData: true);
        DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        ActieDbContextSUT = DbContextFactory.CreateDbContext();
    }

    protected IDbContextFactory<ActieDbContext> DbContextFactory { get; }
    protected ActieDbContext ActieDbContextSUT { get; }

    public async Task InitializeAsync()
    {
        await ActieDbContextSUT.Database.EnsureDeletedAsync();
        await ActieDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await ActieDbContextSUT.Database.EnsureDeletedAsync();
        await ActieDbContextSUT.DisposeAsync();
    }
}
