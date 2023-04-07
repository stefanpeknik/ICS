using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Mappers;

class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel>, IUserModelMapper
{
    public override UserListModel MapToListModel(UserEntity? entity) => throw new NotImplementedException();

    public override UserDetailModel MapToDetailModel(UserEntity entity) => throw new NotImplementedException();

    public override UserEntity MapToEntity(UserDetailModel model) => throw new NotImplementedException();
}
