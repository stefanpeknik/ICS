

using Actie.BL.Models;

namespace Actie.BL.Facades.Interfaces;
public interface IUserProjectFacade
{
    Task<UserProjectDetailModel> SaveAsync(UserProjectDetailModel model, Guid )
}
