namespace BrixtaOS.Domain.Workflows
{
    public class WorkflowTransition
    {
        public string EventName { get; set; }
        public string FromState { get; set; }
        public string ToState { get; set; }
    }
}
