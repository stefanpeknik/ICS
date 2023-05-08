using Actie.BL.Facades;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Mappers;
using Actie.BL.Models;
using Actie.Common.Tests;
using Actie.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
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
        // Arrange
        var model = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = @"Activity 1",
            Type = @"Activity 1 type",
            Start = DateTimeFiller.StartDateTime,
            End = DateTimeFiller.EndDateTime,
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
        DeepAssert.Equal(ActivityModelMapper.MapToListModel(ActivitySeeds.ActivityEntity).Id, activity.Id);
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
            Start = DateTimeFiller.StartDateTime,
            End = DateTimeFiller.EndDateTime,
        };

        //Act
        activity = await _activityFacadeSUT.SaveAsync(activity);
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var activityFromDb = await dbx.Activities.SingleAsync(i => i.Id == activity.Id);

        //Assert
        DeepAssert.Equal(activity, ActivityModelMapper.MapToDetailModel(activityFromDb));
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
            Start = DateTimeFiller.StartDateTime,
            End = DateTimeFiller.EndDateTime,
        };

        activity.Name += " updated";
        activity.Type += " updated";

        //Act
        activity = await _activityFacadeSUT.SaveAsync(activity);
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var ingredientFromDb = await dbxAssert.Activities.SingleAsync(i => i.Id == activity.Id);

        // Assert
        DeepAssert.Equal(activity, ActivityModelMapper.MapToDetailModel(ingredientFromDb));
    }

    [Fact]
    public async Task GetFilteredBeforeOrAfter_GetFilteredByNonExistentUser_EmptyEnumerable()
    {
        // Arrange
        var badId = Guid.Empty;

        // Act
        var filtered = await _activityFacadeSUT.GetFilteredBeforeOrAfterDateTime(badId);

        // Assert
        if (filtered != null)
        {
            Assert.Empty(filtered);
        }
    }

    [Fact]
    public async Task GetFilteredBeforeOrAfter_GetFilteredByUserId_ActivitiesOfUser()
    {
        // Arrange
        var user = UserSeeds.UserEntity;

        // Act
        var filtered = await _activityFacadeSUT.GetFilteredBeforeOrAfterDateTime(user.Id);

        // Assert
        DeepAssert.Equal(filtered, ActivityModelMapper.MapToListModel(user.Activities));
    }

    [Fact]
    public async Task GetFilteredBeforeOrAfter_GetFilteredByUserIdAndAfter_ActivitiesOfUserAfter()
    {
        // Arrange
        var after = DateTime.Parse("2023-01-20 00:00 AM");
        var user = UserSeeds.UserEntity;

        // Act
        var filtered = await _activityFacadeSUT.GetFilteredBeforeOrAfterDateTime(user.Id, after);

        // Assert
        DeepAssert.Equal(filtered, ActivityModelMapper.MapToListModel(user.Activities.Where(a => a.Start > after)));
    }

    [Fact]
    public async Task GetFilteredBeforeOrAfter_GetFilteredByUserIdAndBefore_ActivitiesOfUserBefore()
    {
        // Arrange
        var before = DateTime.Parse("2023-01-20 00:00 AM");
        var user = UserSeeds.UserEntity;

        // Act
        var filtered = await _activityFacadeSUT.GetFilteredBeforeOrAfterDateTime(user.Id, startsBefore: before);

        // Assert
        DeepAssert.Equal(filtered, ActivityModelMapper.MapToListModel(user.Activities.Where(a => a.Start < before)));
    }

    [Fact]
    public async Task GetFilteredBeforeOrAfter_GetFilteredByUserIdAndInterval_ActivitiesOfUserInInterval()
    {
        // Arrange
        var after = DateTime.Parse("2022-12-20 00:00 AM");
        var before = DateTime.Parse("2023-01-20 00:00 AM");
        var user = UserSeeds.UserEntity;

        // Act
        var filtered = await _activityFacadeSUT.GetFilteredBeforeOrAfterDateTime(user.Id, startsAfter: after, startsBefore: before);

        // Assert
        DeepAssert.Equal(filtered, ActivityModelMapper.MapToListModel(user.Activities.Where(a => a.Start < before && a.Start > after)));
    }

    [Fact]
    public async Task GetFilteredPreciseTime_GetFilteredByUserId_ActivitiesOfUser()
    {
        // Arrange
        var user = UserSeeds.UserEntity;

        // Act
        var filtered = await _activityFacadeSUT.GetFilteredPreciseDateTime(user.Id);

        // Assert
        DeepAssert.Equal(filtered, ActivityModelMapper.MapToListModel(user.Activities));
    }

    [Fact]
    public async Task GetFilteredPreciseTime_GetFilteredByNonExistentUser_EmptyEnumerable()
    {
        // Arrange
        var badId = Guid.Empty;

        // Act
        var filtered = await _activityFacadeSUT.GetFilteredPreciseDateTime(badId);

        // Assert
        if (filtered != null)
        {
            Assert.Empty(filtered);
        }
    }

    [Fact]
    public async Task GetFilteredPreciseTime_GetFilteredByUserIdAndInStart_ActivitiesOfUserInStart()
    {
        // Arrange
        var startsIn = DateTime.Parse("2023-01-04 12:00 PM");
        var user = UserSeeds.UserEntity;

        // Act
        var filtered = await _activityFacadeSUT.GetFilteredPreciseDateTime(user.Id, startsIn: startsIn);

        // Assert
        DeepAssert.Equal(filtered, ActivityModelMapper.MapToListModel(user.Activities.Where(a => a.Start == startsIn)));
    }

    [Fact]
    public async Task GetFilteredPreciseTime_GetFilteredByUserIdAndInEnd_ActivitiesOfUserInEnd()
    {
        // Arrange
        var endsIn = DateTime.Parse("2023-01-04 01:00 PM");
        var user = UserSeeds.UserEntity;

        // Act
        var filtered = await _activityFacadeSUT.GetFilteredPreciseDateTime(user.Id, endsIn: endsIn);

        // Assert
        DeepAssert.Equal(filtered, ActivityModelMapper.MapToListModel(user.Activities.Where(a => a.End == endsIn)));
    }

    [Fact]
    public async Task GetFilteredPreciseTime_GetFilteredByUserIdAndInStartAndInEnd_ActivitiesOfUserInStartAndInEnd()
    {
        // Arrange
        var startsIn = DateTime.Parse("2023-01-04 12:00 PM");
        var endsIn = DateTime.Parse("2023-01-04 01:00 PM");
        var user = UserSeeds.UserEntity;

        // Act
        var filtered = await _activityFacadeSUT.GetFilteredPreciseDateTime(user.Id, startsIn: startsIn, endsIn: endsIn);

        // Assert
        DeepAssert.Equal(filtered, ActivityModelMapper.MapToListModel(user.Activities.Where(a => a.Start == startsIn && a.End == endsIn)));
    }
}
