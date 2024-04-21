
using AdvisorHealthAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace AdvisorHealthAPI.Validators;

public class AdvisorValidator
{
    private readonly AdvisorsDbContext context;
    private readonly CancellationToken ct;
    
    public AdvisorValidator(AdvisorsDbContext context, CancellationToken ct)
    {
        this.context = context;
        this.ct = ct;
    }

    public async Task<bool> ExistSinNumber(int sinNumber)
    {
        // verify if SIN number exists
        var hasAdvisor = await context.Advisors.AnyAsync(advisor => advisor.SinNumber == sinNumber, ct);
        if (hasAdvisor)
            return true;

        return false;
    }

    public async Task<bool> ExistSinNumberIdIgnore(int sinNumber, Guid id)
    {
        // verify if SIN number exists
        var hasAdvisor = await context.Advisors.AnyAsync(advisor => advisor.SinNumber == sinNumber && advisor.Id != id, ct);
        if (hasAdvisor)
            return true;

        return false;
    }

    public async Task<bool> ExistAdvisor(Guid id)
    {
        var advisor = await context.Advisors.SingleOrDefaultAsync(advisor => advisor.Id == id, ct);
        if (advisor is null)
            return false;
        
        return true;
    }
}
