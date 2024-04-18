using AdvisorHealthAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;

namespace AdvisorHealthAPI.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Should_return_200_ok_When_send_advisor()
        {
            var api = new AdvisorApiFactory();
            var client = api.CreateClient();
            
            // Act
            var response = await client.PostAsJsonAsync("v1/advisors/", new
            {
                Name = "Lucas",
                SinNumber = 123,
                Address = "My Address",
                Phone = 559
            });

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        /*
        [Fact]
        public async Task Should_add_to_storege_When_has_new_advisor()
        {
           
            var api = new AdvisorApiFactory();
            var client = api.CreateClient();
            var dbContext = api.CreateAdvisorsDbContext();

            var response = await client.PostAsJsonAsync("v1/advisors/", new
            {
                Name = "Lucas Ribeiro",
                SinNumber = 1234,
                Address = "My Address",
                Phone = 559
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var dbEntry = await dbContext.Advisors.FirstOrDefaultAsync(advisor => advisor.SinNumber == 1234);
            
            Assert.NotNull(dbEntry);
            
        }
        */
    }
}