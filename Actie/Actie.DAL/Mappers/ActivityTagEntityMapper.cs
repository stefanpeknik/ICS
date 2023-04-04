using Actie.DAL.Entities;

namespace Actie.DAL.Mappers;

public class ActivityTagEntityMapper : IEntityMapper<ActivityTagEntity>
{
    public void MapToExistingEntity(ActivityTagEntity existingEntity, ActivityTagEntity newEntity)
    {
        existingEntity.ActivityId = newEntity.ActivityId;
        existingEntity.Activity = newEntity.Activity;
        existingEntity.TagId = newEntity.TagId;
        existingEntity.Tag = newEntity.Tag;
    }
}
