using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Actie.DAL.Factories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ActieDbContext>
{
    public ActieDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ActieDbContext> builder = new();
            
        builder.UseSqlite($"Data Source=Actie;Cache=Shared");
            
        return new ActieDbContext(builder.Options);
    }
}
