using AdvisorHealthAPI.Models;
using AdvisorHealthAPI.Response;
using AdvisorHealthAPI.Tests.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;

namespace AdvisorHealthAPI.Tests
{
    public class ApiPOSTUnitTest
    {

        [Fact]
        public async Task Should_return_200_ok_When_send_advisor()
        {
            var api = new AdvisorApiFactory();
            var client = api.CreateClient();

            // Arrange
            var sinNumber = Generator.GenerateRandomNumber(9);

            // Act
            var response = await client.PostAsJsonAsync("api/v1/advisors/", new
            {
                Name = "Lucas",
                SinNumber = sinNumber,
                Address = "My Address",
                Phone = 35590012
            });

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Should_return_400_bad_request_When_send_invalid_fields()
        {
            var api = new AdvisorApiFactory();
            var client = api.CreateClient();
            // Arrange
            var sinNumber = Generator.GenerateRandomNumber(9);

            // Act
            var response = await client.PostAsJsonAsync("api/v1/advisors/", new
            {
                Name = "Lucas",
                SinNumber = sinNumber,
                Address = "My Address",
                Phone = 3559
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task Should_return_409_conflict_When_send_same_sin_number()
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

            // Act
            var responseB = await client.PostAsJsonAsync("api/v1/advisors/", new
            {
                Name = "Lucas",
                SinNumber = sinNumber,
                Address = "My Address",
                Phone = 12345678
            });

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, responseB.StatusCode);
        }
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

    }
}