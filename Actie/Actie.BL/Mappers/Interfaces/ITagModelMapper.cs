

using Actie.BL.Models;
using Actie.DAL.Entities;
using CookBook.BL.Mappers;

namespace Actie.BL.Mappers.Interfaces;
public interface ITagModelMapper : IModelMapper<TagEntity, TagListModel, TagDetailModel>
{
}
