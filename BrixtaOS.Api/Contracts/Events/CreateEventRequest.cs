namespace BrixtaOS.Api.Contracts.Events
{
    public class CreateEventRequest
    {
        public required Guid WorkflowInstanceId { get; init; }
        public required Guid UserId { get; init; }
        public required string EventName { get; init; }
    }
}
