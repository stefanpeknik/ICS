using Microsoft.EntityFrameworkCore;

namespace Actie.DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<ActieDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<ActieDbContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}
