using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Facades.Interfaces;

public interface IActivityTagFacade : IFacade<ActivityTagEntity, ActivityTagListModel, ActivityTagDetailModel>
{
    Task DeleteByActivityAndTagAsync(Guid activityId, Guid tagId);
    Task<ActivityTagDetailModel?> GetByActivityAndTagAsync(Guid activityId, Guid tagId);
}
