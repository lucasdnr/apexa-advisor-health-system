using AdvisorHealthAPI.Data;
using AdvisorHealthAPI.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdvisorHealthAPI.Tests;

public class AdvisorApiFactory : WebApplicationFactory<Program>
{
    public AdvisorsDbContext CreateAdvisorsDbContext()
    {
        var db = Services.GetRequiredService<AdvisorsDbContext>();
        db.Database.EnsureCreated();
        return db;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddDbContextFactory<AdvisorsDbContext>(o => o.UseInMemoryDatabase("AdvisorsDb"));
        });
    }
}
