using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Mappers.Interfaces;
public interface IProjectModelMapper : IModelMapper<ProjectEntity, ProjectListModel, ProjectDetailModel>
{
}
