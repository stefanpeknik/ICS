// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Diagnostics;
using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Actie.Common.Tests.Seeds;
public static class UserSeeds
{
    public static readonly UserEntity EmptyUserEntity = new()
    {
        Id = default,
        Name = default!,
        Surname = default!,
        Photo = default,
        Age = default,
        Gender = default,
        Weight = default,
        Height = default,
    };

    public static readonly UserEntity UserEntity = new()
    {
        Id = Guid.Parse(input: "302a58da-89e8-4fca-b651-3466ee8cd201"),
        Name = "Slavo",
        Surname = "Balko",
        Photo = null,
        Age = 21,
        Gender = "male",
        Weight = 70,
        Height = 170,
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly UserEntity UserEntityWithNoActivitiesNorProjects = UserEntity with { Id = Guid.Parse("9573ef6a-bdfb-4d87-bb3b-416ec3962a8a"), Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<UserProjectEntity>()};
    public static readonly UserEntity UserEntityUpdate = UserEntity with { Id = Guid.Parse("1d82c88e-6839-4be3-b271-393331222e94"), Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<UserProjectEntity>() };
    public static readonly UserEntity UserEntityDelete = UserEntity with { Id = Guid.Parse("2868b4fe-c861-4c69-a1bb-5bb8900f34cb"), Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<UserProjectEntity>() };

    // public static readonly UserEntity UserForUserProjectEntityUpdate = UserEntity with { Id = Guid.Parse("1a2c1aad-a4c0-4562-b4b8-f36ee41a495b"), TODO Tags = Array.Empty<ActivityTagEntity>() };
    // public static readonly UserEntity UserForUserProjectEntityDelete = UserEntity with { Id = Guid.Parse("1a2c1aad-a4c0-4562-b4b8-f36ee41a495b"), TODO Tags = new List<ActivityTagEntity>() };

    static UserSeeds()
    {
        UserEntity.Activities.Add(ActivitySeeds.ActivityEntity);
        UserEntity.Activities.Add(ActivitySeeds.ActivityEntity1);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserEntity with { Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<UserProjectEntity>() },
            UserEntityWithNoActivitiesNorProjects,
            UserEntityUpdate,
            UserEntityDelete
        );
    }
}
