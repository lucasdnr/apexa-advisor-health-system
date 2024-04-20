using AdvisorHealthAPI.Data;
using AdvisorHealthAPI.Models;
using AdvisorHealthAPI.Requests;
using AdvisorHealthAPI.Response;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AdvisorHealthAPI.Validators;
using AdvisorHealthAPI.Caching;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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


    public static void AddRoutesAdvisors(WebApplication app, LRUCache<string, Advisor> cache)
    {
        var advisorsRoutes = app.MapGroup("api/v1/advisors");

        // testing
        //advisorsRoutes.MapGet("/testing", () => "Hello World!");

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
            var valueCache = cache.Get(id.ToString());
            if(valueCache is not null)
                return Results.Ok(EntityToResponse(valueCache));

            var advisor = await context
                .Advisors
                .FindAsync(id, ct);
            if(advisor is null)
                return Results.NotFound();

            // add to cache
            cache.Set(id.ToString(), advisor);

            return Results.Ok(EntityToResponse(advisor));
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

            // save to cache
            cache.Set(advisor.Id.ToString(), advisor);

            return Results.Ok(EntityToResponse(advisor));
       

        }).AddEndpointFilter<ValidationFilter<AdvisorRequest>>(); 

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

            // update cache
            var valueCache = cache.Get(id.ToString());
            if (valueCache is not null)
                cache.Set(id.ToString(), advisor);

            return Results.Ok(EntityToResponse(advisor));

        }).AddEndpointFilter<ValidationFilter<AdvisorRequest>>();

        // delete advisor
        advisorsRoutes.MapDelete("{id:guid}", async (Guid id, AdvisorsDbContext context, CancellationToken ct) =>
        {
            // verify if advisor exists to delete
            var advisor = await context.Advisors.SingleOrDefaultAsync(advisor => advisor.Id == id, ct);
            if (advisor is null)
                return Results.NotFound();

            context.Remove(advisor);
            await context.SaveChangesAsync(ct);

            // remove from cache
            var valueCache = cache.Get(id.ToString());
            if (valueCache is not null)
                cache.Remove(id.ToString());

            return Results.NoContent();
        });

    }
}
