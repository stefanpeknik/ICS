using Actie.BL.Facades.Interfaces;
using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;
using Actie.DAL.Mappers;
using Actie.DAL.UnitOfWork;

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
}
