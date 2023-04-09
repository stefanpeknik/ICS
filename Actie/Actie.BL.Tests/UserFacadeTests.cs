
using Actie.BL.Facades;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
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
        var model = new UserDetailModel()
        {
            Id = Guid.Empty,
            Name = "Test",
            Surname = "Testovac",
        };
        var _ = await _userFacadeSUT.SaveAsync(model);
    }
}
