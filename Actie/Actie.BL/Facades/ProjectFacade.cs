using Actie.BL.Facades.Interfaces;
using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;
using Actie.DAL.Mappers;
using Actie.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Actie.BL.Facades;

class ProjectFacade : FacadeBase<ProjectEntity, ProjectListModel, ProjectDetailModel, ProjectEntityMapper>, IProjectFacade
{
    public ProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IProjectModelMapper modelMapper
    ) : base(unitOfWorkFactory, modelMapper)
    {
    }

    protected override List<string> IncludesNavigationPathDetails => new(){
        $"{nameof(ProjectEntity.Activities)}.{nameof(ActivityEntity.Tags)}.{nameof(ActivityTagEntity.Tag)}",
        $"{nameof(ProjectEntity.Users)}.{nameof(UserProjectEntity.User)}"
    };

    public async Task<IEnumerable<ProjectListModel>> GetByUserIdAsync(Guid userId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ProjectEntity> query = uow.GetRepository<ProjectEntity, ProjectEntityMapper>().Get();

        if (IncludesNavigationPathDetails.Any())
        {
            foreach (string includesNavigationPathDetail in IncludesNavigationPathDetails)
            {
                query = string.IsNullOrWhiteSpace(includesNavigationPathDetail)
                    ? query
                    : query.Include(includesNavigationPathDetail);
            }
        }

        query = query.Where(p => p.Users.Any(up => up.UserId == userId));

        List<ProjectEntity> entities = await query.ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }
}
