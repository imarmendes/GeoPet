using GeoPet.Validation.Base;

namespace GeoPet.Interfaces.Services;

public interface ICepService
{
    public Task<Response> GetCep(string cep);
}