using System;
using System.Collections.Generic;
using System.Linq;
using BrixtaOS.Domain.Workflows;

namespace BrixtaOS.Application.Runtime
{
    public class WorkflowRuntime
    {
        public WorkflowValidationResult CanApplyEvent(
            WorkflowDefinition definition,
            WorkflowInstance instance,
            string eventName)
        {
            // Find current state
            var currentState = instance.CurrentState;
            
            // Verify current state exists
            if (!definition.States.Any(s => s.Name == currentState))
            {
                return new WorkflowValidationResult(false, "Current state does not exist.");
            }

            // Verify event is allowed in that state
            var currentStateDefinition = definition.States.First(s => s.Name == currentState);
            if (!currentStateDefinition.AllowedEvents.Contains(eventName))
            {
                return new WorkflowValidationResult(false, "Event is not allowed in the current state.");
            }

            // Verify matching transition exists
            var transition = definition.Transitions.FirstOrDefault(t => t.FromState == currentState && t.EventName == eventName);
            if (transition == null)
            {
                return new WorkflowValidationResult(false, "Matching transition does not exist.");
            }

            return new WorkflowValidationResult(true);
        }

        public WorkflowValidationResult ApplyEvent(
            WorkflowDefinition definition,
            WorkflowInstance instance,
            string eventName)
        {
            // Find matching transition
            var currentState = instance.CurrentState;
            var transition = definition.Transitions.FirstOrDefault(t => t.FromState == currentState && t.EventName == eventName);

            if (transition == null)
            {
                return new WorkflowValidationResult(false, "Matching transition does not exist.");
            }

            // Update CurrentState
            instance.CurrentState = transition.ToState;

            return new WorkflowValidationResult(true);
        }

        public WorkflowValidationResult CreateWorkflow(
            string name,
            List<WorkflowState> states,
            List<WorkflowTransition> transitions)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new WorkflowValidationResult(false, "Name cannot be empty.");
            }

            if (!states.Any())
            {
                return new WorkflowValidationResult(false, "At least one state is required.");
            }

            if (!transitions.Any())
            {
                return new WorkflowValidationResult(false, "At least one transition is required.");
            }

            var workflowDefinition = new WorkflowDefinition
            {
                Id = Guid.NewGuid(),
                Name = name,
                States = states,
                Transitions = transitions
            };

            // Validate states and transitions
            foreach (var state in states)
            {
                if (!transitions.Any(t => t.FromState == state.Name))
                {
                    return new WorkflowValidationResult(false, $"No transition exists from state: {state.Name}");
                }
            }

            return new WorkflowValidationResult(true);
        }
    }
}
