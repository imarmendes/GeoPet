using GeoPet.DataContract.Request;
using GeoPet.Validation.Base;

namespace GeoPet.Interfaces.Services;

public interface IUserService
{
    Task<Response> CreateUser(UserRequest userRequest);
    Task<Response> GetAllUsers();
    Task<Response> GetUserById(int id);
    Task<Response> UpdateUser(int id, UserRequest userRequest);
    Task<Response> DeleteUser(int id);
}