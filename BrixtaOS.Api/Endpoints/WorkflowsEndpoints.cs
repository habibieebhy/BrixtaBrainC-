using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using BrixtaOS.Api.Contracts.Workflows;
using BrixtaOS.Domain.Workflows;
using BrixtaOS.Infrastructure.Storage;

public static class WorkflowsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/api/workflows",
        async (HttpContext context, InMemoryDataStore dataStore) =>
        {
            var request = await context.Request.ReadFromJsonAsync<CreateWorkflowRequest>();

            if (request == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            var workflow = new WorkflowDefinition
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                States = request.States,
                Transitions = request.Transitions
            };

            dataStore.AddWorkflowDefinition(workflow);

            await context.Response.WriteAsJsonAsync(new
            {
                workflow.Id,
                workflow.Name
            });
        });

        app.MapGet("/api/workflows/{id}",
        (Guid id, InMemoryDataStore dataStore) =>
        {
            var workflow = dataStore.GetWorkflowDefinition(id);

            return workflow is null
                ? Results.NotFound()
                : Results.Ok(workflow);
        });
    }
}