using Actie.BL.Facades.Interfaces;
using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;
using Actie.DAL.Mappers;
using Actie.DAL.UnitOfWork;

namespace Actie.BL.Facades;

class TagFacade : FacadeBase<TagEntity, TagListModel, TagDetailModel, TagEntityMapper>, ITagFacade
{
    public TagFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IModelMapper<TagEntity, TagListModel, TagDetailModel> modelMapper
    ) : base(unitOfWorkFactory, modelMapper)
    {
    }
}
