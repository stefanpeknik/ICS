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

    [Fact]
    public async Task AddNew_UserWithActivity_Persisted()
    {
        //Arrange
        var entity = UserSeeds.EmptyUserEntity with
        {
            Name = "Worldbreaker",
            Surname = "Sion",
            Activities = new List<ActivityEntity> {
                ActivitySeeds.EmptyActivityEntity with
                {
                    Name = "Dopolední běh",
                    Start = DateTime.Parse("2023-01-04 11:00 AM"),
                    End = DateTime.Parse("2023-01-04 11:15 AM"),
                    Type = "Outdoor"
                },
                ActivitySeeds.EmptyActivityEntity with
                {
                    Name = "Odpolední plavání",
                    Start = DateTime.Parse("2023-01-04 05:00 PM"),
                    End = DateTime.Parse("2023-01-04 05:30 PM"),
                    Type = "Indoor"
                },
            }
        };

        //Act
        ActieDbContextSUT.Users.Add(entity);
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users
            .Include(i => i.Activities)
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task GetById_User()
    {
        //Act
        var entity = await ActieDbContextSUT.Users
            .SingleAsync(i => i.Id == UserSeeds.UserEntity.Id);

        //Assert
        DeepAssert.Equal(UserSeeds.UserEntity with { Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<UserProjectEntity>()}, entity);
    }

    

    [Fact]
    public async Task Update_User_Persisted()
    {
        //Arrange
        var baseEntity = UserSeeds.UserEntityUpdate;
        var entity =
            baseEntity with
            {
                Name = baseEntity.Name + "Updated",
                Surname = baseEntity.Surname + "Updated",
            };

        //Act
        ActieDbContextSUT.Users.Update(entity);
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task Delete_User_Deleted()
    {
        //Arrange
        var baseEntity = UserSeeds.UserEntityDelete;

        //Act
        ActieDbContextSUT.Users.Remove(baseEntity);
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ActieDbContextSUT.Users.AnyAsync(i => i.Id == baseEntity.Id));
    }

    [Fact]
    public async Task DeleteById_User_Deleted()
    {
        //Arrange
        var baseEntity = UserSeeds.UserEntityDelete;

        //Act
        ActieDbContextSUT.Remove(
            ActieDbContextSUT.Users.Single(i => i.Id == baseEntity.Id));
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ActieDbContextSUT.Users.AnyAsync(i => i.Id == baseEntity.Id));
    }
}
