using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Facades.Interfaces;
public interface ITagFacade : IFacade<TagEntity, TagListModel, TagDetailModel>
{
     public Task<IEnumerable<TagListModel>?> GetTagsOfActivityAsync(Guid activityId);
}
