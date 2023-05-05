using Actie.Common.Tests.Seeds;
using Actie.DAL.Entities;
using Actie.DAL.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Actie.DAL;

public class ActieDbContext : DbContext
{
    private readonly bool _seedDemoData;

    public ActieDbContext(DbContextOptions contextOptions, bool seedDemoData = false)
        : base(contextOptions) =>
        _seedDemoData = seedDemoData;

    public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
    public DbSet<ActivityTagEntity> ActivitiesTags => Set<ActivityTagEntity>();
    public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
    public DbSet<TagEntity> Tags => Set<TagEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<UserProjectEntity> UsersProjects => Set<UserProjectEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Activity
        modelBuilder.Entity<ActivityEntity>()
            .HasOne(a => a.Project)
            .WithMany(p => p.Activities)
            .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<ActivityEntity>()
            .HasOne(a => a.User)
            .WithMany(u => u.Activities)
            .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<ActivityEntity>()
            .HasMany(a => a.Tags)
            .WithOne(at => at.Activity)
            .OnDelete(DeleteBehavior.Cascade);

        // Project
        modelBuilder.Entity<ProjectEntity>()
            .HasMany(p => p.Activities)
            .WithOne(a => a.Project)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ProjectEntity>()
            .HasMany(p => p.Users)
            .WithOne(up => up.Project)
            .OnDelete(DeleteBehavior.Cascade);

        // User
        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.Activities)
            .WithOne(a => a.User)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.Projects)
            .WithOne(up => up.User)
            .OnDelete(DeleteBehavior.Cascade);

        // UserProject
        modelBuilder.Entity<UserProjectEntity>()
            .HasOne(up => up.User)
            .WithMany(u => u.Projects)
            .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<UserProjectEntity>()
            .HasOne(up => up.Project)
            .WithMany(p => p.Users)
            .OnDelete(DeleteBehavior.Cascade);

        // Tag
        modelBuilder.Entity<TagEntity>()
            .HasMany(t => t.Activities)
            .WithOne(at => at.Tag)
            .OnDelete(DeleteBehavior.Cascade);

        // ActivityTag
        modelBuilder.Entity<ActivityTagEntity>()
            .HasOne(at => at.Activity)
            .WithMany(a => a.Tags)
            .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<ActivityTagEntity>()
            .HasOne(at => at.Tag)
            .WithMany(t => t.Activities)
            .OnDelete(DeleteBehavior.SetNull);

        if (_seedDemoData)
        {
            SeedsInit.LoadLists();

            TagSeeds.Seed(modelBuilder);

            UserSeeds.Seed(modelBuilder);

            ProjectSeeds.Seed(modelBuilder);

            ActivitySeeds.Seed(modelBuilder);

            ActivityTagSeeds.Seed(modelBuilder);

            UserProjectSeeds.Seed(modelBuilder);
        }
    }
}
