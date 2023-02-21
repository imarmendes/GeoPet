using GeoPet.DataContract.Response;
using GeoPet.Interfaces.Services;
using GeoPet.Validation.Base;


namespace GeoPet.Service;

public class CepService : ICepService
{
    private readonly HttpClient _client;
    private const string _baseUrl = "https://viacep.com.br/ws/";

    public CepService(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri(_baseUrl);
    }

    public async Task<Response> GetCep(string cep)
    {
        var response = await _client.GetAsync($"{cep}/json/");

        if (!response.IsSuccessStatusCode) return Response.Unprocessable(Report.Create("Formato do CEP inválido"));

        var result = await response.Content.ReadFromJsonAsync<object>();

        if (result!.ToString()!.Contains("erro")) return Response.Unprocessable(Report.Create("CEP não existe"));

        return new Response<object>(result!);
    }

}
