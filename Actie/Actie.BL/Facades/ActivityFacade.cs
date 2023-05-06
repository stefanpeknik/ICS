using System.Collections;
using System.Runtime.InteropServices.JavaScript;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;
using Actie.DAL.Mappers;
using Actie.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Actie.BL.Facades;

class ActivityFacade : FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>,
    IActivityFacade
{
    public ActivityFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IActivityModelMapper activityModelMapper)
        : base(unitOfWorkFactory, activityModelMapper)
    {
    }

    //protected override string IncludesNavigationPathDetail =>
    //    $"{nameof(ActivityEntity.Tags)}.{nameof(ActivityTagEntity.Tag)}";

    public override async Task<ActivityDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        query = query.Include(a => a.Tags).ThenInclude(t => t.Tag);

        ActivityEntity? entity = await query.SingleOrDefaultAsync(e => e.Id == id);

        return entity is null
            ? null
            : ModelMapper.MapToDetailModel(entity);
    }

    public async Task<IEnumerable<ActivityListModel>?> GetFilteredPreciseTime(Guid? userId = null, DateTime? startsIn = null,
        DateTime? endsIn = null)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        query = query.Include(a => a.Tags).ThenInclude(t => t.Tag);

        if (userId != null)
            query = query.Where(a => a.UserId == userId);
        if (startsIn != null)
            query = query.Where(a => a.Start == startsIn);
        if (endsIn != null)
            query = query.Where(a => a.End == endsIn);

        IEnumerable<ActivityEntity> entities = await query.ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public async Task<IEnumerable<ActivityListModel>?> GetFilteredBeforeOrAfter(Guid? userId = null, DateTime? startsAfter = null,
        DateTime? startsBefore = null)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        query = query.Include(a => a.Tags).ThenInclude(t => t.Tag);

        if (userId != null)
            query = query.Where(a => a.UserId == userId);
        if (startsBefore != null)
            query = query.Where(a => a.Start < startsBefore);
        if (startsAfter != null)
            query = query.Where(a => a.Start > startsAfter);

        IEnumerable<ActivityEntity> entities = await query.ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }
}
