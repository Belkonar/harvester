namespace harvester;

public static class Routes
{
    public static void SetupRoutes(this WebApplication web)
    {
        web.MapGet("/health", () => new
        {
            Status = "Ok"
        });
    }
}