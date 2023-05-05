using Actie.BL.Facades;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Mappers;
using Actie.BL.Models;
using Actie.Common.Tests;
using Actie.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Actie.BL.Tests;
public sealed class ActivityFacadeTests : FacadeTestsBase
{
    private readonly IActivityFacade _activityFacadeSUT;

    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {
        _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        // Arange
        var model = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = @"Activity 1",
            Type = @"Activity 1 type",
            Start = DateTime.Now,
            End = DateTime.Now,
        };

        // Act & Assert
        var _ = await _activityFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetAll_Single_Seeded()
    {
        // Arrange & Act
        var activities = await _activityFacadeSUT.GetAsync();
        var activity = activities.Single(i => i.Id == ActivitySeeds.ActivityEntity.Id);

        // Assert
        DeepAssert.Equal(ActivityModelMapper.MapToListModel(ActivitySeeds.ActivityEntity), activity);
    }

    [Fact]
    public async Task GetById_Seeded()
    {
        // Arrange & Act
        var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.ActivityEntity.Id);

        // Assert
        DeepAssert.Equal(ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityEntity), activity);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        // Arrange & Act
        var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.EmptyActivityEntity.Id);

        // Assert
        Assert.Null(activity);
    }

    //[Fact]
    //public async Task Delete_ActivityUsedByUser_Throws()
    //{
    //    // Act & Assert
    //    await Assert.ThrowsAsync<InvalidOperationException>(async() => await _activityFacadeSUT.DeleteAsync(ActivitySeeds.ActivityEntity1.Id));
    //}

    [Fact]
    public async Task Seeded_DeleteById_Deleted()
    {
        // Arrange
        await _activityFacadeSUT.DeleteAsync(ActivitySeeds.ActivityEntity.Id);

        // Act
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

        // Assert
        Assert.False(await dbxAssert.Activities.AnyAsync(i => i.Id == ActivitySeeds.ActivityEntity.Id));
    }

    [Fact]
    public async Task NewActivity_AddOrUpdate_ActivityAdded()
    {
        //Arrange
        var activity = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Run",
            Type = "Sprint",
            Start = DateTime.Now,
            End = DateTime.Now,
        };

        //Act
        activity = await _activityFacadeSUT.SaveAsync(activity);
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var activityFromDb = await dbx.Activities.SingleAsync(i => i.Id == activity.Id);

        //Assert
        Assert.Equal(activity.Id, ActivityModelMapper.MapToDetailModel(activityFromDb).Id);
    }

    [Fact]
    public async Task Seeded_AddOrUpdate_ActivityUpdated()
    {
        //Arrange
        var activity = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = @"Jumping",
            Type = @"Jumping Jacks",
            Start = DateTime.Now,
            End = DateTime.Now,
        };

        activity.Name += " updated";
        activity.Type += " updated";

        //Act
        activity = await _activityFacadeSUT.SaveAsync(activity);
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var ingredientFromDb = await dbxAssert.Activities.SingleAsync(i => i.Id == activity.Id);

        // Assert
        DeepAssert.Equal(activity.Id, ActivityModelMapper.MapToDetailModel(ingredientFromDb).Id);
    }
}
