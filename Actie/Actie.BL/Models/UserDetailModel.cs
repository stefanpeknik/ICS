using System.Collections.ObjectModel;
using Actie.DAL.Entities;

namespace Actie.BL.Models;

public record UserDetailModel : ModelBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? Photo { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public float? Weight { get; set; }
    public int? Height { get; set; }
    public ObservableCollection<ActivityListModel> Activities { get; init; } = new();
    public ObservableCollection<UserProjectListModel> Projects { get; init; } = new();

    public static UserDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Surname = string.Empty,
        Photo = null,
        Age = null,
        Gender = string.Empty,
        Weight = null,
        Height = null
    };
}
