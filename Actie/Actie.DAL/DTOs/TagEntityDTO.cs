
using Actie.DAL.Entities;

namespace Actie.DAL.DTOs;
public class TagEntityDTO : IEntityDTO<TagEntity>
{
    public void MapToExistingEntity(TagEntity existingEntity, TagEntity newEntity)
    {
        existingEntity.Name = newEntity.Name;
        existingEntity.Description = newEntity.Description;
    }
}
