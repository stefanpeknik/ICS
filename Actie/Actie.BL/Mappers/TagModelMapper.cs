using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Mappers;

class TagModelMapper : ModelMapperBase<TagEntity, TagListModel, TagDetailModel>, ITagModelMapper
{
    public override TagListModel MapToListModel(TagEntity? entity) => throw new NotImplementedException();

    public override TagDetailModel MapToDetailModel(TagEntity entity) => throw new NotImplementedException();

    public override TagEntity MapToEntity(TagDetailModel model) => throw new NotImplementedException();
}
