
using Actie.DAL.Entities;

namespace Actie.DAL.DTOs;
internal class ActivityTagEntityDTO : IEntityDTO<ActivityTagEntity>
{
    public void MapToExistingEntity(ActivityTagEntity existingEntity, ActivityTagEntity newEntity)
    {
        existingEntity.ActivityId = newEntity.ActivityId;
        existingEntity.TagId = newEntity.TagId;
    }
}
