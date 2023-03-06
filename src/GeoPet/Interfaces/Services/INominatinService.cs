using GeoPet.Validation.Base;

namespace GeoPet.Interfaces.Services;

public interface INominatinService
{
    public Task<Response> Reverse(string lat, string lon);
}