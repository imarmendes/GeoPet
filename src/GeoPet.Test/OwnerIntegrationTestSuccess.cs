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

public class OwnerIntegrationTestSucces : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly TestingWebAppFactory<Program> _factory;
    private readonly string _token;
    private readonly HttpClient _client;

    public OwnerIntegrationTestSucces(TestingWebAppFactory<Program> factory)
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
    [MemberData(nameof(ExpectedCreated))]
    public async Task TestCreateEndpointsSuccess(OwnerRequest ownerRequest, OwnerResponse ownerResponse)
    {
        var response = await _client.PostAsJsonAsync("/api/Owner", ownerRequest);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<OwnerResponse>>(body);

        result.Report.Should().BeEmpty();
        result.Data.Should().BeEquivalentTo(ownerResponse);
    }

    public static readonly TheoryData<OwnerRequest, OwnerResponse> ExpectedCreated = new()
    {
        {
            new OwnerRequest
            {
                Email = "owner@email.com",
                Password = "Password",
                Name = "Pessoa3",
                CEP = "97015100"
            },
            new OwnerResponse
            {
                Id = 4,
                Name = "Pessoa3",
                Email = "owner@email.com",
                CEP = "97015100",
                Pets = new List<Pet>()
            }
        }
    };


    [Theory]
    [MemberData(nameof(ExpectedOwnerList))]
    public async Task TestGetAllEndpointSuccess(List<OwnerResponse> ownersExpected)
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<GeoPetContext>();

            Helpers.ResetDbForTests(db);
        }

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var response = await _client.GetAsync("/api/Owner");
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<List<OwnerResponse>>>(body);


        result.Report.Should().BeEmpty();
        result.Data.Should().BeEquivalentTo(ownersExpected);
    }

    public static readonly TheoryData<List<OwnerResponse>> ExpectedOwnerList = new()
    {
         new() {
            new OwnerResponse
            {
                Id = 1,
                Name = "Admin",
                Email = "admin@email.com",
                CEP = "01001000",
                Pets = new List<Pet>()
            },
             new OwnerResponse
             {
                Id = 2,
                Name = "Pessoa1",
                Email = "email@email.com",
                CEP = "01001000",
                Pets = new List<Pet>()
             },
             new OwnerResponse
             {
                Id = 3,
                Name = "Pessoa2",
                Email = "email2@email.com",
                CEP = "01001000",
                Pets = new List<Pet>()
             }
        }
};

    [Theory]
    [MemberData(nameof(ExpectedOwnerById))]
    public async Task TestGetByIdEndpointSuccess(int id, OwnerResponse ownersExpected)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var response = await _client.GetAsync($"/api/Owner/{id}");
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<OwnerResponse>>(body);


        result.Report.Should().BeEmpty();
        result.Data.Should().BeEquivalentTo(ownersExpected);
    }

    public static readonly TheoryData<int, OwnerResponse> ExpectedOwnerById = new()
    {
        {
            3,
            new OwnerResponse
             {
                Id = 3,
                Name = "Pessoa2",
                Email = "email2@email.com",
                CEP = "01001000",
                Pets = new List<Pet>()
             }
        }
    };

    [Theory]
    [MemberData(nameof(ExpectedForUpdate))]
    public async Task TestUpdateEndpointSuccess(int id, OwnerRequest ownerRequest, OwnerResponse ownersExpected)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var response = await _client.PutAsJsonAsync($"/api/Owner/{id}", ownerRequest);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<OwnerResponse>>(body);


        result.Report.Should().BeEmpty();
        result.Data.Should().BeEquivalentTo(ownersExpected);
    }

    public static readonly TheoryData<int, OwnerRequest, OwnerResponse> ExpectedForUpdate = new()
    {
        {
            2,
            new OwnerRequest
             {
                Name = "Pessoa10",
                Email = "email10@email.com",
                Password = "Password",
                CEP = "01001000",
             },
             new OwnerResponse
             {
                Id = 2,
                Name = "Pessoa10",
                Email = "email10@email.com",
                CEP = "01001000",
                Pets = new List<Pet>()
             }
        }
    };

    [Theory]
    [InlineData(2)]
    public async Task TestDeleteEndpointSuccess(int id)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var response = await _client.DeleteAsync($"/api/Owner/{id}");
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<OwnerResponse>>(body);


        using var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<GeoPetContext>();
        var ownerCount = db.Owner.Count();


        result.Report.Should().BeEmpty();
        ownerCount.Should().Be(2);
    }
}