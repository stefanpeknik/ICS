using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Facades.Interfaces;
public interface IUserFacade : IFacade<UserEntity, UserListModel, UserDetailModel>
{
}
