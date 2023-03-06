using GeoPet.DataContract.Model;
using GeoPet.DataContract.Request;
using GeoPet.Validation.Base;

namespace GeoPet.Interfaces.Services;

public interface ISecurityServices
{
    public Task<Response<string>> EncryptPassword(string password);
    public Task<Response<bool>> VerifyPassword(string password, DataContract.Request.UserRequest petParent);
}