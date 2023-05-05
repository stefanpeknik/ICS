
using Actie.BL.Facades;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using Actie.Common.Tests;
using Actie.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Actie.BL.Tests;
public sealed class UserFacadeTests : FacadeTestsBase
{
    private readonly IUserFacade _userFacadeSUT;

    public UserFacadeTests(ITestOutputHelper output) : base(output)
    {
        _userFacadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        //Arrange
        var model = new UserDetailModel()
        {
            Id = Guid.Empty,
            Name = "Test",
            Surname = "Testovac",
        };
        //Act & Assert
        var _ = await _userFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task Add_NewUser()
    {
        //Arrange
        var user = new UserDetailModel()
        {
            Id = Guid.Empty,
            Name = "Josef",
            Surname = "Chroustal",
            Age = 1,
            
        };
        //Act
        user = await _userFacadeSUT.SaveAsync(user);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var user_from_db = await dbxAssert.Users.SingleAsync(i => i.Id == user.Id);
        DeepAssert.Equal(user, UserModelMapper.MapToDetailModel(user_from_db));
    }

    [Fact]
    public async Task GetAll_Single_SeededUser()
    {
        var users = await _userFacadeSUT.GetAsync();
        var user =  users.SingleOrDefault(i => i.Id == UserSeeds.UserEntity.Id);
        DeepAssert.Equal(UserModelMapper.MapToListModel(UserSeeds.UserEntity), user);
    }

    [Fact]
    public async Task GetById_SeededUserEntity()
    {
        var user = await _userFacadeSUT.GetAsync(UserSeeds.UserEntity.Id);

        DeepAssert.Equal(UserModelMapper.MapToDetailModel(UserSeeds.UserEntity), user);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var user = await _userFacadeSUT.GetAsync(UserSeeds.EmptyUserEntity.Id);

        Assert.Null(user);
    }
}
