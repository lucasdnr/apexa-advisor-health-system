namespace AdvisorHealthAPI.Routes;

public static class AdvisorsRoutes
{
    public static void AddRoutesAdvisors(WebApplication app)
    {
        app.MapGet("advisors", () => "hello world");
    }
}
