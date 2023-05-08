using Actie.BL.Facades.Interfaces;
using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;
using Actie.DAL.Mappers;
using Actie.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Actie.BL.Facades;

class ActivityTagFacade : FacadeBase<ActivityTagEntity, ActivityTagListModel, ActivityTagDetailModel, ActivityTagEntityMapper>,
    IActivityTagFacade
{
    public ActivityTagFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IActivityTagModelMapper modelMapper
    ) : base(unitOfWorkFactory, modelMapper)
    {
    }

    public async Task DeleteByActivityAndTagAsync(Guid activityId, Guid tagId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        var atToDelete = await GetByActivityAndTagAsync(activityId, tagId);

        if(atToDelete == null ) return;

        var id = atToDelete.Id;

        try
        {
            uow.GetRepository<ActivityTagEntity, ActivityTagEntityMapper>().Delete(id);
            await uow.CommitAsync().ConfigureAwait(false);
        }
        catch (DbUpdateException e)
        {
            throw new InvalidOperationException("Entity deletion failed.", e);
        }
    }

    public async Task<ActivityTagDetailModel?> GetByActivityAndTagAsync(Guid activityId, Guid tagId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityTagEntity> query = uow.GetRepository<ActivityTagEntity, ActivityTagEntityMapper>().Get();

        if (IncludesNavigationPathDetails.Any())
        {
            foreach (string includesNavigationPathDetail in IncludesNavigationPathDetails)
            {
                query = string.IsNullOrWhiteSpace(includesNavigationPathDetail)
                    ? query
                    : query.Include(includesNavigationPathDetail);
            }
        }

        ActivityTagEntity? entity = await query.SingleOrDefaultAsync(e => e.ActivityId == activityId && e.TagId == tagId);

        return entity is null
            ? null
            : ModelMapper.MapToDetailModel(entity);
    }

}
