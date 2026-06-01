using BrixtaOS.Domain.Organizations;

namespace BrixtaOS.Domain.Roles
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<string> AllowedEvents { get; set; } = new();
        public Guid OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }
}
