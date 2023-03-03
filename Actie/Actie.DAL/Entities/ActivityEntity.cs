using Actie.DAL.Entities;

namespace Actie.DAT.Entities;

public class ActivityEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public int? Rating { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }

    public Guid ActivityTagId { get; set; }
    public ActivityTagEntity? ActivityTag { get; set; }

}
