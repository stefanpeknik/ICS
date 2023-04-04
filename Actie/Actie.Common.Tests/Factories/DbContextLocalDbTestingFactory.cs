using Actie.DAL;
using Microsoft.EntityFrameworkCore;

namespace Actie.Common.Tests.Factories;

public class DbContextLocalDbTestingFactory : IDbContextFactory<ActieDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextLocalDbTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }
    public ActieDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<ActieDbContext> builder = new();
        
        builder.UseSqlServer($"Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog = {_databaseName};MultipleActiveResultSets = True;Integrated Security = True; ");
        
        // contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // builder.EnableSensitiveDataLogging();
        
        return new ActieTestingDbContext(builder.Options, _seedTestingData);
    }
}
