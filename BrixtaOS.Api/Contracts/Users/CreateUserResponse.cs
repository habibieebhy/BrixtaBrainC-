namespace BrixtaOS.Api.Contracts.Users
{
    public sealed record CreateUserResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public IReadOnlyList<Guid> RoleIds { get; init; }
    }
}
