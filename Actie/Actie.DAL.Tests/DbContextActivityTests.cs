using Actie.Common.Tests;
using Actie.Common.Tests.Seeds;
using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Actie.DAL.Tests;

public class DbContextActivityTests : DbContextTestsBase
{
    public DbContextActivityTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_ActivityWithoutTags_Persisted()
    {
        // Arrange
        var entity = ActivitySeeds.EmptyActivityEntity with
        {
            Name = "Karate",
            Description = "Karate coaching sessions from Mr. Kim Un",
            Start = DateTime.Parse("2023-01-04 07:00 AM"),
            End = DateTime.Parse("2023-01-04 08:00 AM"),
            Type = "Indoors",
        };

        // Act
        ActieDbContextSUT.Activities.Add(entity);
        await ActieDbContextSUT.SaveChangesAsync();

        // Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntries = await dbx.Activities.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(expected: entity, actual: actualEntries);
    }

    [Fact]
    public async Task AddNew_ActivityWithTag_Persisted()
    {
        //Arrange
        var entity = ActivitySeeds.EmptyActivityEntity with
        {
            Name = "Gym",
            Description = "Gym sessions under qualified trainer",
            Start = DateTime.Parse("2023-01-04 08:00 PM"),
            End = DateTime.Parse("2023-01-04 10:00 PM"),
            Type = "Indoors",
            Tags = new List<ActivityTagEntity> {}
        };

        //Act
        ActieDbContextSUT.Activities.Add(entity);
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Activities
            .Include(i => i.Tags)
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task GetById_Activity()
    {
        //Act
        var entity = await ActieDbContextSUT.Activities
            .SingleAsync(i => i.Id == ActivitySeeds.ActivityEntity.Id);

        //Assert
        DeepAssert.Equal(ActivitySeeds.ActivityEntity with { Tags = Array.Empty<ActivityTagEntity>(), Project = null, User = null}, entity);
    }


    [Fact]
    public async Task Update_Activity_Persisted()
    {
        //Arrange
        var baseEntity = ActivitySeeds.ActivityEntityUpdate;
        var entity =
            baseEntity with
            {
                Name = baseEntity.Name + "Updated",
                Description = baseEntity.Description + "Updated",
            };

        //Act
        ActieDbContextSUT.Activities.Update(entity);
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Activities.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task Delete_Activity_Deleted()
    {
        //Arrange
        var baseEntity = ActivitySeeds.ActivityEntityDelete;

        //Act
        ActieDbContextSUT.Activities.Remove(baseEntity);
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ActieDbContextSUT.Activities.AnyAsync(i => i.Id == baseEntity.Id));
    }

    [Fact]
    public async Task DeleteById_Activity_Deleted()
    {
        //Arrange
        var baseEntity = ActivitySeeds.ActivityEntityDelete;

        //Act
        ActieDbContextSUT.Remove(
            ActieDbContextSUT.Activities.Single(i => i.Id == baseEntity.Id));
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ActieDbContextSUT.Activities.AnyAsync(i => i.Id == baseEntity.Id));
    }
}
