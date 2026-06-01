namespace BrixtaOS.Api.Contracts.Workflows
{
    public sealed record CreateWorkflowResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
