using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Facades.Interfaces;
public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    public Task<IEnumerable<(DateTime, DateTime)>?> SaveCheckDateTimeAsync(ActivityDetailModel model);

    public Task<IEnumerable<ActivityListModel>?> GetFilteredPreciseDateTime(Guid userId, DateTime? startsIn = null,
        DateTime? endsIn = null);

    public Task<IEnumerable<ActivityListModel>?> GetFilteredBeforeOrAfterDateTime(Guid userId, DateTime? startsAfter = null,
        DateTime? startsBefore = null);

    public Task<IEnumerable<ActivityListModel>?> GetFilteredPreciseTime(Guid userId, TimeSpan? startsIn = null,
        TimeSpan? endsIn = null);

    public Task<IEnumerable<ActivityListModel>?> GetFilteredBeforeOrAfterPreciseTime(Guid userId,
        TimeSpan? startsAfter = null,
        TimeSpan? endsBefore = null);

    public Task<IEnumerable<ActivityListModel>> GetByUserIdAsync(Guid userId);
    
}
