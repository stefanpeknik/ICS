using System.Collections.ObjectModel;
using Actie.DAL.Entities;

namespace Actie.BL.Models;

public record TagDetailModel : ModelBase
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    public ObservableCollection<ActivityTagListModel> Activities { get; init; } = new();

    public static TagDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Description = string.Empty,
    };
}
