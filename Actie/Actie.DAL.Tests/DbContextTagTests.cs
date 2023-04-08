using Actie.Common.Tests;
using Actie.Common.Tests.Seeds;
using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Actie.DAL.Tests;

public class DbContextTagTests : DbContextTestsBase
{
    public DbContextTagTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_TagWithoutActivities_Persisted()
    {
        // Arrange
        var entity = TagSeeds.EmptyTagEntity with {Name = "IDK", Description = "Ion Duncan Kick"};

        // Act
        ActieDbContextSUT.Tags.Add(entity);
        await ActieDbContextSUT.SaveChangesAsync();

        // Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntries = await dbx.Tags.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(expected: entity, actual: actualEntries);
    }

    [Fact]
    public async Task AddNew_TagWithActivity_Persisted()
    {
        //Arrange
        var entity = TagSeeds.EmptyTagEntity with
        {
            Name = "HLE",
            Description = "Hoping living ends",
            Activities = new List<ActivityTagEntity> {}
        };

        //Act
        ActieDbContextSUT.Tags.Add(entity);
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Tags
            .Include(i => i.Activities)
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }


    [Fact]
    public async Task GetById_Tag()
    {
        //Act
        var entity = await ActieDbContextSUT.Tags
            .SingleAsync(i => i.Id == TagSeeds.Training.Id);

        //Assert
        DeepAssert.Equal(TagSeeds.Training with { Activities = Array.Empty<ActivityTagEntity>()}, entity);
    }


    [Fact]
    public async Task Update_Tag_Persisted()
    {
        //Arrange
        var baseEntity = TagSeeds.TrainingUpdate;
        var entity =
            baseEntity with
            {
                Name = baseEntity.Name + "Updated",
                Description = baseEntity.Description + "Updated",
            };

        //Act
        ActieDbContextSUT.Tags.Update(entity);
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Tags.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task Delete_Tag_Deleted()
    {
        //Arrange
        var baseEntity = TagSeeds.TrainingDelete;

        //Act
        ActieDbContextSUT.Tags.Remove(baseEntity);
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ActieDbContextSUT.Tags.AnyAsync(i => i.Id == baseEntity.Id));
    }

    [Fact]
    public async Task DeleteById_Tag_Deleted()
    {
        //Arrange
        var baseEntity = TagSeeds.TrainingDelete;

        //Act
        ActieDbContextSUT.Remove(
            ActieDbContextSUT.Tags.Single(i => i.Id == baseEntity.Id));
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ActieDbContextSUT.Tags.AnyAsync(i => i.Id == baseEntity.Id));
    }
}
