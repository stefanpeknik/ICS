

using Actie.BL.Facades;
using Actie.BL.Facades.Interfaces;
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

public sealed class TagFacadeTests : FacadeTestsBase
{
    private readonly ITagFacade _tagFacadeSUT;

    public TagFacadeTests(ITestOutputHelper output) : base(output)
    {
        _tagFacadeSUT = new TagFacade(UnitOfWorkFactory, TagModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        // Arrange
        var model = new TagDetailModel()
        {
            Id = Guid.Empty,
            Name = "Bodybuilding",
            Description = "Building muscles.",
        };

        // Act & Assert
        var _ = await _tagFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetAll_Single_SeededTraining()
    {
        // Arrange & Act
        var tags = await _tagFacadeSUT.GetAsync();
        var tag = tags.Single(i => i.Id == TagSeeds.Training.Id);

        // Assert
        DeepAssert.Equal(TagModelMapper.MapToListModel(TagSeeds.Training), tag);
    }

    [Fact]
    public async Task GetById_SeededTraining()
    {
        // Arrange & Act
        var tag = await _tagFacadeSUT.GetAsync(TagSeeds.Training.Id);

        // Assert
        DeepAssert.Equal(TagModelMapper.MapToDetailModel(TagSeeds.Training), tag);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        // Arrange & Act
        var tag = await _tagFacadeSUT.GetAsync(TagSeeds.EmptyTagEntity.Id);

        // Assert
        Assert.Null(tag);
    }

    [Fact]
    public async Task SeededTraining_DeleteById_Deleted()
    {
        // Arrange & Act
        await _tagFacadeSUT.DeleteAsync(TagSeeds.Training.Id);

        // Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Tags.AnyAsync(i => i.Id == TagSeeds.Training.Id));
    }

    [Fact]
    public async Task NewTag_InsertOrUpdate_TagAdded()
    {
        // Arrange
        var tag = new TagDetailModel()
        {
            Id = Guid.Empty,
            Name = "Bodybuilding",
            Description = "Gym - all body workout.",
        };

        // Act
        tag = await _tagFacadeSUT.SaveAsync(tag);

        // Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var tagFromDb = await dbxAssert.Tags.SingleAsync(i => i.Id == tag.Id);
        DeepAssert.Equal(tag, TagModelMapper.MapToDetailModel(tagFromDb));
    }


    [Fact]
    public async Task SeededTraining_InsertOrUpdate_TagUpdated()
    {
        // Arrange
        var tag = new TagDetailModel()
        {
            Id = TagSeeds.Training.Id,
            Name = TagSeeds.Training.Name,
            Description = TagSeeds.Training.Description
        };
        tag.Name += "updated";
        tag.Description += "updated";

        // Act
        await _tagFacadeSUT.SaveAsync(tag);

        // Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var tagFromDb = await dbxAssert.Tags.SingleAsync(i => i.Id == tag.Id);
        DeepAssert.Equal(tag, TagModelMapper.MapToDetailModel(tagFromDb));
    }
}
