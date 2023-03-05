namespace Actie.DAL.Entities;

public class UserEntity : IEntity
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }
    public required string Surname { get; set; }
    public Uri? Photo { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public float? Weight { get; set; }
    public int? Height { get; set; }

    public ICollection<ActivityEntity>? Activities { get; set; }

    public ICollection<UserProjectEntity>? Projects { get; set; }
}
