using Actie.DAT.Entities;



namespace Actie.DAL.Entities
{
    public class TagEntity : IEntity
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<ActivityTagEntity> Activities { get; set; }
    }
}
