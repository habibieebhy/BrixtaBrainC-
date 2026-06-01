public static class WorkflowInstancesEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/api/workflow-instances", () => Results.Ok(new { Message = "Workflow instance created" }));
        app.MapGet("/api/workflow-instances/{id}", (int id) => Results.Ok(new { Id = id, Status = "Running" }));
    }
}
