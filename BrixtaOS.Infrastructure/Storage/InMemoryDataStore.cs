using System;
using System.Collections.Generic;
using BrixtaOS.Domain.Users;
using BrixtaOS.Domain.Roles;
using BrixtaOS.Domain.Events;
using BrixtaOS.Domain.Workflows;

namespace BrixtaOS.Infrastructure.Storage
{
    public class InMemoryDataStore
    {
        private static readonly Lazy<InMemoryDataStore> _instance = new Lazy<InMemoryDataStore>(() => new InMemoryDataStore());

        public static InMemoryDataStore Instance => _instance.Value;

        private Dictionary<Guid, User> _users = new Dictionary<Guid, User>();
        private Dictionary<Guid, Role> _roles = new Dictionary<Guid, Role>();
        private Dictionary<Guid, EventDefinition> _eventDefinitions = new Dictionary<Guid, EventDefinition>();
        private Dictionary<Guid, EventInstance> _eventInstances = new Dictionary<Guid, EventInstance>();
        private Dictionary<Guid, WorkflowDefinition> _workflowDefinitions = new Dictionary<Guid, WorkflowDefinition>();
        private Dictionary<Guid, WorkflowInstance> _workflowInstances = new Dictionary<Guid, WorkflowInstance>();

        // User methods
        public void AddUser(User user) => _users[user.Id] = user;
        public User? GetUser(Guid id) => _users.TryGetValue(id, out var user) ? user : null;

        // Role methods
        public void AddRole(Role role) => _roles[role.Id] = role;
        public Role? GetRole(Guid id) => _roles.TryGetValue(id, out var role) ? role : null;

        // EventDefinition methods
        public void AddEventDefinition(EventDefinition eventDefinition) => _eventDefinitions[eventDefinition.Id] = eventDefinition;
        public EventDefinition? GetEventDefinition(Guid id) => _eventDefinitions.TryGetValue(id, out var eventDefinition) ? eventDefinition : null;

        // EventInstance methods
        public void AddEventInstance(EventInstance eventInstance) => _eventInstances[eventInstance.Id] = eventInstance;
        public EventInstance? GetEventInstance(Guid id) => _eventInstances.TryGetValue(id, out var eventInstance) ? eventInstance : null;

        // WorkflowDefinition methods
        public void AddWorkflowDefinition(WorkflowDefinition workflowDefinition) => _workflowDefinitions[workflowDefinition.Id] = workflowDefinition;
        public WorkflowDefinition? GetWorkflowDefinition(Guid id) => _workflowDefinitions.TryGetValue(id, out var workflowDefinition) ? workflowDefinition : null;

        // WorkflowInstance methods
        public void AddWorkflowInstance(WorkflowInstance workflowInstance) => _workflowInstances[workflowInstance.Id] = workflowInstance;
        public WorkflowInstance? GetWorkflowInstance(Guid id) => _workflowInstances.TryGetValue(id, out var workflowInstance) ? workflowInstance : null;
    }
}
