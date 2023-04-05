namespace Actie.DAL.Entities;

public record TagEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<ActivityTagEntity> Activities { get; init; } = new List<ActivityTagEntity>();
}
