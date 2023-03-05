using System.Xml.Serialization;

namespace Actie.DAL.Entities;

public class ProjectEntity : IEntity
{
    public required Guid Id { get; set; }
        
    public required string Name { get; set; }
    public required string Description { get; set; }
    public ICollection<ActivityEntity>? Activities { get; set; }

    public ICollection<UserProjectEntity>? Users { get; set; }
}
