using System.Collections.Generic;

namespace BrixtaOS.Api.Contracts.Users
{
    public class CreateUserResponse
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required List<Guid> RoleIds { get; init; }
    }
}
