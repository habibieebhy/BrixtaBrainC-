using System;
using System.Collections.Generic;

namespace BrixtaOS.Domain.Workflows
{
    public class WorkflowState
    {
        public string Name { get; set; }
        public List<string> AllowedEvents { get; set; } = new List<string>();
    }
}
