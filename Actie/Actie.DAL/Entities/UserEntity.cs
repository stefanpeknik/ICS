namespace Actie.DAL.Entities;

public record UserEntity : IEntity
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? Photo { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public float? Weight { get; set; }
    public int? Height { get; set; }

    public ICollection<ActivityEntity> Activities { get; init; } = new List<ActivityEntity>();

    public ICollection<UserProjectEntity> Projects { get; init; } = new List<UserProjectEntity>();
}
