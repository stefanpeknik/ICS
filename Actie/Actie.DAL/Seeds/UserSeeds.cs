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

    public static readonly UserEntity UserEntity1 = new()
    {
        Id = Guid.Parse(input: "b39c4c7f-beaa-42dc-b756-66fd9c1e6bc2"),
        Name = "Brano",
        Surname = "Chorko",
        Photo = null,
        Age = 56,
        Gender = "Branikar",
        Weight = 100,
        Height = 150,
    };

    public static readonly UserEntity Chados = new()
    {
        Id = Guid.Parse(input: "c5e7d0ca-e936-403c-8565-e7f72d0825db"),
        Name = "Gigga",
        Surname = "Chaddo",
        Photo = null,
        Age = 31,
        Gender = "AlphaMale",
        Weight = 110,
        Height = 170,
    };

    public static void LoadLists()
    {
        UserEntity.Activities.Add(ActivitySeeds.ActivityEntity1Proj1);
        UserEntity.Activities.Add(ActivitySeeds.ActivityEntity1Proj2);
        UserEntity.Activities.Add(ActivitySeeds.ActivityEntity2Proj2);
        Chados.Activities.Add(ActivitySeeds.FastWalk);
        Chados.Activities.Add(ActivitySeeds.RomWalk);
        Chados.Activities.Add(ActivitySeeds.Nnch);
        Chados.Activities.Add(ActivitySeeds.Gym);

        UserEntity.Projects.Add(UserProjectSeeds.UserProjectEntity1);
        UserEntity.Projects.Add(UserProjectSeeds.UserProjectEntity2);
        Chados.Projects.Add(UserProjectSeeds.UserProjectEntity3);
        Chados.Projects.Add(UserProjectSeeds.UserProjectEntity4);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserEntity with { Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<UserProjectEntity>() },
            UserEntity1 with { Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<UserProjectEntity>() },
            Chados with { Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<UserProjectEntity>() }
        );
    }

}
