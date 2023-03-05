using System.Net.Http.Headers;
using System.Net.Http.Json;
using GeoPet.DataContract.Model;
using GeoPet.DataContract.Request;
using GeoPet.DataContract.Response;
using GeoPet.Repository;
using GeoPet.Validation.Base;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeoPet.Test;

public class NomiantinTestSucces : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly TestingWebAppFactory<Program> _factory;
    private readonly string _token;
    private readonly HttpClient _client;

    public NomiantinTestSucces(TestingWebAppFactory<Program> factory)
    {
        _factory = factory;
        _token = GetToken().Result;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    private async Task<string> GetToken()
    {
        var client = _factory.CreateClient();
        UserRequest userRequest = new() { Email = "admin@email.com", Password = "Password" };

        var response = await client.PostAsJsonAsync("/api/login", userRequest);

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<string>>(body);

        return result.Data!;
    }


    [Theory]
    [MemberData(nameof(ExpectedAdress))]
    public async Task TestGetAllEndpointSuccess(string lat, string lon, string expectedAddress)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var response = await _client.GetAsync($"/api/Nominatin/?lat={lat}&lon={lon}");
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<object>>(body);
        var address = ((JObject)result!.Data!).Children<JProperty>()
                                            .FirstOrDefault(p => p.Name == "display_name")!.Value
                                            .ToString();

        result.Report.Should().BeEmpty();
        address.Should().Be(expectedAddress);
    }

    public static readonly TheoryData<string, string, string> ExpectedAdress = new()
    {
        {
            "-29.6866607",
            "-53.8084675",
            "Calçadão Salvador Isaia, Centro Histórico, Sede, Santa Maria, Região Geográfica Imediata de Santa Maria, Região Geográfica Intermediária de Santa Maria, Rio Grande do Sul, Região Sul, 97015-015, Brasil"
        }
};




}