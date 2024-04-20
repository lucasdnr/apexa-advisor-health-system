using AdvisorHealthAPI.Response;
using AdvisorHealthAPI.Tests.Services;
using System.Net.Http.Json;
using System.Net;

namespace AdvisorHealthAPI.Tests;

public class ApiGETUnitTest
{
    [Fact]
    public async Task Should_return_200_ok_When_get_advisor_by_id()
    {
        var api = new AdvisorApiFactory();
        var client = api.CreateClient();

        // Arrange
        var sinNumber = Generator.GenerateRandomNumber(9);

        var responseA = await client.PostAsJsonAsync("api/v1/advisors/", new
        {
            Name = "Lucas",
            SinNumber = sinNumber,
            Address = "My Address",
            Phone = 12345678
        });

        Assert.Equal(HttpStatusCode.OK, responseA.StatusCode);

        var data = await responseA.Content.ReadFromJsonAsync<AdvisorResponse>();

        // Act
        var responseB = await client.GetAsync($"api/v1/advisors/{data.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, responseB.StatusCode);
    }

    [Fact]
    public async Task Should_return_404_error_When_use_wrong_id()
    {
        var api = new AdvisorApiFactory();
        var client = api.CreateClient();

        // Arrange
        var id = Generator.GenerateRandomNumber(10);

        // Act
        var responseB = await client.GetAsync($"api/v1/advisors/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, responseB.StatusCode);

    }

    [Fact]
    public async Task Should_return_200_ok_When_get_all_advisors()
    {
        var api = new AdvisorApiFactory();
        var client = api.CreateClient();

        // Arrange


        // Act
        var response = await client.GetAsync("api/v1/advisors");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    }
}
