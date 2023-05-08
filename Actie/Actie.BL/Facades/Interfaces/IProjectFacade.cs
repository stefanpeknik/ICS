using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Facades.Interfaces;

public interface IProjectFacade : IFacade<ProjectEntity, ProjectListModel, ProjectDetailModel>
{
    Task<IEnumerable<ProjectListModel>> GetByUserIdAsync(Guid userId);
    public Task SaveAsync(ProjectDetailModel project, Guid userId);
}
