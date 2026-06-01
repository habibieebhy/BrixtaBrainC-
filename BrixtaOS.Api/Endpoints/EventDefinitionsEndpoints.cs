public static class EventDefinitionsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/api/event-definitions", () => Results.Ok(new { Message = "Event definition created" }));
        app.MapGet("/api/event-definitions", () => Results.Ok(new List<object>()));
    }
}
