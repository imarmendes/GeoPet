using GeoPet.DataContract.Request;
using GeoPet.Validation.Base;

namespace GeoPet.Interfaces.Services;

public interface IAuthenticationService
{
    public Task<Response> Authenticate(UserRequest user);
}