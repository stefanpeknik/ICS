

using System.Collections.ObjectModel;
using Actie.BL.Facades;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using Actie.Common.Tests.Seeds;
using Actie.DAL.Entities;
using Xunit.Abstractions;

namespace Actie.BL.Tests;
public sealed class ProjectFacadeTests : FacadeTestsBase
{
    private readonly IProjectFacade _projectFacadeSUT;
    public ProjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _projectFacadeSUT = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingActivity_Throws()
    {
        //Arrange
        var project = new ProjectDetailModel()
        {
            Name = "Test",
            Description = "Prvni aktivita",
            Activities = new ObservableCollection<ActivityEntity>()
            {
                new()
                {
                    Id = Guid.Empty,
                    Name= "Nocni klus",
                    Type = "behani",
                    Start= DateTime.Now,
                    End= DateTime.Now,
                }
            }
        };
        //Act & Assert
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _projectFacadeSUT.SaveAsync(project));
    }

    [Fact]
    public async Task Create_WithActivity_DoesNotThrowAndEqualsCreated()
    {
        //Arrange
        var model = new ProjectDetailModel()
        {
            Name = "Treninkovy denik",
            Description = "Popis",
            Activities = new ObservableCollection<ActivityEntity>()
            {
                new ()
                {
                    Id = ActivitySeeds.ActivityEntity.Id,
                    Name = ActivitySeeds.ActivityEntity.Name,
                    Start= ActivitySeeds.ActivityEntity.Start,
                    End= ActivitySeeds.ActivityEntity.End,
                    Type = ActivitySeeds.ActivityEntity.Type
                    

                }
            },
        };

        //Act && Assert
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _projectFacadeSUT.SaveAsync(model));

    }
}
