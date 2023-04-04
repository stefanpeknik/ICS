using Actie.DAL;
using Microsoft.EntityFrameworkCore;

namespace Actie.Common.Tests.Factories;

public class DbContextTestingInMemoryFactory : IDbContextFactory<ActieDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextTestingInMemoryFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }

    public ActieDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<ActieDbContext> contextOptionsBuilder = new();
        contextOptionsBuilder.UseInMemoryDatabase(_databaseName);

        // contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // builder.EnableSensitiveDataLogging();

        return new ActieTestingDbContext(contextOptionsBuilder.Options, _seedTestingData);
    }
}
