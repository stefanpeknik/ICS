using Actie.BL.Facades.Interfaces;
using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;
using Actie.DAL.Mappers;
using Actie.DAL.UnitOfWork;

namespace Actie.BL.Facades;

class UserFacade : FacadeBase<UserEntity, UserListModel, UserDetailModel, UserEntityMapper>, IUserFacade
{
    public UserFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IModelMapper<UserEntity, UserListModel, UserDetailModel> modelMapper
    ) : base(unitOfWorkFactory, modelMapper)
    {
    }
}
