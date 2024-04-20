using AdvisorHealthAPI.Tests.Services;
using System.Net.Http.Json;
using System.Net;
using AdvisorHealthAPI.Response;

namespace AdvisorHealthAPI.Tests;

public class ApiDELUnitTest
{
    [Fact]
    public async Task Should_return_404_not_found_When_send_invalid_id()
    {
        var api = new AdvisorApiFactory();
        var client = api.CreateClient();

        // Arrange
        var id = Generator.GenerateRandomNumber(10);

        // Act
        var response = await client.DeleteAsync($"api/v1/advisors/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_return_204_no_content_When_send_valid_id()
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
        var responseB = await client.DeleteAsync($"api/v1/advisors/{data.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, responseB.StatusCode);
    }

    [Fact]
    public async Task Should_return_404_no_found_When_try_deleted_id()
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

        
        var responseB = await client.DeleteAsync($"api/v1/advisors/{data.Id}");

        Assert.Equal(HttpStatusCode.NoContent, responseB.StatusCode);


        // Act
        var responseC = await client.DeleteAsync($"api/v1/advisors/{data.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, responseC.StatusCode);
    }
}
