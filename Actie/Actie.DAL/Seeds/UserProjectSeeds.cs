// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Actie.DAL.Seeds;

public static class UserProjectSeeds
{
    public static readonly UserProjectEntity UserProjectEntity1 = new()
    {
        Id = Guid.Parse(input: "a2a42ed2-9d52-4ac9-ae7f-3373a29123b1"),
        UserId = UserSeeds.UserEntity.Id,
        User = UserSeeds.UserEntity,
        ProjectId = ProjectSeeds.ProjectEntity1.Id,
        Project = ProjectSeeds.ProjectEntity1,
    };

    public static readonly UserProjectEntity UserProjectEntity2 = new()
    {
        Id = Guid.Parse(input: "7ce427d6-633f-4d2c-bc32-55c83e013a01"),
        UserId = UserSeeds.UserEntity.Id,
        User = UserSeeds.UserEntity,
        ProjectId = ProjectSeeds.ProjectEntity2.Id,
        Project = ProjectSeeds.ProjectEntity2,
    };

    public static readonly UserProjectEntity UserProjectEntity3 = new()
    {
        Id = Guid.Parse(input: "234af95d-f481-43f0-bfb1-512364833011"),
        UserId = UserSeeds.Chados.Id,
        User = UserSeeds.Chados,
        ProjectId = ProjectSeeds.Muscles.Id,
        Project = ProjectSeeds.Muscles,
    };

    public static readonly UserProjectEntity UserProjectEntity4 = new()
    {
        Id = Guid.Parse(input: "24797d0d-7f2e-42e0-a3c4-ac60d2f5586b"),
        UserId = UserSeeds.Chados.Id,
        User = UserSeeds.Chados,
        ProjectId = ProjectSeeds.Wmn.Id,
        Project = ProjectSeeds.Wmn,
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProjectEntity>().HasData(
            UserProjectEntity1 with { User = null, Project = null },
            UserProjectEntity2 with { User = null, Project = null },
            UserProjectEntity3 with { User = null, Project = null },
            UserProjectEntity4 with { User = null, Project = null }
            );
    }

}

