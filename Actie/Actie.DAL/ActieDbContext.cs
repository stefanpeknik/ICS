using Actie.DAL.Entities;
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

        // TODO
    }
}
