using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Actie.Common.Tests.Seeds;

public static class ActivityTagSeeds
{
    public static readonly ActivityTagEntity EmptyActivityTagEntity = new()
    {
        Id = default,
        ActivityId = default,
        TagId = default,
        Activity = default,
        Tag = default,
    };

    public static readonly ActivityTagEntity ActivityTagEntity1 = new()
    {
        Id = Guid.Parse(input: "0d4fa150-ad80-4d46-a511-4c666166ec5e"),
        ActivityId = ActivitySeeds.ActivityEntity.Id,
        TagId = TagSeeds.TagEntity1.Id,
        Activity = ActivitySeeds.ActivityEntity,
        Tag = TagSeeds.TagEntity1,
    };

    public static readonly ActivityTagEntity ActivityTagEntity2 = new()
    {
        Id = Guid.Parse(input: "87833e66-05ba-4d6b-900b-fe5ace88dbd8"),
        ActivityId = ActivitySeeds.ActivityEntity.Id,
        TagId = TagSeeds.TagEntity2.Id,
        Activity = ActivitySeeds.ActivityEntity,
        Tag = TagSeeds.TagEntity2,
    };

    public static readonly ActivityTagEntity ActivityTagEntity3 = new()
    {
        Id = Guid.Parse(input: "868bd6c9-7b6f-4201-849d-d1ae52caa03b"),
        ActivityId = ActivitySeeds.ActivityEntity1.Id,
        TagId = TagSeeds.TagEntity2.Id,
        Activity = ActivitySeeds.ActivityEntity1,
        Tag = TagSeeds.TagEntity2,
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly ActivityTagEntity ActivityTagEntityUpdate = ActivityTagEntity1 with { Id = Guid.Parse("A2E6849D-A158-4436-980C-7FC26B60C674"), Tag = null, Activity = null, ActivityId = ActivitySeeds.ActivityForActivityTagEntityUpdate.Id };
    public static readonly ActivityTagEntity ActivityTagEntityDelete = ActivityTagEntity2 with { Id = Guid.Parse("30872EFF-CED4-4F2B-89DB-0EE83A74D279"), Tag = null, Activity = null, ActivityId = ActivitySeeds.ActivityForActivityTagEntityDelete.Id };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityTagEntity>().HasData(
            ActivityTagEntity1 with { Activity = null, Tag = null},
            ActivityTagEntity2 with { Activity = null, Tag = null},
            ActivityTagEntity3 with { Activity = null, Tag = null},
            ActivityTagEntityUpdate,
            ActivityTagEntityDelete
        );
    }
}
