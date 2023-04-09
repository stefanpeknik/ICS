using Actie.DAL;
using Microsoft.EntityFrameworkCore;

namespace Actie.Common.Tests.Factories;

public class DbContextSqLiteTestingFactory : IDbContextFactory<ActieDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextSqLiteTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }
    public ActieDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<ActieDbContext> builder = new();
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");
        builder.EnableDetailedErrors(detailedErrorsEnabled: true);
        builder.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);

        // contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // builder.EnableSensitiveDataLogging();
        
        return new ActieTestingDbContext(builder.Options, _seedTestingData);
    }
}
