using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Mappers;

class UserProjectModelMapper : ModelMapperBase<UserProjectEntity, UserProjectListModel, UserProjectDetailModel>, IUserProjectModelMapper
{
    public override UserProjectListModel MapToListModel(UserProjectEntity? entity) => throw new NotImplementedException();

    public override UserProjectDetailModel MapToDetailModel(UserProjectEntity entity) => throw new NotImplementedException();

    public override UserProjectEntity MapToEntity(UserProjectDetailModel model) => throw new NotImplementedException();
}
