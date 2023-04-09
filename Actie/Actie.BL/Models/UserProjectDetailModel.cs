namespace Actie.BL.Models;

public record UserProjectDetailModel : ModelBase
{
    public required Guid? UserId { get; set; }

    public required Guid? ProjectId { get; set; }

    public static UserProjectDetailModel Empty => new()
    {
        UserId = Guid.Empty,
        ProjectId = Guid.Empty,
    };
}
