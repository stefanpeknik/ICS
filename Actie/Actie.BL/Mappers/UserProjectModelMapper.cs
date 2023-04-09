using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Mappers;

public class UserProjectModelMapper : ModelMapperBase<UserProjectEntity, UserProjectListModel, UserProjectDetailModel>, IUserProjectModelMapper
{
    public override UserProjectListModel MapToListModel(UserProjectEntity? entity)
        => entity is null
            ? UserProjectListModel.Empty
            : new UserProjectListModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ProjectId = entity.ProjectId
            };

    public override UserProjectDetailModel MapToDetailModel(UserProjectEntity? entity)
        => entity is null
            ? UserProjectDetailModel.Empty
            : new UserProjectDetailModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ProjectId = entity.ProjectId
            };

    public override UserProjectEntity MapToEntity(UserProjectDetailModel model)
        => new()
        {
            Id = model.Id,
            UserId = model.UserId,
            ProjectId = model.ProjectId
        };
}
