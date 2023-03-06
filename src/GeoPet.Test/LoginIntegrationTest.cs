using System.Net.Http.Json;
using GeoPet.DataContract.Request;
using GeoPet.Test;
using GeoPet.Validation.Base;
using Newtonsoft.Json;

namespace GeoPet.Test;


public class LoginIntegrationTest : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly TestingWebAppFactory<Program> _factory;


    public LoginIntegrationTest(TestingWebAppFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task TestLoginEndpointsSuccess()
    {
        var client = _factory.CreateClient();
        UserRequest userRequest = new() { Email = "admin@email.com", Password = "Password" };

        var response = await client.PostAsJsonAsync("/api/login", userRequest);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<string>>(body);
        var dataSplited = result.Data?.Split('.');

        result.Report.Should().BeEmpty();
        result.Data.Should().NotBeNullOrEmpty();
        dataSplited?.Length.Should().Be(3);
    }

    [Theory]
    [InlineData("adm@email.com", "Password")]
    [InlineData("admin@email.com", "password")]
    public async Task TestLoginEndpointsFail(string email, string password)
    {
        var client = _factory.CreateClient();

        UserRequest userRequest = new() { Email = email, Password = password };

        var response = await client.PostAsJsonAsync("/api/login", userRequest);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<string>>(body);

        result.Report.Should().NotBeNullOrEmpty();
        result.Data.Should().BeNull();
    }
}