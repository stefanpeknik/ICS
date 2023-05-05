using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Facades.Interfaces;
public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    public Task<IEnumerable<ActivityListModel>?> GetFiltered(Guid? userId = null, DateTime? startTime = null,
        DateTime? endTime = null);
}
