namespace BrixtaOS.Api.Contracts.Events
{
    public sealed record CreateEventResponse
    {
        public bool Accepted { get; init; }
        public string NewState { get; init; }
    }
}
