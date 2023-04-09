using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Mappers;

class TagModelMapper : ModelMapperBase<TagEntity, TagListModel, TagDetailModel>, ITagModelMapper
{
    private readonly IActivityTagModelMapper _activityTagModelMapper;

    public TagModelMapper(IActivityTagModelMapper activityTagModelMapper)
    {
        _activityTagModelMapper = activityTagModelMapper;
    }

    public override TagListModel MapToListModel(TagEntity? entity)
        => entity is null
            ? TagListModel.Empty
            : new TagListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };

    public override TagDetailModel MapToDetailModel(TagEntity? entity)
        => entity is null
            ? TagDetailModel.Empty
            : new TagDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Activities = _activityTagModelMapper.MapToListModel(entity.Activities).ToObservableCollection()
            };

    public override TagEntity MapToEntity(TagDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description
        };
}
