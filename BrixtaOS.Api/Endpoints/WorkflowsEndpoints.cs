public static class WorkflowsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/api/workflows", () => Results.Ok(new { Message = "Workflow created" }));
        app.MapGet("/api/workflows", () => Results.Ok(new List<object>()));
        app.MapGet("/api/workflows/{id}", (int id) => Results.Ok(new { Id = id, Name = "Sample Workflow" }));
    }
}
