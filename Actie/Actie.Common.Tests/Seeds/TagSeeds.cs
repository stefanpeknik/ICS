using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Actie.Common.Tests.Seeds;

public static class TagSeeds
{
    public static readonly TagEntity EmptyTagEntity = new()
    {
        Id = default,
        Name = default!,
        Description = default!
    };

    public static readonly TagEntity Training = new()
    {
        Id = Guid.Parse(input: "06a8a2cf-ea03-4095-a3e4-aa0291fe9c75"),
        Name = "Training",
        Description = "conditioning training"
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly TagEntity TrainingUpdate = Training with { Id = Guid.Parse("143332B9-080E-4953-AEA5-BEF64679B052") };
    public static readonly TagEntity TrainingDelete = Training with { Id = Guid.Parse("274D0CC9-A948-4818-AADB-A8B4C0506619") };

    public static TagEntity TagEntity1 = new()
    {
        Id = Guid.Parse(input: "df935095-8709-4040-a2bb-b6f97cb416dc"),
        Name = "Training seeded training 1",
        Description = "Training seeded training 1 description"
    };

    public static TagEntity TagEntity2 = new()
    {
        Id = Guid.Parse(input: "23b3902d-7d4f-4213-9cf0-112348f56238"),
        Name = "Training seeded training 2",
        Description = "Training seeded training 2 description"
    };

    public static void LoadLists()
    {
        TagEntity1.Activities.Add(ActivityTagSeeds.ActivityTagEntity1);
        TagEntity2.Activities.Add(ActivityTagSeeds.ActivityTagEntity2);
        TagEntity2.Activities.Add(ActivityTagSeeds.ActivityTagEntity3);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TagEntity>().HasData(
            TagEntity1 with { Activities = Array.Empty<ActivityTagEntity>() },
            TagEntity2 with { Activities = Array.Empty<ActivityTagEntity>() },
            Training,
            TrainingUpdate,
            TrainingDelete
        );
    }
}
