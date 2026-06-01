using BrixtaOS.Domain.Organizations;

namespace BrixtaOS.Domain.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Guid> RoleIds { get; set; } = new();
        public Guid OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }
}
