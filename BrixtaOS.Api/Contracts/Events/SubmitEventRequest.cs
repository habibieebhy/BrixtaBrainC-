namespace BrixtaOS.Api.Contracts.Events
{
    public sealed record SubmitEventRequest
    {
        public Guid WorkflowInstanceId { get; init; }
        public required string EventName { get; init; } // Added 'required' modifier to make it non-nullable
    }
}
