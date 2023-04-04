using Actie.DAL.Entities;

namespace Actie.DAL.Mappers;

public class TagEntityMapper : IEntityMapper<TagEntity>
{
    public void MapToExistingEntity(TagEntity existingEntity, TagEntity newEntity)
    {
        existingEntity.Name = newEntity.Name;
        existingEntity.Description = newEntity.Description;
    }
}
