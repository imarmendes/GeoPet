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

namespace GeoPet.Test;

public class LostPetTestSucces : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly TestingWebAppFactory<Program> _factory;
    private readonly string _token;
    private readonly HttpClient _client;

    public LostPetTestSucces(TestingWebAppFactory<Program> factory)
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
    [MemberData(nameof(PetGuid))]
    public async Task TestGetByIdEndpointSuccess(Guid id)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var response = await _client.GetAsync($"/api/LostPet/{id}");
        response.EnsureSuccessStatusCode();

        var result = response.Content.Headers.ContentType.ToString();

        result.Should().Be("image/jpeg");
    }

    public static readonly TheoryData<Guid> PetGuid = new()
    {
        {
            Guid.Parse("D386421C-B9DD-46F0-8EC1-5D788C1A33A6")
        }
    };
}