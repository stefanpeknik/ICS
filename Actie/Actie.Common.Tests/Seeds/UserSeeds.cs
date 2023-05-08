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

    public static readonly UserEntity UserForUserProjectEntityUpdate = UserEntity with { Id = Guid.Parse("b5476ecf-0010-49f5-887d-e54c168f47d9"), Projects = Array.Empty<UserProjectEntity>(), Activities = Array.Empty<ActivityEntity>()};
    public static readonly UserEntity UserForUserProjectEntityDelete = UserEntity with { Id = Guid.Parse("20cce012-1d55-48fa-beb2-f4453324234f"), Projects = new List<UserProjectEntity>(), Activities = Array.Empty<ActivityEntity>()};


    public static void LoadLists()
    {
        UserEntity.Activities.Add(ActivitySeeds.ActivityEntity);
        UserEntity.Activities.Add(ActivitySeeds.ActivityEntity1);
        UserEntity.Projects.Add(UserProjectSeeds.UserProjectEntity);

    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserEntity with {Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<UserProjectEntity>()},
            UserEntityWithNoActivitiesNorProjects,
            UserEntityUpdate,
            UserEntityDelete,
            UserForUserProjectEntityUpdate,
            UserForUserProjectEntityDelete with{Projects = Array.Empty<UserProjectEntity>()}
        );
    }
}
