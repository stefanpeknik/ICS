namespace Actie.BL.Models;

public record ActivityTagListModel : ModelBase
{
    public required Guid? TagId { get; set; }
    public required Guid? ActivityId { get; set; }

    public static ActivityTagListModel Empty => new()
    {
        ActivityId= Guid.Empty,
        TagId = Guid.Empty
    };
}
