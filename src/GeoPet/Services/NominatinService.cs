using System.Net.Http.Headers;
using GeoPet.DataContract.Response;
using GeoPet.Interfaces.Services;
using GeoPet.Validation.Base;


namespace GeoPet.Service;

public class NominatinService : INominatinService
{
    private readonly HttpClient _client;
    private const string _baseUrl = "https://nominatim.openstreetmap.org";

    public NominatinService(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri(_baseUrl);
    }

    public async Task<Response> Reverse(string lat, string lon)
    {
        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("StudentProjectGeoPet", "1.0"));
        var response = await _client.GetAsync($"/reverse?format=jsonv2&zoom=17&lat={lat}&lon={lon}");

        if (!response.IsSuccessStatusCode) return Response.Unprocessable(Report.Create("Erro ao Localizar"));

        var result = await response.Content.ReadFromJsonAsync<object>();

        return new Response<object>(result!);
    }
}