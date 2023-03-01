using Actie.DAT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actie.DAL.Factories
{
    public class DbContextSQLiteFactory
    {
        public class DbContextSqLiteFactory : IDbContextFactory<ActieDbContext>
        {
            private readonly string _databaseName;
            private readonly bool _seedTestingData;

            public DbContextSqLiteFactory(string databaseName, bool seedTestingData = false)
            {
                _databaseName = databaseName;
                _seedTestingData = seedTestingData;
            }

            public ActieDbContext CreateDbContext()
            {
                DbContextOptionsBuilder<ActieDbContext> builder = new();

                ////May be helpful for ad-hoc testing, not drop in replacement, needs some more configuration.
                //builder.UseSqlite($"Data Source =:memory:;");
                builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");

                ////Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
                //builder.EnableSensitiveDataLogging();
                //builder.LogTo(Console.WriteLine); 

                return new ActieDbContext(builder.Options, _seedTestingData);
            }
        }
    }
}
