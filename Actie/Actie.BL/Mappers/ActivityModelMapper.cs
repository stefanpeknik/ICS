using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Mappers;

class ActivityModelMapper : ModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>, IActivityModelMapper
{
    private readonly IActivityTagModelMapper _activityTagModelMapper;

    public ActivityModelMapper(IActivityTagModelMapper activityTagModelMapper)
    {
        _activityTagModelMapper = activityTagModelMapper;
    }

    public override ActivityListModel MapToListModel(ActivityEntity? entity)
        => entity is null
            ? ActivityListModel.Empty
            : new ActivityListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Start = entity.Start,
                End = entity.End,
                Type = entity.Type,
                Tags = _activityTagModelMapper.MapToListModel(entity.Tags).ToObservableCollection()

            };

    public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity)
        => entity is null
            ? ActivityDetailModel.Empty
            : new ActivityDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Start = entity.Start,
                End = entity.End,
                Type = entity.Type,
                Rating = entity.Rating,
                Description = entity.Description,
                ProjectId = entity.ProjectId,
                UserId = entity.UserId,
                Tags = _activityTagModelMapper.MapToListModel(entity.Tags).ToObservableCollection()
            };

    public override ActivityEntity MapToEntity(ActivityDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Start = model.Start,
            End = model.End,
            Type = model.Type,
            Rating = model.Rating,
            Description = model.Description,
            ProjectId = model.ProjectId,
            UserId = model.UserId
        };
}
