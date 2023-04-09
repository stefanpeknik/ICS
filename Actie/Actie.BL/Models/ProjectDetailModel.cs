using System.Collections.ObjectModel;
using Actie.DAL.Entities;

namespace Actie.BL.Models;

public record ProjectDetailModel : ModelBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public ObservableCollection<ActivityListModel> Activities { get; init; } = new ();
    public ObservableCollection<UserProjectListModel> Users { get; init; } = new();

    public static ProjectDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Description = string.Empty,

    };
}
