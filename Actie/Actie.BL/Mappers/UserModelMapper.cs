using Actie.BL.Mappers.Interfaces;
using Actie.BL.Models;
using Actie.DAL.Entities;

namespace Actie.BL.Mappers;

class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel>, IUserModelMapper
{
    private readonly IActivityModelMapper _activityModelMapper;
    private readonly IUserProjectModelMapper _userProjectModelMapper;

    public UserModelMapper(IActivityModelMapper activityModelMapper, IUserProjectModelMapper userProjectModelMapper)
    {
        _activityModelMapper = activityModelMapper;
        _userProjectModelMapper = userProjectModelMapper;
    }

    public override UserListModel MapToListModel(UserEntity? entity)
        => entity is null
            ? UserListModel.Empty
            : new UserListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Photo = entity.Photo
            };

    public override UserDetailModel MapToDetailModel(UserEntity? entity)
        => entity is null
            ? UserDetailModel.Empty
            : new UserDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Photo = entity.Photo,
                Age = entity.Age,
                Gender = entity.Gender,
                Weight = entity.Weight,
                Height = entity.Height,
                Activities = _activityModelMapper.MapToListModel(entity.Activities).ToObservableCollection(),
                Projects = _userProjectModelMapper.MapToListModel(entity.Projects).ToObservableCollection()
            };

    public override UserEntity MapToEntity(UserDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Surname = model.Surname,
            Photo = model.Photo,
            Age = model.Age,
            Gender = model.Gender,
            Weight = model.Weight,
            Height = model.Height
        };
}
