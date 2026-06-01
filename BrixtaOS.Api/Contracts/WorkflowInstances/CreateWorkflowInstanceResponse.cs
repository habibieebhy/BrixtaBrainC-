namespace BrixtaOS.Api.Contracts.WorkflowInstances
{
    public sealed record CreateWorkflowInstanceResponse
    {
        public Guid Id { get; init; }
        public string CurrentState { get; init; }
    }
}
