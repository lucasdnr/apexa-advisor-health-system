using AdvisorHealthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvisorHealthAPI.Data;

public class AdvisorsDbContext : DbContext
{
    private DbSet<Advisor> Advisors {  get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("AdvisorsDb");
        base.OnConfiguring(optionsBuilder);
    }
}
