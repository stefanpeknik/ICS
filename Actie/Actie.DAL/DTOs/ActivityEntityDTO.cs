

using Actie.DAL.Entities;

namespace Actie.DAL.DTOs;
internal class ActivityEntityDTO : IEntityDTO<ActivityEntity>
{
    public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity)
    {
        existingEntity.Name = newEntity.Name;
        existingEntity.Description = newEntity.Description;
        existingEntity.Start = newEntity.Start;
        existingEntity.End = newEntity.End;
        existingEntity.Rating= newEntity.Rating;
        existingEntity.Type = newEntity.Type;

        existingEntity.ProjectId= newEntity.ProjectId;

    }
}
