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

public class PetIntegrationTestSucces : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly TestingWebAppFactory<Program> _factory;
    private readonly string _token;
    private readonly HttpClient _client;

    public PetIntegrationTestSucces(TestingWebAppFactory<Program> factory)
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
    public async Task TestCreateEndpointsSuccess(PetRequest petRequest, PetResponse petResponse)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var response = await _client.PostAsJsonAsync("/api/Pet", petRequest);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<PetResponse>>(body);

        result.Report.Should().BeEmpty();
        result.Data.Should().NotBeNull();
        result.Data.Id.Should().NotBeEmpty();
        petResponse.Id = result.Data.Id;
        result.Data.Should().BeEquivalentTo(petResponse);
    }

    public static readonly TheoryData<PetRequest, PetResponse> ExpectedCreated = new()
    {
        {
            new PetRequest
            {
                Name = "Pet4",
                Age = 6,
                Size = "médio",
                Breed = "Vira-lata",
                OwnerId = 3
            },
            new PetResponse
            {
                Id = Guid.Empty,
                Name = "Pet4",
                Age = 6,
                Size = "médio",
                Breed = "Vira-lata",
                OwnerId = 3
            }
        }
    };


    [Theory]
    [MemberData(nameof(ExpectedPetList))]
    public async Task TestGetAllEndpointSuccess(List<PetResponse> petExpected)
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<GeoPetContext>();

            Helpers.ResetDbForTests(db);
        }

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var response = await _client.GetAsync("/api/Pet");
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<List<PetResponse>>>(body);


        result.Report.Should().BeEmpty();
        result.Data.Should().BeEquivalentTo(petExpected);
    }

    public static readonly TheoryData<List<PetResponse>> ExpectedPetList = new()
    {
         new() {
            new PetResponse
            {
                Id = Guid.Parse("08148B33-D29C-41B8-B797-EC903D9EDE71"),
                Name = "Pet2",
                Age = 6,
                Size = "pequeno",
                Breed = "Pug",
                OwnerId = 2
            },
            new PetResponse
            {
                Id = Guid.Parse("E646F236-E19D-4C3A-99B9-1FD09E21EB4F"),
                Name = "Pet3",
                Age = 6,
                Size = "pequeno",
                Breed = "Pug",
                OwnerId = 2
            },
            new PetResponse
            {
                Id = Guid.Parse("D386421C-B9DD-46F0-8EC1-5D788C1A33A6"),
                Name = "Pet1",
                Age = 5,
                Size = "grande",
                Breed = "Pastor-alemão",
                OwnerId = 3
            }
        }
        };


    [Theory]
    [MemberData(nameof(ExpectedOwnerById))]
    public async Task TestGetByIdEndpointSuccess(Guid id, PetResponse petExpected)
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<GeoPetContext>();

            Helpers.ResetDbForTests(db);
        }

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var response = await _client.GetAsync($"/api/Pet/{id}");
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<PetResponse>>(body);


        result.Report.Should().BeEmpty();
        result.Data.Should().BeEquivalentTo(petExpected);
    }

    public static readonly TheoryData<Guid, PetResponse> ExpectedOwnerById = new()
    {
        {
            Guid.Parse("D386421C-B9DD-46F0-8EC1-5D788C1A33A6"),
           new PetResponse
            {
                Id = Guid.Parse("D386421C-B9DD-46F0-8EC1-5D788C1A33A6"),
                Name = "Pet1",
                Age = 5,
                Size = "grande",
                Breed = "Pastor-alemão",
                OwnerId = 3
            }
        }
    };

    [Theory]
    [MemberData(nameof(ExpectedForUpdate))]
    public async Task TestUpdateEndpointSuccess(Guid id, PetRequest petRequest, PetResponse petExpected)
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<GeoPetContext>();

            Helpers.ResetDbForTests(db);
        }

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var response = await _client.PutAsJsonAsync($"/api/Pet/{id}", petRequest);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<PetResponse>>(body);


        result.Report.Should().BeEmpty();
        result.Data.Should().BeEquivalentTo(petExpected);
    }

    public static readonly TheoryData<Guid, PetRequest, PetResponse> ExpectedForUpdate = new()
        {
            {
                Guid.Parse("D386421C-B9DD-46F0-8EC1-5D788C1A33A6"),
                new PetRequest
                {
                    Name = "Pet1",
                    Age = 6,
                    Size = "grande",
                    Breed = "Pastor-alemão",
                    OwnerId = 3
                },
                new PetResponse
                {
                    Id = Guid.Parse("D386421C-B9DD-46F0-8EC1-5D788C1A33A6"),
                    Name = "Pet1",
                    Age = 6,
                    Size = "grande",
                    Breed = "Pastor-alemão",
                    OwnerId = 3
                }
            }
        };

    [Theory]
    [MemberData(nameof(GuidForDelete))]
    public async Task TestDeleteEndpointSuccess(Guid id)
    {
        using var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<GeoPetContext>();
        Helpers.ResetDbForTests(db);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var response = await _client.DeleteAsync($"/api/Pet/{id}");
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<PetResponse>>(body);



        var ownerCount = db.Pet.Count();


        result.Report.Should().BeEmpty();
        ownerCount.Should().Be(2);
    }

    public static readonly TheoryData<Guid> GuidForDelete = new() { { Guid.Parse("E646F236-E19D-4C3A-99B9-1FD09E21EB4F") } };
}