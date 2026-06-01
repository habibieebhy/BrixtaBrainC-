using System;
using System.Collections.Generic;

namespace BrixtaOS.Domain.Events
{
    public class EventInstance
    {
        public Guid Id { get; set; }
        public Guid WorkflowInstanceId { get; set; }
        public Guid UserId { get; set; }
        public Guid EventDefinitionId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
