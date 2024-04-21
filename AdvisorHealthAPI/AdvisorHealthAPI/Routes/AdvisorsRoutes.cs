using AdvisorHealthAPI.Data;
using AdvisorHealthAPI.Models;
using AdvisorHealthAPI.Requests;
using AdvisorHealthAPI.Response;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AdvisorHealthAPI.Validators;
using AdvisorHealthAPI.Caching;

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
        advisorsRoutes.MapGet("", async (AdvisorRepository repository) =>
        {
            return Results.Ok(EntityListToResponseList(await repository.GetAdvisors()));
        });

        // get one advisor
        advisorsRoutes.MapGet("{id:guid}", async (Guid id, AdvisorRepository repository) =>
        {
            var advisor = await repository.GetAdvisor(id, cache);
            if (advisor is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityToResponse(advisor));
        });

        // create new advisor
        advisorsRoutes.MapPost("", async (AdvisorRequest request, AdvisorRepository repository, AdvisorValidator validator) =>
        {
            // verify if SIN number exists
            var hasSinNumber = await validator.ExistSinNumber(request.SinNumber);
            if (hasSinNumber)
                return Results.Conflict("SIN number already exists!");


            var advisor = await repository.AddAdvisor(request, cache);


            return Results.Ok(EntityToResponse(advisor));


        }).AddEndpointFilter<ValidationFilter<AdvisorRequest>>(); 

        // update advisor
        advisorsRoutes.MapPut("{id:guid}", async (Guid id, AdvisorRequest request, AdvisorValidator validator, AdvisorRepository repository) =>
        {
            // verify if advisor exists to update
            var hasAdvisor = await validator.ExistAdvisor(id);
            if (!hasAdvisor)
                return Results.NotFound();

            // verify if SIN number exists
            var hasSinNumber = await validator.ExistSinNumberIdIgnore(request.SinNumber, id);
            if (hasSinNumber)
                return Results.Conflict("SIN number already exists!");

            var advisor = await repository.UpdateAdvisor(id, request, cache);
            if (advisor is null)
                return Results.NotFound();

            return Results.Ok(EntityToResponse(advisor));

        }).AddEndpointFilter<ValidationFilter<AdvisorRequest>>();

        // delete advisor
        advisorsRoutes.MapDelete("{id:guid}", async (Guid id, AdvisorRepository repository) =>
        {
            var response = await repository.DeleteAdvisor(id, cache);
            if (response is false)
                return Results.NotFound();

            return Results.NoContent();
        });

    }
}
