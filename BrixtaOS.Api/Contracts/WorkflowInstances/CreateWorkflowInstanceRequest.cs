namespace BrixtaOS.Api.Contracts.WorkflowInstances
{
    public sealed record CreateWorkflowInstanceRequest
    {
        public Guid WorkflowDefinitionId { get; init; }
    }
}
