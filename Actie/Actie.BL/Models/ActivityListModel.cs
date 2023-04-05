namespace Actie.BL.Models;
public record ActivityListModel : ModelBase
{
    public required string Name { get; set; }
    public string? Type { get; set; }

    public static ActivityListModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Type = string.Empty,
    };
}
