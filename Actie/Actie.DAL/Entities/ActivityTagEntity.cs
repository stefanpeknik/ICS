using Actie.DAL.Entities;

namespace Actie.DAT.Entities;

public class ActivityTagEntity : IEntity
{
    public required Guid Id { get; set; }

    public required ICollection<ActivityEntity> Activities { get; set; }
    public required ICollection<TagEntity> Tags { get; set; }

}
