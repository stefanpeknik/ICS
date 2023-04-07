using Actie.Common.Tests.Seeds;
using Actie.DAL;
using Microsoft.EntityFrameworkCore;

namespace Actie.Common.Tests;

public class ActieTestingDbContext : ActieDbContext
{
    private readonly bool _seedTestingData;

    public ActieTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
        : base(contextOptions, seedDemoData:false)
    {
        _seedTestingData = seedTestingData;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (_seedTestingData)
        {
            ActivitySeeds.Seed((modelBuilder));
            ActivityTagSeeds.Seed((modelBuilder));
            ProjectSeeds.Seed((modelBuilder));
            TagSeeds.Seed(modelBuilder);
            UserProjectSeeds.Seed((modelBuilder));
            UserSeeds.Seed(modelBuilder);
        }
    }
}
