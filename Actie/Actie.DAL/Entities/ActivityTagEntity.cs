namespace Actie.DAL.Entities;

public class ActivityTagEntity : IEntity
{
    public required Guid Id { get; set; }

    public required Guid ActivityId { get; set; }
    public required ActivityEntity Activity { get; set; }

    public required Guid TagId { get; set; }
    public required TagEntity Tag { get; set; }
}
