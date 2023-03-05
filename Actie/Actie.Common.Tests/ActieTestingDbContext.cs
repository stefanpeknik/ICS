using Actie.DAL;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Common.Tests;

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
            // TODO
        }
    }
}
