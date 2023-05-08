

using System.Collections.ObjectModel;
using Actie.BL.Facades;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using Actie.Common.Tests;
using Actie.Common.Tests.Seeds;
using Actie.DAL.Entities;
using Xunit.Abstractions;

namespace Actie.BL.Tests;
public sealed class ProjectFacadeTests : FacadeTestsBase
{
    private readonly IProjectFacade _projectFacadeSUT;
    public ProjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _projectFacadeSUT = new ProjectFacade(UnitOfWorkFactory, UserProjectModelMapper, ProjectModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingActivity_Throws()
    {
        //Arrange
        var project = new ProjectDetailModel()
        {
            Name = "Test",
            Description = "Prvni aktivita",
            Activities = new ObservableCollection<ActivityListModel>()
            {
                new()
                {
                    Id = Guid.Empty,
                    Name= "Nocni klus",
                    Type = "behani",
                    Start= DateTimeFiller.StartDateTime,
                    End= DateTimeFiller.EndDateTime,
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
            Activities = new ObservableCollection<ActivityListModel>()
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

    [Fact]
    public async Task GetById_FromSeeded_DoesNotThrowAndEqualsSeeded()
    {
        //Arrange
        var detailModel = ProjectModelMapper.MapToDetailModel(ProjectSeeds.ProjectEntity);

        //Act
        var returnedModel = await _projectFacadeSUT.GetAsync(detailModel.Id);

        //Assert
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task GetAll_FromSeeded_DoesNotThrowAndContainsSeeded()
    {
        //Arrange
        var listModel = ProjectModelMapper.MapToListModel(ProjectSeeds.ProjectEntity);

        //Act
        var returnedModel = await _projectFacadeSUT.GetAsync();

        //Assert
        Assert.Contains(listModel, returnedModel);
    }

    [Fact]
    public async Task Update_FromSeeded_DoesNotThrow()
    {
        //Arrange
        var detailModel = ProjectModelMapper.MapToDetailModel(ProjectSeeds.ProjectEntity);
        detailModel.Name = "Changed name";

        //Act & Assert
        await _projectFacadeSUT.SaveAsync(detailModel with { Activities = null!,  Users = null! });
    }

    [Fact]
    public async Task Update_Name_FromSeeded_CheckUpdated()
    {
        //Arrange
        var detailModel = ProjectModelMapper.MapToDetailModel(ProjectSeeds.ProjectEntity);
        detailModel.Name = "Changed name 1";

        //Act
        await _projectFacadeSUT.SaveAsync(detailModel with { Activities = null!, Users = null! });

        //Assert
        var returnedModel = await _projectFacadeSUT.GetAsync(detailModel.Id);
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task Update_RemoveActivities_FromSeeded_CheckNotUpdated()
    {
        //Arrange
        var detailModel = ProjectModelMapper.MapToDetailModel(ProjectSeeds.ProjectEntity);
        detailModel.Activities.Clear();
        detailModel.Users.Clear();

        //Act
        await _projectFacadeSUT.SaveAsync(detailModel); //detailModel ma USERA!!!!

        //Assert
        var returnedModel = await _projectFacadeSUT.GetAsync(detailModel.Id);
        DeepAssert.Equal(ProjectModelMapper.MapToDetailModel(ProjectSeeds.ProjectEntity), returnedModel);
    }

    //need fix

    [Fact]
    public async Task Update_RemoveOneOfActivities_FromSeeded_CheckUpdated()
    {
        //Arrange
        var detailModel = ProjectModelMapper.MapToDetailModel(ProjectSeeds.ProjectEntity);
        detailModel.Activities.Remove(detailModel.Activities.First());

        //Act
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _projectFacadeSUT.SaveAsync(detailModel));

        //Assert
        var returnedModel = await _projectFacadeSUT.GetAsync(detailModel.Id);
        DeepAssert.Equal(ProjectModelMapper.MapToDetailModel(ProjectSeeds.ProjectEntity), returnedModel);
    }

    [Fact]
    public async Task DeleteById_FromSeeded_DoesNotThrow()
    {
        //Act
        await _projectFacadeSUT.DeleteAsync(ProjectSeeds.ProjectEntity.Id);

        //Assert
        var returnedModel = await _projectFacadeSUT.GetAsync(ProjectSeeds.ProjectEntity.Id);
        Assert.Null(returnedModel);
    }
}
