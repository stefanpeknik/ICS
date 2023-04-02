
using Actie.DAL.Entities;

namespace Actie.DAL.DTOs;
public interface IEntityDTO<in TEntity>
    where TEntity : IEntity
{
    void MapToExistingEntity(TEntity existingEntity, TEntity newEntity);
}
