using System.Collections.Generic;

namespace BrixtaOS.Api.Contracts.Roles
{
    public class CreateRoleResponse
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required List<string> AllowedEvents { get; init; }
    }
}
