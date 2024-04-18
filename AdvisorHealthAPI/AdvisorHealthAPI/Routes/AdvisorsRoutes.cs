using AdvisorHealthAPI.Data;
using AdvisorHealthAPI.Models;
using AdvisorHealthAPI.Requests;
using Microsoft.EntityFrameworkCore;

namespace AdvisorHealthAPI.Routes;

public static class AdvisorsRoutes
{
    public static void AddRoutesAdvisors(this WebApplication app)
    {
        var advisorsRoutes = app.MapGroup("advisors");

        // get all advisors
        advisorsRoutes.MapGet("", async (AdvisorsDbContext context) =>
        {
            var advisors = await context.Advisors.ToListAsync();
            return Results.Ok(advisors);
        });

        // create new advisor
        advisorsRoutes.MapPost("", async (AdvisorRequest request, AdvisorsDbContext context) =>
        {
            // verify if SIN number exists
            var hasAdvisor = await context.Advisors.AnyAsync(advisor => advisor.SinNumber == request.SinNumber);
            if (hasAdvisor)
                return Results.Conflict("SIN number already exists!");
            
            // Save new record
            var advisor = new Advisor(request.Name, request.SinNumber, request.Address, request.Phone);
            await context.Advisors.AddAsync(advisor);
            await context.SaveChangesAsync();
            return Results.Ok(advisor);
 
        });

        // update advisor
        advisorsRoutes.MapPut("{id:guid}", async (Guid id, AdvisorRequest request, AdvisorsDbContext context) =>
        {
            // verify if advisor exists to update
            var advisor = await context.Advisors.SingleOrDefaultAsync(advisor => advisor.Id == id);
            if(advisor == null)
                return Results.NotFound();

            // verify if SIN number exists
            var hasSinNumber = await context.Advisors.AnyAsync(advisor => advisor.SinNumber == request.SinNumber && advisor.Id != id);
            if (hasSinNumber)
                return Results.Conflict("SIN number already exists!");


            advisor.SetName(request.Name);
            advisor.SetSinNumber(request.SinNumber);
            advisor.SetAddress(request.Address);
            advisor.SetPhone(request.Phone);

            await context.SaveChangesAsync();
            return Results.Ok(advisor);

        });

    }
}
