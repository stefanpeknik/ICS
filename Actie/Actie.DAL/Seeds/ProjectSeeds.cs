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

    public static void LoadLists()
    {
        ProjectEntity1.Users.Add(UserProjectSeeds.UserProjectEntity1);
        ProjectEntity2.Users.Add(UserProjectSeeds.UserProjectEntity2);

        ProjectEntity1.Activities.Add(ActivitySeeds.ActivityEntity1Proj1);
        ProjectEntity2.Activities.Add(ActivitySeeds.ActivityEntity1Proj2);
        ProjectEntity2.Activities.Add(ActivitySeeds.ActivityEntity2Proj2);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>().HasData(
            ProjectEntity1 with { Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserProjectEntity>() },
            ProjectEntity2 with { Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserProjectEntity>() }
        );
    }

}

