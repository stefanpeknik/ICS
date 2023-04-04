using Actie.DAL.Entities;

namespace Actie.DAL.Mappers;

public class UserEntityMapper : IEntityMapper<UserEntity>
{
    public void MapToExistingEntity(UserEntity existingEntity, UserEntity newEntity)
    {
        existingEntity.Name = newEntity.Name;
        existingEntity.Surname = newEntity.Surname;
        existingEntity.Photo = newEntity.Photo;
        existingEntity.Age = newEntity.Age;
        existingEntity.Gender = newEntity.Gender;
        existingEntity.Weight = newEntity.Weight;
        existingEntity.Height = newEntity.Height;
    }
}
