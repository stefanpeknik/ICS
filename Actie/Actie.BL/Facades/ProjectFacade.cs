using Actie.BL.Facades.Interfaces;
using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;
using Actie.DAL.Mappers;
using Actie.DAL.UnitOfWork;

namespace Actie.BL.Facades;

class ProjectFacade : FacadeBase<ProjectEntity, ProjectListModel, ProjectDetailModel, ProjectEntityMapper>, IProjectFacade
{
    public ProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IModelMapper<ProjectEntity, ProjectListModel, ProjectDetailModel> modelMapper
    ) : base(unitOfWorkFactory, modelMapper)
    {
    }
}
