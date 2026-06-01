using System.Collections.Generic;

namespace BrixtaOS.Api.Contracts.Roles
{
    public class CreateRoleRequest
    {
        public required string Name { get; init; }
        public required List<string> AllowedEvents { get; init; }
    }
}
