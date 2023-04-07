using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Mappers.Interfaces;
public interface IUserModelMapper : IModelMapper<UserEntity, UserListModel, UserDetailModel>
{
}
