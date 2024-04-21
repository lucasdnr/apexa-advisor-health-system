using AdvisorHealthAPI.Caching;
using AdvisorHealthAPI.Data;
using AdvisorHealthAPI.Interfaces;
using AdvisorHealthAPI.Requests;
using Microsoft.EntityFrameworkCore;
using System;

namespace AdvisorHealthAPI.Models;

public class AdvisorRepository : IAdvisorRepository
{
    private readonly AdvisorsDbContext context;
    private readonly CancellationToken ct;

    public AdvisorRepository(AdvisorsDbContext context, CancellationToken ct)
    {
        this.context = context;
        this.ct = ct;
    }

    public async Task<IEnumerable<Advisor>> GetAdvisors()
    {
        return await context.Advisors.ToListAsync(ct);
    }

    public async Task<Advisor?> GetAdvisor(Guid id, LRUCache<string, Advisor> cache)
    {
        var valueCache = cache.Get(id.ToString());
        if (valueCache is not null)
            return valueCache;

        var advisor = await context
            .Advisors
            .FindAsync(id, ct);
        if (advisor is null)
            return advisor;

        // add to cache
        cache.Set(id.ToString(), advisor);

        return advisor;
    }

    public async Task<Advisor> AddAdvisor(AdvisorRequest request, LRUCache<string, Advisor> cache)
    {
        // Save new record
        var advisor = new Advisor(request.Name, request.SinNumber, request.Address, request.Phone);
        await context.Advisors.AddAsync(advisor, ct);
        await context.SaveChangesAsync(ct);

        // save to cache
        cache.Set(advisor.Id.ToString(), advisor);

        return advisor;
    }

    public async Task<Advisor?> UpdateAdvisor(Guid id, AdvisorRequest request, LRUCache<string, Advisor> cache)
    {
        var advisor = await context.Advisors.SingleOrDefaultAsync(advisor => advisor.Id == id, ct);
        if (advisor is null)
            return null;

        advisor.SetName(request.Name);
        advisor.SetSinNumber(request.SinNumber);
        advisor.SetAddress(request.Address);
        advisor.SetPhone(request.Phone);

        await context.SaveChangesAsync(ct);

        // update cache
        var valueCache = cache.Get(id.ToString());
        if (valueCache is not null)
            cache.Set(id.ToString(), advisor);

        return advisor;
    }
    public async Task<bool> DeleteAdvisor(Guid id, LRUCache<string, Advisor> cache)
    {
        // verify if advisor exists to delete
        var advisor = await context.Advisors.SingleOrDefaultAsync(advisor => advisor.Id == id, ct);
        if (advisor is null)
            return false;

        context.Remove(advisor);
        await context.SaveChangesAsync(ct);

        // remove from cache
        var valueCache = cache.Get(id.ToString());
        if (valueCache is not null)
            cache.Remove(id.ToString());

        return true;
    }
}
