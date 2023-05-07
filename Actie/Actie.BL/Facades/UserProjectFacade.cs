using Actie.BL.Facades.Interfaces;
using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;
using Actie.DAL.Mappers;
using Actie.DAL.UnitOfWork;

namespace Actie.BL.Facades;

class UserProjectFacade : FacadeBase<UserProjectEntity, UserProjectListModel, UserProjectDetailModel, UserProjectEntityMapper>, IUserProjectFacade
{
    public UserProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserProjectModelMapper modelMapper
    ) : base(unitOfWorkFactory, modelMapper)
    {
    }
}
