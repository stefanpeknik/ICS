
using Actie.DAL.Entities;

namespace Actie.DAL.DTOs;
internal class ProjectEntityDTO : IEntityDTO<ProjectEntity>
{
    public void MapToExistingEntity(ProjectEntity existingEntity, ProjectEntity newEntity)
    {
        existingEntity.Name= newEntity.Name;
        existingEntity.Description= newEntity.Description;
    }
}
