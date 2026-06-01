namespace BrixtaOS.Application.Runtime
{
    public class WorkflowValidationResult
    {
        public bool IsValid { get; set; }
        public string? Error { get; set; }

        public WorkflowValidationResult(bool isValid, string? error = null)
        {
            IsValid = isValid;
            Error = error;
        }
    }
}
