namespace Actie.DAL.Entities;

public class UserProjectEntity : IEntity
{
    public required Guid Id { get; set; }

    public required Guid UserId { get; set; }
    public required UserEntity? User { get; set; }

    public required Guid ProjectId { get; set; }
    public required ProjectEntity? Project { get; set; }
}
