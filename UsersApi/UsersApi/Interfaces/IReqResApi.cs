using UsersApi.Dtos;
using UsersApi.Models.Request;
using UsersApi.Models.Response;

namespace UsersApi.Interfaces
{
    public interface IReqResApi
    {
        Task<List<UserResponseModel>> GetAllUsersByPage(int page);
        Task<UserResponseModel> GetUserById(int userId);
        Task<CreateUserResponseModel> CreateUser(CreateUserRequestModel createUserModel);
        Task<UpdateUserResponseModel> UpdateUser(UpdateUserDto updateUserDto);
        Task DeleteUserById(int userId);
    }
}
