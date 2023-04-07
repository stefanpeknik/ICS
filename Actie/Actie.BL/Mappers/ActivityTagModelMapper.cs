using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Mappers;

class ActivityTagModelMapper : ModelMapperBase<ActivityTagEntity, ActivityTagListModel, ActivityTagDetailModel>, IActivityTagModelMapper
{
    public override ActivityTagListModel MapToListModel(ActivityTagEntity? entity) => throw new NotImplementedException();

    public override ActivityTagDetailModel MapToDetailModel(ActivityTagEntity entity) => throw new NotImplementedException();

    public override ActivityTagEntity MapToEntity(ActivityTagDetailModel model) => throw new NotImplementedException();
}
