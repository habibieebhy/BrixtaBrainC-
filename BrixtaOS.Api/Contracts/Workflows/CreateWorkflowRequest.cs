namespace BrixtaOS.Api.Contracts.Workflows
{
    public sealed record CreateWorkflowRequest
    {
        public string Name { get; init; }
        public IReadOnlyList<string> States { get; init; }
    }
}
