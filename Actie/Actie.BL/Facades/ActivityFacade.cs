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

    protected override List<string> IncludesNavigationPathDetails => new(){
        $"{nameof(ActivityEntity.Tags)}.{nameof(ActivityTagEntity.Tag)}"};

    public async Task<IEnumerable<ActivityListModel>?> GetFilteredPreciseDateTime(Guid userId, DateTime? startsIn = null,
        DateTime? endsIn = null)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        query = query.Include(a => a.Tags).ThenInclude(t => t.Tag);

        
        query = query.Where(a => a.UserId == userId);
        if (startsIn != null)
            query = query.Where(a => a.Start == startsIn);
        if (endsIn != null)
            query = query.Where(a => a.End == endsIn);

        IEnumerable<ActivityEntity> entities = await query.ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    private static bool TimeSpansEqualPreciseOnMinute(TimeSpan timeSpan1, TimeSpan timeSpan2)
    {
        return Math.Abs(timeSpan1.TotalMinutes - timeSpan2.TotalMinutes) < 1;
    }


    public async Task<IEnumerable<ActivityListModel>?> GetFilteredPreciseTime(Guid userId, TimeSpan? startsIn = null,
        TimeSpan? endsIn = null)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        query = query.Include(a => a.Tags).ThenInclude(t => t.Tag);

        query = query.Where(a => a.UserId == userId);
        if (startsIn != null)
            query = query.Where(a => TimeSpansEqualPreciseOnMinute(a.Start.TimeOfDay, (TimeSpan) startsIn));
        if (endsIn != null)
            query = query.Where(a => TimeSpansEqualPreciseOnMinute(a.End.TimeOfDay, (TimeSpan) endsIn));

        IEnumerable<ActivityEntity> entities = await query.ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public async Task<IEnumerable<ActivityListModel>?> GetFilteredBeforeOrAfterPreciseTime(Guid userId, TimeSpan? startsAfter = null,
        TimeSpan? endsBefore = null)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        query = query.Include(a => a.Tags).ThenInclude(t => t.Tag);

        query = query.Where(a => a.UserId == userId);
        if (startsAfter != null)
            query = query.Where(a => a.Start.TimeOfDay > startsAfter);
        if (endsBefore != null)
            query = query.Where(a => a.End.TimeOfDay < endsBefore);

        IEnumerable<ActivityEntity> entities = await query.ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }



    public async Task<IEnumerable<ActivityListModel>?> GetFilteredBeforeOrAfterDateTime(Guid userId, DateTime? startsAfter = null,
        DateTime? startsBefore = null)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        query = query.Include(a => a.Tags).ThenInclude(t => t.Tag);

        
        query = query.Where(a => a.UserId == userId);
        if (startsBefore != null)
            query = query.Where(a => a.Start < startsBefore);
        if (startsAfter != null)
            query = query.Where(a => a.Start > startsAfter);

        IEnumerable<ActivityEntity> entities = await query.ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public async Task<IEnumerable<ActivityListModel>> GetByUserIdAsync(Guid userId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        if (IncludesNavigationPathDetails.Any())
        {
            foreach (string includesNavigationPathDetail in IncludesNavigationPathDetails)
            {
                query = string.IsNullOrWhiteSpace(includesNavigationPathDetail)
                    ? query
                    : query.Include(includesNavigationPathDetail);
            }
        }

        query = query.Where(activity => activity.UserId == userId);

        List<ActivityEntity> entities = await query.ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }
}
