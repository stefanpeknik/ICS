using Actie.Common.Tests;
using Actie.Common.Tests.Seeds;
using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Actie.DAL.Tests;

public class DbContextUserTests : DbContextTestsBase
{
    public DbContextUserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_UserWithoutProjectsNorActivities_Persisted()
    {
        // Arrange
        var entity = UserSeeds.EmptyUserEntity with {Name = "Ujo", Surname = "Čaja"};

        // Act
        ActieDbContextSUT.Users.Add(entity);
        await ActieDbContextSUT.SaveChangesAsync();

        // Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntries = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(expected: entity, actual: actualEntries);
    }
}
