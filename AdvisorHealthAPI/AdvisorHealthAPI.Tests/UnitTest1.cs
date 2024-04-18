using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace AdvisorHealthAPI.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task TestRootEndpoint()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var response = await client.GetStringAsync("v1/advisors/testing");

            Assert.Equal("Hello World!", response);
        }
    }
}