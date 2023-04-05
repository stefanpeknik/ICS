// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Actie.Common.Tests.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity EmptyActivityEntity = new()
    {
        Id = default,
        Name = default!,
        Start = default,
        End = default,
        Rating = default,
        Type = default,
        Description = default,
    };

    public static readonly ActivityEntity ActivityEntity = new()
    {
        Id = Guid.Parse(input: "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
        Name = "Running",
        Start = DateTime.Parse("2023-01-04 12:00 PM"),
        End = DateTime.Parse("2023-01-04 13:00 PM"),
        Rating = 6,
        Type = null,
        Description = null,
    };

    public static readonly ActivityEntity ActivityEntity1 = new()
    {
        Id = Guid.Parse(input: "c2261147-0a6f-46c2-9b17-d5a20ce8c055"),
        Name = "Walking",
        Start = DateTime.Parse("2023-02-04 15:20 PM"),
        End = DateTime.Parse("2023-02-04 17:00 PM"),
        Rating = 9,
        Type = null,
        Description = null,
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly ActivityEntity ActivityEntityWithNoTags = ActivityEntity with { Id = Guid.Parse("98B7F7B6-0F51-43B3-B8C0-B5FCFFF6DC2E"), Tags = Array.Empty<ActivityTagEntity>() };
    public static readonly ActivityEntity ActivityEntityUpdate = ActivityEntity with { Id = Guid.Parse("0953F3CE-7B1A-48C1-9796-D2BAC7F67868"), Tags = Array.Empty<ActivityTagEntity>() };
    public static readonly ActivityEntity ActivityEntityDelete = ActivityEntity with { Id = Guid.Parse("5DCA4CEA-B8A8-4C86-A0B3-FFB78FBA1A09"), Tags = Array.Empty<ActivityTagEntity>() };

    public static readonly ActivityEntity ActivityForActivityTagEntityUpdate = ActivityEntity with { Id = Guid.Parse("4FD824C0-A7D1-48BA-8E7C-4F136CF8BF31"), Tags = Array.Empty<ActivityTagEntity>() };
    public static readonly ActivityEntity ActivityForActivityTagEntityDelete = ActivityEntity with { Id = Guid.Parse("F78ED923-E094-4016-9045-3F5BB7F2EB88"), Tags = new List<ActivityTagEntity>() };

    static ActivitySeeds()
    {
        ActivityEntity.Tags.Add(ActivityTagSeeds.ActivityTagEntity1);
        ActivityEntity.Tags.Add(ActivityTagSeeds.ActivityTagEntity2);

        ActivityEntity1.Tags.Add(ActivityTagSeeds.ActivityTagEntity2);

        ActivityForActivityTagEntityDelete.Tags.Add(ActivityTagSeeds.ActivityTagEntityDelete);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            ActivityEntity with { Tags = Array.Empty<ActivityTagEntity>() },
            ActivityEntity1 with { Tags = Array.Empty<ActivityTagEntity>() },
            ActivityEntityWithNoTags,
            ActivityEntityUpdate,
            ActivityEntityDelete,
            ActivityForActivityTagEntityUpdate,
            ActivityForActivityTagEntityDelete with { Tags = Array.Empty<ActivityTagEntity>()}
        );
    }
}
