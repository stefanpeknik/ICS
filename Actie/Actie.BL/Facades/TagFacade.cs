using Actie.BL.Facades.Interfaces;
using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;
using Actie.DAL.Mappers;
using Actie.DAL.Repositories;
using Actie.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Actie.BL.Facades;

class TagFacade : FacadeBase<TagEntity, TagListModel, TagDetailModel, TagEntityMapper>, ITagFacade
{
    private readonly IActivityTagModelMapper _activityTagModelMapper;

    public TagFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        ITagModelMapper modelMapper
    ) : base(unitOfWorkFactory, modelMapper)
    {
    }

    protected List<string> IncludesNavigationPathDetail => new()
    {
        $"{nameof(TagEntity.Activities)}.{nameof(ActivityTagEntity.Activity)}.{nameof(ActivityEntity.User)}"
    };

    public virtual async Task<IEnumerable<TagListModel>?> GetTagsOfActivityAsync(Guid activityId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<TagEntity> query = uow.GetRepository<TagEntity, TagEntityMapper>().Get();

        if (IncludesNavigationPathDetails.Any())
        {
            foreach (string includesNavigationPathDetail in IncludesNavigationPathDetails)
            {
                query = string.IsNullOrWhiteSpace(includesNavigationPathDetail)
                    ? query
                    : query.Include(includesNavigationPathDetail);
            }
        }

        query = query.Where(t => t.Activities.Any(at => at.ActivityId == activityId));

        var entities = await query.ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

}
