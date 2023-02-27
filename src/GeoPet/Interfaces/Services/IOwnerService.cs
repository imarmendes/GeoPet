using GeoPet.DataContract.Request;
using GeoPet.Validation.Base;

namespace GeoPet.Interfaces.Services;

public interface IOwnerService
{
    Task<Response> CreateUser(OwnerRequest ownerRequest);
    Task<Response> GetAllUsers();
    Task<Response> GetUserById(int id);
    Task<Response> UpdateUser(int id, OwnerRequest ownerRequest);
    Task<Response> DeleteUser(int id);
}