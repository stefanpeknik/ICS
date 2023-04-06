// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Actie.Common.Tests.Seeds;

public static class UserProjectSeeds
{
    public static readonly UserProjectEntity EmptyUserProjectEntity = new()
    {
        Id = default,
        UserId = default,
        User = default,
        ProjectId = default,
        Project = default,
    };

    public static readonly UserProjectEntity UserProjectEntity = new()
    {
        Id = Guid.Parse(input: "a2a42ed2-9d52-4ac9-ae7f-3373a29123b1"),
        UserId = UserSeeds.UserEntity.Id,
        User = UserSeeds.UserEntity,
        ProjectId = ProjectSeeds.ProjectEntity.Id,
        Project = ProjectSeeds.ProjectEntity,
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly UserProjectEntity UserProjectEntityUpdate = UserProjectEntity with { Id = Guid.Parse("892d59d6-3eea-47c3-9028-ca57536724b2"), Project = null, User = null, UserId = UserSeeds.UserForUserProjectEntityUpdate.Id };
    public static readonly UserProjectEntity UserProjectEntityDelete = UserProjectEntity with { Id = Guid.Parse("62599455-7a10-4181-92a9-0c0322dd57cb"), Project = null, User = null, UserId = UserSeeds.UserForUserProjectEntityDelete.Id };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProjectEntity>().HasData(
            UserProjectEntity with { User = null, Project = null },
            UserProjectEntityUpdate,
            UserProjectEntityDelete
        );
    }
}
