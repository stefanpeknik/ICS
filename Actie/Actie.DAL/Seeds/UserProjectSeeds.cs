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

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProjectEntity>().HasData(
            UserProjectEntity1 with { User = null, Project = null },
            UserProjectEntity2 with { User = null, Project = null }
            );
    }

}

