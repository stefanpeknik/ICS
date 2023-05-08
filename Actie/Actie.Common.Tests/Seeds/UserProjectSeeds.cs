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
    

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProjectEntity>().HasData(
            UserProjectEntity with { User = null, Project = null }
        );
    }
}
