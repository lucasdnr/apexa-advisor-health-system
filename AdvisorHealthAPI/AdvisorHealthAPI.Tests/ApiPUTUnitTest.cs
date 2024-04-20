using AdvisorHealthAPI.Response;
using AdvisorHealthAPI.Tests.Services;
using System.Net.Http.Json;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace AdvisorHealthAPI.Tests;

public class ApiPUTUnitTest
{
    [Fact]
    public async Task Should_return_404_not_found_When_send_invalid_id()
    {
        var api = new AdvisorApiFactory();
        var client = api.CreateClient();

        // Arrange
        var id = Generator.GenerateRandomNumber(10);
        var sinNumber = Generator.GenerateRandomNumber(9);

        // Act
        var response = await client.PutAsJsonAsync($"api/v1/advisors/{id}", new
        {
            Name = "Lucas",
            SinNumber = sinNumber,
            Address = "My Address",
            Phone = 12345678
        });

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_return_200_ok_When_send_new_data_valid_id()
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
        var responseB = await client.PutAsJsonAsync($"api/v1/advisors/{data.Id}", new
        {
            Name = "Lucas",
            SinNumber = sinNumber,
            Address = "My Address",
            Phone = 99999999
        });

        // Assert
        Assert.Equal(HttpStatusCode.OK, responseB.StatusCode);
    }

}
