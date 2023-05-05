using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Actie.Common.Tests.Seeds;

public static class ProjectSeeds
{
    public static readonly ProjectEntity EmptyProjectEntity = new()
    {
        Id = default,
        Name = default!,
        Description = default!,
    };

    public static readonly ProjectEntity ProjectEntity = new()
    {
        Id = Guid.Parse(input: "ed42df43-897d-426e-903e-2cf080dca58d"),
        Name = "Getting fit",
        Description = "Getting fit to feel better.",
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly ProjectEntity ProjectEntityWithNoActivities = ProjectEntity with { Id = Guid.Parse("f57c2abe-9b9a-40e0-8b68-f833fb6f6801"), Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserProjectEntity>()};
    public static readonly ProjectEntity ProjectEntityUpdate = ProjectEntity with { Id = Guid.Parse("28d3add7-7b57-437e-8748-d82319fb2aae"), Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserProjectEntity>() };
    public static readonly ProjectEntity ProjectEntityDelete = ProjectEntity with { Id = Guid.Parse("86d802de-9b67-4f83-bde2-d50c60f658d6"), Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserProjectEntity>() };

    public static void LoadLists()
    {
        ProjectEntity.Activities.Add(ActivitySeeds.ActivityEntity);
        ProjectEntity.Activities.Add(ActivitySeeds.ActivityEntity1);

        ProjectEntity.Users.Add(UserProjectSeeds.UserProjectEntity);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>().HasData(
            ProjectEntity with { Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserProjectEntity>()},
            ProjectEntityWithNoActivities with { Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserProjectEntity>() },
            ProjectEntityUpdate with { Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserProjectEntity>() },
            ProjectEntityDelete with { Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserProjectEntity>() }
        );
    }
}
