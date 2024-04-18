namespace AdvisorHealthAPI.Routes;

public static class AdvisorsRoutes
{
    public static void AddRoutesAdvisors(this WebApplication app)
    {
        app.MapGet("advisors", () => "hello world");
    }
}
