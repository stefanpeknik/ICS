using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Mappers.Interfaces;
public interface IActivityModelMapper : IModelMapper<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
}
