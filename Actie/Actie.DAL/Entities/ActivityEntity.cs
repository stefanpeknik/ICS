namespace Actie.DAT.Entities;

public class ActivityEntity : IEntity
{
    public Guid Id { get; set; }
        
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public enum ActivityType { }
    public ActivityType Type { get; set; }
    public string? Description { get; set; }

}