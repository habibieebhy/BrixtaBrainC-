namespace BrixtaOS.Api.Contracts.Events
{
    public sealed record CreateEventRequest
    {
        public Guid WorkflowInstanceId { get; init; }
        public Guid UserId { get; init; }
        public string EventName { get; init; }
    }
}
