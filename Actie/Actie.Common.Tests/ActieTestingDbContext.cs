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
            TagSeeds.Seed(modelBuilder);

            UserSeeds.Seed(modelBuilder);

            ProjectSeeds.Seed(modelBuilder);

            ActivitySeeds.Seed(modelBuilder);

            ActivityTagSeeds.Seed(modelBuilder);

            UserProjectSeeds.Seed(modelBuilder);
        }
    }
}
