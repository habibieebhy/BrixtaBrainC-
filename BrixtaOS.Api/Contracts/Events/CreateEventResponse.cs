namespace BrixtaOS.Api.Contracts.Events
{
    public class CreateEventResponse
    {
        public required bool Accepted { get; init; }
        public required string NewState { get; init; }
    }
}
