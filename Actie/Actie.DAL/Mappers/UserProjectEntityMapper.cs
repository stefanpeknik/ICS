using Actie.DAL.Entities;

namespace Actie.DAL.Mappers;

public class UserProjectEntityMapper : IEntityMapper<UserProjectEntity>
{
    public void MapToExistingEntity(UserProjectEntity existingEntity, UserProjectEntity newEntity)
    {
        existingEntity.UserId = newEntity.UserId;
        existingEntity.User = newEntity.User;
        existingEntity.ProjectId = newEntity.ProjectId;
        existingEntity.Project = newEntity.Project;
    }
}
