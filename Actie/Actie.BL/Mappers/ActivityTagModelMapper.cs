using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;


namespace Actie.BL.Mappers;

public class ActivityTagModelMapper : ModelMapperBase<ActivityTagEntity, ActivityTagListModel, ActivityTagDetailModel>,
    IActivityTagModelMapper
{
    public override ActivityTagListModel MapToListModel(ActivityTagEntity? entity)
        => entity is null
            ? ActivityTagListModel.Empty
            : new ActivityTagListModel
            {
                Id = entity.Id,
                TagId = entity.TagId,
                ActivityId = entity.ActivityId
            };

    public override ActivityTagDetailModel MapToDetailModel(ActivityTagEntity? entity)
        => entity is null
            ? ActivityTagDetailModel.Empty
            : new ActivityTagDetailModel
            {
                Id = entity.Id,
                TagId = entity.TagId,
                ActivityId = entity.ActivityId
            };

    public override ActivityTagEntity MapToEntity(ActivityTagDetailModel model)
        => new()
        {
            Id = model.Id,
            TagId = model.TagId,
            ActivityId = model.ActivityId,
        };
}
