using Actie.BL.Facades.Interfaces;
using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;
using Actie.DAL.Mappers;
using Actie.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Actie.BL.Facades;

class UserFacade : FacadeBase<UserEntity, UserListModel, UserDetailModel, UserEntityMapper>, IUserFacade
{
    public UserFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserModelMapper modelMapper
    ) : base(unitOfWorkFactory, modelMapper)
    {
    }

    //protected override string IncludesNavigationPathDetail => $"{nameof(UserEntity.Activities)},{nameof(UserEntity.Projects)}.{nameof(UserProjectEntity.Project)}";

    public override async Task<UserDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<UserEntity> query = uow.GetRepository<UserEntity, UserEntityMapper>().Get();

        query = query.Include(u => u.Activities)
                .ThenInclude(a => a.Tags)
                .ThenInclude(t=> t.Tag)
            .Include(u=> u.Projects)
                .ThenInclude(p => p.Project);

        UserEntity? entity = await query.SingleOrDefaultAsync(e => e.Id == id);

        return entity is null
            ? null
            : ModelMapper.MapToDetailModel(entity);
    }
}
