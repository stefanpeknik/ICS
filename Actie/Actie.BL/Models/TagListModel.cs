namespace Actie.BL.Models;

public record TagListModel : ModelBase
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    public static TagListModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Description = string.Empty,
    };
}
