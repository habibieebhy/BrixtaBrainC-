using BrixtaOS.Domain.Organizations;

namespace BrixtaOS.Domain.Workflows
{
    public class WorkflowInstance
    {
        public Guid Id { get; set; }
        public Guid WorkflowDefinitionId { get; set; }
        public string CurrentState { get; set; } = null!;
        public Guid OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }
}
