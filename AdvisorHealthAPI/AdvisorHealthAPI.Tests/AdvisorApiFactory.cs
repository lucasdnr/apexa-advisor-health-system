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
    /* I understand the idea that involves using dBContext in tests to be able to access 
     * the same database as the main API application, I need to read the documentation 
     * more to understand why this method still doesn't work
     */
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
