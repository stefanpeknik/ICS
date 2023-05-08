using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Facades.Interfaces;
public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    public Task<IEnumerable<ActivityListModel>?> GetFilteredPreciseTime(Guid userId, DateTime? startsIn = null,
        DateTime? endsIn = null);

    public Task<IEnumerable<ActivityListModel>?> GetFilteredBeforeOrAfter(Guid userId, DateTime? startsAfter = null,
        DateTime? startsBefore = null);

    public Task<IEnumerable<ActivityListModel>> GetByUserIdAsync(Guid userId);
    
}
