using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Actie.Common.Tests.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity EmptyActivityEntity = new()
    {
        Id = default,
        Name = default!,
        Start = DateTimeFiller.StartDateTime,
        End = DateTimeFiller.EndDateTime,
        Rating = default,
        Type = default!,
        Description = default,
        UserId = default,
        User = default,
        ProjectId = default,
        Project = default
    };

    public static readonly ActivityEntity ActivityEntity = new()
    {
        Id = Guid.Parse(input: "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
        Name = "Running",
        Start = DateTime.Parse("2023-01-04 12:00 PM"),
        End = DateTime.Parse("2023-01-04 13:00 PM"),
        Rating = 6,
        Type = string.Empty,
        Description = null,
        UserId = UserSeeds.UserEntity.Id,
        User = UserSeeds.UserEntity,
        ProjectId = ProjectSeeds.ProjectEntity.Id,
        Project = ProjectSeeds.ProjectEntity
    };

    public static readonly ActivityEntity ActivityEntity1 = new()
    {
        Id = Guid.Parse(input: "c2261147-0a6f-46c2-9b17-d5a20ce8c055"),
        Name = "Walking",
        Start = DateTime.Parse("2023-02-04 15:20 PM"),
        End = DateTime.Parse("2023-02-04 17:00 PM"),
        Rating = 9,
        Type = "Outdoor",
        Description = null,
        UserId = UserSeeds.UserEntity.Id,
        User = UserSeeds.UserEntity,
        ProjectId = ProjectSeeds.ProjectEntity.Id,
        Project = ProjectSeeds.ProjectEntity
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly ActivityEntity ActivityEntityWithNoTags = ActivityEntity with { Id = Guid.Parse("98B7F7B6-0F51-43B3-B8C0-B5FCFFF6DC2E"), Tags = Array.Empty<ActivityTagEntity>(), UserId = default, ProjectId = default, User = null, Project = null };
    public static readonly ActivityEntity ActivityEntityUpdate = ActivityEntity with { Id = Guid.Parse("0953F3CE-7B1A-48C1-9796-D2BAC7F67868"), Tags = Array.Empty<ActivityTagEntity>(), UserId = default, ProjectId = default, User = null, Project = null };
    public static readonly ActivityEntity ActivityEntityDelete = ActivityEntity with { Id = Guid.Parse("5DCA4CEA-B8A8-4C86-A0B3-FFB78FBA1A09"), Tags = Array.Empty<ActivityTagEntity>(), UserId = default, ProjectId = default, User = null, Project = null };

    public static readonly ActivityEntity ActivityForActivityTagEntityUpdate = ActivityEntity with { Id = Guid.Parse("4FD824C0-A7D1-48BA-8E7C-4F136CF8BF31"), Tags = Array.Empty<ActivityTagEntity>(), UserId = default, ProjectId = default, User = null, Project = null };
    public static readonly ActivityEntity ActivityForActivityTagEntityDelete = ActivityEntity with { Id = Guid.Parse("F78ED923-E094-4016-9045-3F5BB7F2EB88"), Tags = new List<ActivityTagEntity>(), UserId = default, ProjectId = default, User = null, Project = null };

    public static void LoadLists()
    {
        ActivityEntity.Tags.Add(ActivityTagSeeds.ActivityTagEntity1);
        ActivityEntity.Tags.Add(ActivityTagSeeds.ActivityTagEntity2);

        ActivityEntity1.Tags.Add(ActivityTagSeeds.ActivityTagEntity3);

        ActivityForActivityTagEntityDelete.Tags.Add(ActivityTagSeeds.ActivityTagEntityDelete);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            ActivityEntity with { Tags = Array.Empty<ActivityTagEntity>(), User = null, Project = null },
            ActivityEntity1 with { Tags = Array.Empty<ActivityTagEntity>(), User = null, Project = null },
            ActivityEntityWithNoTags,
            ActivityEntityUpdate,
            ActivityEntityDelete,
            ActivityForActivityTagEntityUpdate,
            ActivityForActivityTagEntityDelete with { Tags = Array.Empty<ActivityTagEntity>()}
        );
    }
}
