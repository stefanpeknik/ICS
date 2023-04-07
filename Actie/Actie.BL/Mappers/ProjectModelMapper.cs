using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Mappers;

class ProjectModelMapper : ModelMapperBase<ProjectEntity, ProjectListModel, ProjectDetailModel>, IProjectModelMapper
{
    public override ProjectListModel MapToListModel(ProjectEntity? entity) => throw new NotImplementedException();

    public override ProjectDetailModel MapToDetailModel(ProjectEntity entity) => throw new NotImplementedException();

    public override ProjectEntity MapToEntity(ProjectDetailModel model) => throw new NotImplementedException();
}
