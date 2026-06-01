using BrixtaOS.Api.Contracts.WorkflowInstances;
using BrixtaOS.Domain.Workflows;
using BrixtaOS.Infrastructure.Storage;

public static class WorkflowInstancesEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/api/workflow-instances/{id}", (Guid id, InMemoryDataStore dataStore) =>
        {
            var workflowInstance = dataStore.GetWorkflowInstance(id);

            if (workflowInstance == null)
            {
                return Results.NotFound(new { Message = "Workflow instance not found" });
            }

            return Results.Ok(workflowInstance);
        });

        app.MapPost("/api/workflow-instances", (
            CreateWorkflowInstanceRequest request,
            InMemoryDataStore dataStore) =>
        {
            var workflowDefinition =
                dataStore.GetWorkflowDefinition(request.WorkflowDefinitionId);

            if (workflowDefinition == null)
            {
                return Results.NotFound(new
                {
                    Message = "Workflow definition not found"
                });
            }

            var initialState = workflowDefinition.States.FirstOrDefault();

            if (initialState == null)
            {
                return Results.BadRequest(new
                {
                    Message = "Workflow has no states"
                });
            }

            var workflowInstance = new WorkflowInstance
            {
                Id = Guid.NewGuid(),
                WorkflowDefinitionId = workflowDefinition.Id,
                CurrentState = initialState.Name
            };

            dataStore.AddWorkflowInstance(workflowInstance);

            return Results.Ok(new
            {
                workflowInstance.Id,
                workflowInstance.CurrentState
            });
        });
    }
}
