using System.Collections.ObjectModel;

namespace Actie.BL.Models;

public record ActivityDetailModel : ModelBase
{
    public required string Name { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required string Type { get; set; }
    public int? Rating { get; set; }
    public string? Description { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? UserId { get; set; }
    public ObservableCollection<ActivityTagListModel>? Tags { get; init; } = new();

    public static ActivityDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Start = DateTime.MinValue,
        End = DateTime.MinValue,
        Type = string.Empty,
        Rating = null,
        Description = string.Empty,
        ProjectId = Guid.Empty,
        UserId = Guid.Empty,
        Tags = new()
    };
}
