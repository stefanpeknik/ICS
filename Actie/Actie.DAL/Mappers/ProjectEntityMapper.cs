using Actie.DAL.Entities;

namespace Actie.DAL.Mappers;

public class ProjectEntityMapper : IEntityMapper<ProjectEntity>
{
    public void MapToExistingEntity(ProjectEntity existingEntity, ProjectEntity newEntity)
    {
        existingEntity.Name = newEntity.Name;
        existingEntity.Description = newEntity.Description;
    }
}
