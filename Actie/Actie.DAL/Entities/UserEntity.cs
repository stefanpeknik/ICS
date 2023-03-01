namespace Actie.DAT.Entities;

public class UserEntity : IEntity
{
    public Guid Id { get; set; }

    public required string Name { get; set; }
    public required string Surname { get; set; }
    public Uri? Photo { get; set; }
    public ICollection<ActivityEntity>? Activities { get; set; }
    public ICollection<ProjectEntity>? Projects { get; set; }

}