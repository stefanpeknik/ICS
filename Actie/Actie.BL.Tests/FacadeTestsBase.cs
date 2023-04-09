using Actie.BL.Mappers;
using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.Common.Tests;
using Actie.Common.Tests.Factories;
using Actie.DAL;
using Actie.DAL.Mappers;
using Actie.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Actie.BL.Tests;

public class FacadeTestsBase : IAsyncLifetime
{
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().FullName!, seedTestingData: true);
        DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        ActivityEntityMapper = new ActivityEntityMapper();
        UserEntityMapper = new UserEntityMapper();
        ProjectEntityMapper = new ProjectEntityMapper();

        TagEntityMapper = new TagEntityMapper();
        ActivityTagEntityMapper = new ActivityTagEntityMapper();
        UserProjectEntityMapper = new UserProjectEntityMapper();

        ActivityModelMapper = new ActivityModelMapper();
        ActivityTagModelMapper = new ActivityTagModelMapper();
        ProjectModelMapper = new ProjectModelMapper();
        TagModelMapper = new TagModelMapper();
        UserModelMapper = new UserModelMapper();
        UserProjectModelMapper = new UserProjectModelMapper();

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<ActieDbContext> DbContextFactory { get; }

    protected ActivityEntityMapper ActivityEntityMapper { get; }
    protected ActivityTagEntityMapper ActivityTagEntityMapper { get;}
    protected ProjectEntityMapper ProjectEntityMapper { get; }

    protected TagEntityMapper TagEntityMapper { get; }
    protected UserEntityMapper UserEntityMapper { get; }
    protected UserProjectEntityMapper UserProjectEntityMapper { get; }
    protected UnitOfWorkFactory UnitOfWorkFactory { get; }
    protected IActivityModelMapper ActivityModelMapper { get; }
    protected IUserModelMapper UserModelMapper { get; }
    protected IActivityTagModelMapper ActivityTagModelMapper { get; }
    protected IProjectModelMapper ProjectModelMapper { get; }
    protected ITagModelMapper TagModelMapper { get; }
    protected IUserProjectModelMapper UserProjectModelMapper { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}
