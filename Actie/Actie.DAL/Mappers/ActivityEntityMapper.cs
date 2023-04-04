using Actie.DAL.Entities;

namespace Actie.DAL.Mappers;

public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
{
    public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity)
    {
        existingEntity.Name = newEntity.Name;
        existingEntity.Start = newEntity.Start;
        existingEntity.End = newEntity.End;
        existingEntity.Rating = newEntity.Rating;
        existingEntity.Type = newEntity.Type;
        existingEntity.Description = newEntity.Description;
        existingEntity.ProjectId = newEntity.ProjectId;
        existingEntity.Project = newEntity.Project;
    }
}
