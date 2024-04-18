using AdvisorHealthAPI.Data;
using AdvisorHealthAPI.Models;
using AdvisorHealthAPI.Requests;
using AdvisorHealthAPI.Response;
using Microsoft.EntityFrameworkCore;
using System;

namespace AdvisorHealthAPI.Routes;

public static class AdvisorsRoutes
{
    private static ICollection<AdvisorResponse> EntityListToResponseList(IEnumerable<Advisor> advisorList)
    {
        return advisorList.Select(a => EntityToResponse(a)).ToList();
    }

    private static AdvisorResponse EntityToResponse(Advisor advisor)
    {
        return new AdvisorResponse(
                        advisor.Id,
                        advisor.Name,
                        advisor.SinNumber,
                        advisor.Address,
                        advisor.Phone,
                        advisor.HealthStatus
                    );
    }

    public static void AddRoutesAdvisors(this WebApplication app)
    {
        var advisorsRoutes = app.MapGroup("advisors");

        // get all advisors
        advisorsRoutes.MapGet("", async (AdvisorsDbContext context, CancellationToken ct) =>
        {
            var advisors = await context
                .Advisors
                .ToListAsync(ct);

            var advisorListResponse = EntityListToResponseList(advisors);
            return Results.Ok(advisorListResponse);
        });

        // get one advisor
        advisorsRoutes.MapGet("{id:guid}", async (Guid id, AdvisorsDbContext context, CancellationToken ct) =>
        {
            var advisor = await context
                .Advisors
                .FindAsync(id, ct);
            if(advisor is null)
                return Results.NotFound();

            return Results.Ok(advisor);
        });

        // create new advisor
        advisorsRoutes.MapPost("", async (AdvisorRequest request, AdvisorsDbContext context, CancellationToken ct) =>
        {
            // verify if SIN number exists
            var hasAdvisor = await context.Advisors.AnyAsync(advisor => advisor.SinNumber == request.SinNumber, ct);
            if (hasAdvisor)
                return Results.Conflict("SIN number already exists!");
            
            // Save new record
            var advisor = new Advisor(request.Name, request.SinNumber, request.Address, request.Phone);
            await context.Advisors.AddAsync(advisor, ct);
            await context.SaveChangesAsync(ct);

            return Results.Ok(EntityToResponse(advisor));
 
        });

        // update advisor
        advisorsRoutes.MapPut("{id:guid}", async (Guid id, AdvisorRequest request, AdvisorsDbContext context, CancellationToken ct) =>
        {
            // verify if advisor exists to update
            var advisor = await context.Advisors.SingleOrDefaultAsync(advisor => advisor.Id == id, ct);
            if(advisor is null)
                return Results.NotFound();

            // verify if SIN number exists
            var hasSinNumber = await context.Advisors.AnyAsync(advisor => advisor.SinNumber == request.SinNumber && advisor.Id != id, ct);
            if (hasSinNumber)
                return Results.Conflict("SIN number already exists!");


            advisor.SetName(request.Name);
            advisor.SetSinNumber(request.SinNumber);
            advisor.SetAddress(request.Address);
            advisor.SetPhone(request.Phone);

            await context.SaveChangesAsync(ct);

            return Results.Ok(EntityToResponse(advisor));

        });

        // delete advisor
        advisorsRoutes.MapDelete("{id:guid}", async (Guid id, AdvisorsDbContext context, CancellationToken ct) =>
        {
            // verify if advisor exists to delete
            var advisor = await context.Advisors.SingleOrDefaultAsync(advisor => advisor.Id == id, ct);
            if (advisor is null)
                return Results.NotFound();

            context.Remove(advisor);
            await context.SaveChangesAsync(ct);

            return Results.NoContent();
        });

    }
}
