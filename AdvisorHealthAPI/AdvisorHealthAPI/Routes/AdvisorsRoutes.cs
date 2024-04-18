using AdvisorHealthAPI.Data;
using AdvisorHealthAPI.Models;
using AdvisorHealthAPI.Requests;

namespace AdvisorHealthAPI.Routes;

public static class AdvisorsRoutes
{
    public static void AddRoutesAdvisors(this WebApplication app)
    {
        var advisorsRoutes = app.MapGroup("advisors");

        //advisorsRoutes.MapGet("advisors", () => new Advisor("lucas", "address", 456));

        advisorsRoutes.MapPost("", async (AdvisorRequest request, AdvisorsDbContext context) =>
        {
            
            var advisor = new Advisor(request.Name, request.SIN, request.Address, request.Phone);
            await context.Advisors.AddAsync(advisor);
            await context.SaveChangesAsync();
            return Results.Ok();
 
        });

    }
}
