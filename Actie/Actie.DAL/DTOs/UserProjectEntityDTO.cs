
using Actie.DAL.Entities;

namespace Actie.DAL.DTOs;
internal class UserProjectEntityDTO : IEntityDTO<UserProjectEntity>
{
    public void MapToExistingEntity(UserProjectEntity existingEntity, UserProjectEntity newEntity)
    {
        existingEntity.ProjectId = newEntity.ProjectId;
        existingEntity.UserId = newEntity.UserId; 
    }
}
