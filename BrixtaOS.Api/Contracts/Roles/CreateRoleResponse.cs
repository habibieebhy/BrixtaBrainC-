namespace BrixtaOS.Api.Contracts.Roles
{
    public sealed record CreateRoleResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public IReadOnlyList<string> AllowedEvents { get; init; }
    }
}
