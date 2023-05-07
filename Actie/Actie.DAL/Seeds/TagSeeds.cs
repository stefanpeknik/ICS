// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Actie.DAL.Seeds;

public static class TagSeeds
{
    public static TagEntity TagEntity1 = new()
    {
        Id = Guid.Parse(input: "df935095-8709-4040-a2bb-b6f97cb416dc"),
        Name = "Training seeded training 1",
        Description = "Training seeded training 1 description"
    };

    public static TagEntity TagEntity2 = new()
    {
        Id = Guid.Parse(input: "23b3902d-7d4f-4213-9cf0-112348f56238"),
        Name = "Training seeded training 2",
        Description = "Training seeded training 2 description"
    };

    public static TagEntity Cut = new()
    {
        Id = Guid.Parse(input: "e507bdb7-b16e-45fe-8148-28d041a4465b"),
        Name = "Cut",
        Description = "Cut phase."
    };

    public static TagEntity Rom = new()
    {
        Id = Guid.Parse(input: "decced87-f00b-4828-95f6-4884b9c85c5b"),
        Name = "Romantic stuff",
        Description = "--"
    };

    public static void LoadLists()
    {
       TagEntity1.Activities.Add(ActivityTagSeeds.ActivityTagEntity1);
       TagEntity2.Activities.Add(ActivityTagSeeds.ActivityTagEntity2);
       Cut.Activities.Add(ActivityTagSeeds.ActivityTagEntity3);
       Rom.Activities.Add(ActivityTagSeeds.ActivityTagEntity4);
    }
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TagEntity>().HasData(
            TagEntity1 with { Activities = Array.Empty<ActivityTagEntity>() },
            TagEntity2 with { Activities = Array.Empty<ActivityTagEntity>() },
            Cut with { Activities = Array.Empty<ActivityTagEntity>() },
            Rom with { Activities = Array.Empty<ActivityTagEntity>() }
            );
    }
}
