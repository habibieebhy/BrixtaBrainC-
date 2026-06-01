using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrixtaOS.Api.Contracts.Events;
using BrixtaOS.Application.Runtime;
using BrixtaOS.Infrastructure.Storage;

public static class EventsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/api/events", async (SubmitEventRequest request, InMemoryDataStore dataStore, WorkflowRuntime workflowRuntime) =>
        {
            var workflowInstance = dataStore.GetWorkflowInstance(request.WorkflowInstanceId);
            if (workflowInstance == null)
            {
                return Results.NotFound(new { Message = "Workflow instance not found" });
            }

            var workflowDefinition = dataStore.GetWorkflowDefinition(workflowInstance.WorkflowDefinitionId); // Ensure WorkflowInstance has DefinitionId
            if (workflowDefinition == null)
            {
                return Results.NotFound(new { Message = "Workflow definition not found" });
            }

            var eventResult = workflowRuntime.ApplyEvent(
                workflowDefinition,
                workflowInstance,
                request.EventName);
            if (!eventResult.IsValid)
            {
                return Results.BadRequest(new { Message = eventResult.Error });
            }

            dataStore.AddWorkflowInstance(workflowInstance);

            return Results.Ok(new { Accepted = true, NewState = workflowInstance.CurrentState });
        });
    }
}
