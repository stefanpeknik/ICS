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

    public static readonly ActivityEntity FastWalk = new()
    {
        Id = Guid.Parse(input: "89db49eb-5170-4085-8a0c-044cd53ce2ed"),
        Name = "FastWalk",
        Start = DateTime.Parse("2023-05-07 15:40 PM"),
        End = DateTime.Parse("2023-05-07 17:00 PM"),
        Rating = 10,
        Type = "Outdoor",
        Description = "This fastwalk was very fun. I totaled 7kms while listening to the new XX album. Really nice.",
        UserId = UserSeeds.Chados.Id,
        User = UserSeeds.Chados,
        ProjectId = ProjectSeeds.Muscles.Id,
        Project = ProjectSeeds.Muscles
    };

    public static readonly ActivityEntity RomWalk = new()
    {
        Id = Guid.Parse(input: "fe1b2999-9740-4e73-ae4a-5c660c001dca"),
        Name = "Romantic walk.",
        Start = DateTime.Parse("2023-05-08 20:40 PM"),
        End = DateTime.Parse("2023-05-08 21:30 PM"),
        Rating = 10,
        Type = "Outdoor",
        Description = "Romantic walk on the river edge. Great atm., we both liked it.",
        UserId = UserSeeds.Chados.Id,
        User = UserSeeds.Chados,
        ProjectId = ProjectSeeds.Wmn.Id,
        Project = ProjectSeeds.Wmn
    };

    public static readonly ActivityEntity Nnch = new()
    {
        Id = Guid.Parse(input: "7b611e29-e545-4c6f-a2aa-991a9e36207e"),
        Name = "Netflix n' chill.",
        Start = DateTime.Parse("2023-05-01 20:40 PM"),
        End = DateTime.Parse("2023-05-01 21:20 PM"),
        Rating = 7,
        Type = "Indoor",
        Description = null,
        UserId = UserSeeds.Chados.Id,
        User = UserSeeds.Chados,
        ProjectId = ProjectSeeds.Wmn.Id,
        Project = ProjectSeeds.Wmn
    };

    public static readonly ActivityEntity Gym = new()
    {
        Id = Guid.Parse(input: "fd56aa83-5cb4-47c5-b768-60d4789127d2"),
        Name = "Gym",
        Start = DateTime.Parse("2023-05-02 15:00 PM"),
        End = DateTime.Parse("2023-05-02 17:00 PM"),
        Rating = 5,
        Type = "Indoor",
        Description = "Normal gym session. Not great nor terrible.",
        UserId = UserSeeds.Chados.Id,
        User = UserSeeds.Chados,
        ProjectId = ProjectSeeds.Muscles.Id,
        Project = ProjectSeeds.Muscles
    };

    public static void LoadLists()
    {
        ActivityEntity1Proj2.Tags.Add(ActivityTagSeeds.ActivityTagEntity1);
        ActivityEntity2Proj2.Tags.Add(ActivityTagSeeds.ActivityTagEntity2);
        FastWalk.Tags.Add(ActivityTagSeeds.ActivityTagEntity3);
        RomWalk.Tags.Add(ActivityTagSeeds.ActivityTagEntity3);
        RomWalk.Tags.Add(ActivityTagSeeds.ActivityTagEntity4);
        Nnch.Tags.Add(ActivityTagSeeds.ActivityTagEntity3);
        Nnch.Tags.Add(ActivityTagSeeds.ActivityTagEntity4);
        Gym.Tags.Add(ActivityTagSeeds.ActivityTagEntity3);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            ActivityEntity1Proj1 with { Tags = Array.Empty<ActivityTagEntity>(), User = null, Project = null },
            ActivityEntity1Proj2 with { Tags = Array.Empty<ActivityTagEntity>(), User = null, Project = null },
            ActivityEntity2Proj2 with { Tags = Array.Empty<ActivityTagEntity>(), User = null, Project = null },
            FastWalk with { Tags = Array.Empty<ActivityTagEntity>(), User = null, Project = null },
            RomWalk with { Tags = Array.Empty<ActivityTagEntity>(), User = null, Project = null },
            Nnch with { Tags = Array.Empty<ActivityTagEntity>(), User = null, Project = null },
            Gym with { Tags = Array.Empty<ActivityTagEntity>(), User = null, Project = null }
        );
    }

}

