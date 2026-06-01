using BrixtaOS.Domain.Organizations;

namespace BrixtaOS.Domain.Events
{
    public class EventDefinition
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }
}
