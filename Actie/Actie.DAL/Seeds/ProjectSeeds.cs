// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Actie.DAL.Seeds;

public static class ProjectSeeds
{
    public static readonly ProjectEntity ProjectEntity1 = new()
    {
        Id = Guid.Parse(input: "ed42df43-897d-426e-903e-2cf080dca58d"),
        Name = "Getting fit",
        Description = "Getting fit to feel better.",
    };

    public static readonly ProjectEntity ProjectEntity2 = new()
    {
        Id = Guid.Parse(input: "2a10946d-87b0-4aba-bd16-95a0494756f1"),
        Name = "Runnin in the 90's",
        Description = "Forest be runnin and Forest will be runnin."
    };

    public static readonly ProjectEntity Muscles = new()
    {
        Id = Guid.Parse(input: "7a4dfc84-6b04-4bde-becc-df071ed13412"),
        Name = "Getting big muscles.",
        Description = "Yep."
    };

    public static readonly ProjectEntity Wmn = new()
    {
        Id = Guid.Parse(input: "6930caff-b0f9-4c21-83b1-25c59d2340c6"),
        Name = "Relationship stuff.",
        Description = "Time spend with my gf."
    };

    public static void LoadLists()
    {
        ProjectEntity1.Users.Add(UserProjectSeeds.UserProjectEntity1);
        ProjectEntity2.Users.Add(UserProjectSeeds.UserProjectEntity2);
        Muscles.Users.Add(UserProjectSeeds.UserProjectEntity3);
        Wmn.Users.Add(UserProjectSeeds.UserProjectEntity4);

        ProjectEntity1.Activities.Add(ActivitySeeds.ActivityEntity1Proj1);
        ProjectEntity2.Activities.Add(ActivitySeeds.ActivityEntity1Proj2);
        ProjectEntity2.Activities.Add(ActivitySeeds.ActivityEntity2Proj2);
        Muscles.Activities.Add(ActivitySeeds.FastWalk);
        Wmn.Activities.Add(ActivitySeeds.RomWalk);
        Wmn.Activities.Add(ActivitySeeds.Nnch);
        Muscles.Activities.Add(ActivitySeeds.Gym);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>().HasData(
            ProjectEntity1 with { Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserProjectEntity>() },
            ProjectEntity2 with { Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserProjectEntity>() },
            Muscles with { Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserProjectEntity>() },
            Wmn with { Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserProjectEntity>() }
        );
    }

}

