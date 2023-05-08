using Actie.BL.Facades.Interfaces;
using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;
using Actie.DAL.Mappers;
using Actie.DAL.Repositories;
using Actie.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Actie.BL.Facades;

class ProjectFacade : FacadeBase<ProjectEntity, ProjectListModel, ProjectDetailModel, ProjectEntityMapper>, IProjectFacade
{
    private readonly IUserProjectModelMapper _userProjectModelMapper;

    public ProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserProjectModelMapper userProjectModelMapper,
        IProjectModelMapper modelMapper
    ) : base(unitOfWorkFactory, modelMapper)
    {
        _userProjectModelMapper = userProjectModelMapper;
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

    public async Task SaveAsync(ProjectDetailModel project, Guid userId)
    {
        project.Id = Guid.NewGuid();
        ProjectEntity projectEntity = ModelMapper.MapToEntity(project);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        
        var userProject = UserProjectDetailModel.Empty with {Id = Guid.NewGuid()};
        userProject.ProjectId = project.Id;
        userProject.UserId = userId;
        var upEntity = _userProjectModelMapper.MapToEntity(userProject);

        IRepository<UserProjectEntity> upRepository = uow.GetRepository<UserProjectEntity, UserProjectEntityMapper>();
        await upRepository.InsertAsync(upEntity);

        IRepository<ProjectEntity> projectRepository = uow.GetRepository<ProjectEntity, ProjectEntityMapper>();
        await projectRepository.InsertAsync(projectEntity);

        await uow.CommitAsync();
    }
}
