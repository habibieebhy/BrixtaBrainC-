using BrixtaOS.Domain.Organizations;

namespace BrixtaOS.Domain.Workflows
{
    public class WorkflowDefinition
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<WorkflowState> States { get; set; } = new();
        public List<WorkflowTransition> Transitions { get; set; } = new();
        public Guid OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }
}
