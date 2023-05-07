using Actie.BL.Facades.Interfaces;
using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;
using Actie.DAL.Mappers;
using Actie.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Actie.BL.Facades;

class TagFacade : FacadeBase<TagEntity, TagListModel, TagDetailModel, TagEntityMapper>, ITagFacade
{
    public TagFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        ITagModelMapper modelMapper
    ) : base(unitOfWorkFactory, modelMapper)
    {
    }

    //protected override string IncludesNavigationPathDetail => $"{nameof(TagEntity.Activities)}.{nameof(ActivityTagEntity.Activity)}";

    public override async Task<TagDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<TagEntity> query = uow.GetRepository<TagEntity, TagEntityMapper>().Get();

        query = query.Include(t => t.Activities)
            .ThenInclude(a => a.Activity);

        TagEntity? entity = await query.SingleOrDefaultAsync(e => e.Id == id);

        return entity is null
            ? null
            : ModelMapper.MapToDetailModel(entity);
    }
}
