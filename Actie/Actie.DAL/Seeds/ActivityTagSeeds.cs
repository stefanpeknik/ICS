// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Actie.DAL.Seeds;

public static class ActivityTagSeeds
{
    public static readonly ActivityTagEntity ActivityTagEntity1 = new()
    {
        Id = Guid.Parse(input: "0d4fa150-ad80-4d46-a511-4c666166ec5e"),
        ActivityId = ActivitySeeds.ActivityEntity1Proj1.Id,
        TagId = TagSeeds.TagEntity1.Id,
        Activity = ActivitySeeds.ActivityEntity1Proj1,
        Tag = TagSeeds.TagEntity1,
    };

    public static readonly ActivityTagEntity ActivityTagEntity2 = new()
    {
        Id = Guid.Parse(input: "87833e66-05ba-4d6b-900b-fe5ace88dbd8"),
        ActivityId = ActivitySeeds.ActivityEntity1Proj2.Id,
        TagId = TagSeeds.TagEntity2.Id,
        Activity = ActivitySeeds.ActivityEntity1Proj2,
        Tag = TagSeeds.TagEntity2,
    };

    public static void Seed(this ModelBuilder modelBuilder) =>
    modelBuilder.Entity<ActivityTagEntity>().HasData(
            ActivityTagEntity1 with { Activity = null, Tag = null },
            ActivityTagEntity2 with { Activity = null, Tag = null }
        );
}
