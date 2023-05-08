using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Mappers;

class ProjectModelMapper : ModelMapperBase<ProjectEntity, ProjectListModel, ProjectDetailModel>, IProjectModelMapper
{
    private readonly IActivityModelMapper _activityModelMapper;
    private readonly IUserProjectModelMapper _userProjectModelMapper;

    public ProjectModelMapper(IActivityModelMapper activityModelMapper, IUserProjectModelMapper userProjectModelMapper)
    {
        _activityModelMapper = activityModelMapper;
        _userProjectModelMapper = userProjectModelMapper;
    }


    public override ProjectListModel MapToListModel(ProjectEntity? entity)
        => entity is null
        ? ProjectListModel.Empty
        : new ProjectListModel
        {
            Id = entity.Id,
            Name = entity.Name
        };

    public override ProjectDetailModel MapToDetailModel(ProjectEntity? entity)
        => entity is null
            ? ProjectDetailModel.Empty
            : new ProjectDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Activities = _activityModelMapper.MapToListModel(entity.Activities).ToObservableCollection(),
                Users = _userProjectModelMapper.MapToListModel(entity.Users).ToObservableCollection()
            };

    public override ProjectEntity MapToEntity(ProjectDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description
        };
}
