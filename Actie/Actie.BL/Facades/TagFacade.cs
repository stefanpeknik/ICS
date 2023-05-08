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

    
    public async Task SaveAsync(TagDetailModel tag, Guid userId)
    {
        tag.Id = Guid.NewGuid();
        TagEntity tagEntity = ModelMapper.MapToEntity(tag);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();


        IRepository<TagEntity> tagRepository = uow.GetRepository<TagEntity, TagEntityMapper>();
        await tagRepository.InsertAsync(tagEntity);

        await uow.CommitAsync();
    }
}
