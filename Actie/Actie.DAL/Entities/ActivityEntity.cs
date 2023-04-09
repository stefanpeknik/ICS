namespace Actie.DAL.Entities;

public record ActivityEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required string Type { get; set; }
    public int? Rating { get; set; }
    public string? Description { get; set; }

    public ICollection<ActivityTagEntity> Tags { get; init; } = new List<ActivityTagEntity>();

    public Guid? ProjectId { get; set; }
    public ProjectEntity? Project { get; set; }

    public Guid? UserId { get; set; }
    public UserEntity? User { get; set; }
}
