// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Actie.DAL.Seeds;

public static class UserSeeds
{
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

    public static void LoadLists()
    {
        UserEntity.Activities.Add(ActivitySeeds.ActivityEntity1Proj1);
        UserEntity.Activities.Add(ActivitySeeds.ActivityEntity1Proj2);
        UserEntity.Activities.Add(ActivitySeeds.ActivityEntity2Proj2);

        UserEntity.Projects.Add(UserProjectSeeds.UserProjectEntity1);
        UserEntity.Projects.Add(UserProjectSeeds.UserProjectEntity2);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserEntity with { Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<UserProjectEntity>() }
        );
    }

}

