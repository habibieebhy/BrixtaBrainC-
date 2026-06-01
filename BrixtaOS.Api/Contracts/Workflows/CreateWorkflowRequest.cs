using System;
using System.Collections.Generic;
using BrixtaOS.Domain.Workflows;

namespace BrixtaOS.Api.Contracts.Workflows
{
    public class CreateWorkflowRequest
    {
        public required string Name { get; set; }
        public required List<WorkflowState> States { get; set; }
        public required List<WorkflowTransition> Transitions { get; set; }
    }
}
