namespace BrixtaOS.Api.Contracts.Users
{
    public sealed record CreateUserRequest
    {
        public string Name { get; init; }
    }
}
