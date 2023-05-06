using System.Runtime.InteropServices.JavaScript;
using Actie.BL.Mappers;
using Actie.BL.Mappers.Interfaces;
using Actie.Common.Tests;
using Actie.Common.Tests.Factories;
using Actie.Common.Tests.Seeds;
using Actie.DAL;
using Actie.DAL.Mappers;
using Actie.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

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

        ActivityTagModelMapper = new ActivityTagModelMapper();
        UserProjectModelMapper = new UserProjectModelMapper();

        ActivityModelMapper = new ActivityModelMapper(ActivityTagModelMapper);
        UserModelMapper = new UserModelMapper(ActivityModelMapper, UserProjectModelMapper);
        ProjectModelMapper = new ProjectModelMapper(ActivityModelMapper, UserProjectModelMapper);
        TagModelMapper = new TagModelMapper(ActivityTagModelMapper);


        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<ActieDbContext> DbContextFactory { get; }

    protected ActivityEntityMapper ActivityEntityMapper { get; }
    protected UserEntityMapper UserEntityMapper { get; }
    protected ProjectEntityMapper ProjectEntityMapper { get; }
    protected TagEntityMapper TagEntityMapper { get; }

    protected ActivityTagEntityMapper ActivityTagEntityMapper { get; }
    protected UserProjectEntityMapper UserProjectEntityMapper { get; }

    
    protected IActivityModelMapper ActivityModelMapper { get; }
    protected IUserModelMapper UserModelMapper { get; }
    protected IProjectModelMapper ProjectModelMapper { get; }
    protected ITagModelMapper TagModelMapper { get; }

    protected ActivityTagModelMapper ActivityTagModelMapper { get; }
    protected UserProjectModelMapper UserProjectModelMapper { get; }

    protected UnitOfWorkFactory UnitOfWorkFactory { get; }


    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        TestSeedsInit.LoadLists();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}
