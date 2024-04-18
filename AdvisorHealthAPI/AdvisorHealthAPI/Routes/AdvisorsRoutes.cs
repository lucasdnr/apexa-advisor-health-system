using AdvisorHealthAPI.Models;

namespace AdvisorHealthAPI.Routes;

public static class AdvisorsRoutes
{
    public static void AddRoutesAdvisors(this WebApplication app)
    {
        app.MapGet("advisors", () => new Advisor("lucas", 123, "address", 456));
    }
}
