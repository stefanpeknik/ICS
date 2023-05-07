using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Facades.Interfaces;
public interface ITagFacade : IFacade<TagEntity, TagListModel, TagDetailModel>
{
    Task<IEnumerable<TagListModel>> GetByUserIdAsync(Guid userId);
}
