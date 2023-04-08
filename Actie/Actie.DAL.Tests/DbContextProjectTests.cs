using Actie.Common.Tests;
using Actie.Common.Tests.Seeds;
using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Actie.DAL.Tests;

public class DbContextProjectTests : DbContextTestsBase
{
    public DbContextProjectTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_ProjectWithoutUsersNorActivities_Persisted()
    {
        // Arrange
        var entity = ProjectSeeds.EmptyProjectEntity with {Name = "Karate", Description = "Karate coaching sessions from Mr. Kim Un"};

        // Act
        ActieDbContextSUT.Projects.Add(entity);
        await ActieDbContextSUT.SaveChangesAsync();

        // Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntries = await dbx.Projects.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(expected: entity, actual: actualEntries);
    }

    [Fact]
    public async Task AddNew_ProjectWithActivity_Persisted()
    {
        //Arrange
        var entity = ProjectSeeds.EmptyProjectEntity with
        {
            Name = "Gym",
            Description = "Gym sessions under qualified trainer",
            Activities = new List<ActivityEntity> {
                ActivitySeeds.EmptyActivityEntity with
                {
                    Name = "Running",
                    Start = DateTime.Parse("2023-01-04 11:00 AM"),
                    End = DateTime.Parse("2023-01-04 11:15 AM"),
                    Type = "Indoor"
                },
                ActivitySeeds.EmptyActivityEntity with
                {
                    Name = "Bench",
                    Start = DateTime.Parse("2023-01-04 05:00 PM"),
                    End = DateTime.Parse("2023-01-04 05:30 PM"),
                    Type = "Indoor"
                },
            }
        };

        //Act
        ActieDbContextSUT.Projects.Add(entity);
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Projects
            .Include(i => i.Activities)
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task AddNew_ProjectWithUser_Persisted()
    {
        //Arrange
        var entity = ProjectSeeds.EmptyProjectEntity with
        {
            Name = "Gym",
            Description = "Gym sessions under qualified trainer",
            Users = new List<UserProjectEntity> {}
        };

        //Act
        ActieDbContextSUT.Projects.Add(entity);
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Projects
            .Include(i => i.Users)
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task GetById_Project()
    {
        //Act
        var entity = await ActieDbContextSUT.Projects
            .SingleAsync(i => i.Id == ProjectSeeds.ProjectEntity.Id);

        //Assert
        DeepAssert.Equal(ProjectSeeds.ProjectEntity with { Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserProjectEntity>() }, entity);
    }


    [Fact]
    public async Task Update_Project_Persisted()
    {
        //Arrange
        var baseEntity = ProjectSeeds.ProjectEntityUpdate;
        var entity =
            baseEntity with
            {
                Name = baseEntity.Name + "Updated",
                Description = baseEntity.Description + "Updated",
            };

        //Act
        ActieDbContextSUT.Projects.Update(entity);
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Projects.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task Delete_Project_Deleted()
    {
        //Arrange
        var baseEntity = ProjectSeeds.ProjectEntityDelete;

        //Act
        ActieDbContextSUT.Projects.Remove(baseEntity);
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ActieDbContextSUT.Projects.AnyAsync(i => i.Id == baseEntity.Id));
    }

    [Fact]
    public async Task DeleteById_Project_Deleted()
    {
        //Arrange
        var baseEntity = ProjectSeeds.ProjectEntityDelete;

        //Act
        ActieDbContextSUT.Remove(
            ActieDbContextSUT.Projects.Single(i => i.Id == baseEntity.Id));
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ActieDbContextSUT.Projects.AnyAsync(i => i.Id == baseEntity.Id));
    }
}
