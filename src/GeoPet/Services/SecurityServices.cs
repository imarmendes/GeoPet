using GeoPet.DataContract.Model;
using GeoPet.DataContract.Request;
using GeoPet.Interfaces.Services;
using GeoPet.Validation.Base;

namespace GeoPet.Service;

public class SecurityServices : ISecurityServices
{
    public Task<Response<string>> EncryptPassword(string password)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        return Task.FromResult(Response.Ok<string>(passwordHash));
    }

    public Task<Response<bool>> VerifyPassword(string password, DataContract.Request.UserRequest petParent)
    {
        bool validPassword = BCrypt.Net.BCrypt.Verify(petParent.Password, password);

        return Task.FromResult(Response.Ok<bool>(validPassword));
    }
}