using Actie.DAT.Entities;
using Microsoft.EntityFrameworkCore;

namespace Actie.DAT;

public class ActieDbContext : DbContext
{
    private readonly bool _seedDemoData;

    public ActieDbContext(DbContextOptions contextOptions, bool seedDemoData = false)
        : base(contextOptions) =>
        _seedDemoData = seedDemoData;

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
    public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO
    }
}