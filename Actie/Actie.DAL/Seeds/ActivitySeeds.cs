// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Actie.DAL.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity ActivityEntity1Proj1 = new()
    {
        Id = Guid.Parse(input: "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
        Name = "Running",
        Start = DateTime.Parse("2023-01-04 12:00 PM"),
        End = DateTime.Parse("2023-01-04 13:00 PM"),
        Rating = 6,
        Type = string.Empty,
        Description = "Outdoor",
        UserId = UserSeeds.UserEntity.Id,
        User = UserSeeds.UserEntity,
        ProjectId = ProjectSeeds.ProjectEntity1.Id,
        Project = ProjectSeeds.ProjectEntity1
    };

    public static readonly ActivityEntity ActivityEntity1Proj2 = new()
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
        ProjectId = ProjectSeeds.ProjectEntity2.Id,
        Project = ProjectSeeds.ProjectEntity2
    };

    public static readonly ActivityEntity ActivityEntity2Proj2 = new()
    {
        Id = Guid.Parse(input: "2408d4b4-0684-4f7b-962f-f2848ed6c880"),
        Name = "Swimming",
        Start = DateTime.Parse("2023-02-04 15:20 PM"),
        End = DateTime.Parse("2023-02-04 17:00 PM"),
        Rating = 9,
        Type = "Indoor",
        Description = null,
        UserId = UserSeeds.UserEntity.Id,
        User = UserSeeds.UserEntity,
        ProjectId = ProjectSeeds.ProjectEntity2.Id,
        Project = ProjectSeeds.ProjectEntity2
    };

    public static void LoadLists()
    {
        ActivityEntity1Proj2.Tags.Add(ActivityTagSeeds.ActivityTagEntity1);
        ActivityEntity2Proj2.Tags.Add(ActivityTagSeeds.ActivityTagEntity2);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            ActivityEntity1Proj1 with { User = null, Project = null },
            ActivityEntity1Proj2 with { User = null, Project = null },
            ActivityEntity2Proj2 with { User = null, Project = null }
        );
    }

}

