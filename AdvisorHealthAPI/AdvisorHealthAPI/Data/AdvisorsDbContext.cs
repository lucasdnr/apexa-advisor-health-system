using AdvisorHealthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvisorHealthAPI.Data;

public class AdvisorsDbContext : DbContext
{
    public DbSet<Advisor> Advisors {  get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("AdvisorsDb");
        //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

        base.OnConfiguring(optionsBuilder);
    }
}
