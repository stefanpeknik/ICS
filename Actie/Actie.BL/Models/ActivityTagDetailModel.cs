namespace Actie.BL.Models;

public record ActivityTagDetailModel : ModelBase
{
    public required Guid? TagId { get; set; }
    public required Guid? ActivityId { get; set; }

    public static ActivityTagDetailModel Empty => new()
    {
        ActivityId = Guid.Empty,
        TagId = Guid.Empty
    };
}
