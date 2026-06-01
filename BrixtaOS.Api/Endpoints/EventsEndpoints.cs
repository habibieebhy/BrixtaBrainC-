public static class EventsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/api/events", () => Results.Ok(new { Message = "Event received" }));
    }
}
