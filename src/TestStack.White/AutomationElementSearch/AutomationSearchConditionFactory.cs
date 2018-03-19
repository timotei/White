using System.Collections.Generic;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;

namespace TestStack.White.AutomationElementSearch
{
    public class AutomationSearchConditionFactory
    {
        public virtual List<AutomationSearchCondition> GetWindowSearchConditions(int processId)
        {
            return new List<AutomationSearchCondition>
            {
                AutomationSearchCondition.GetWindowSearchCondition(processId, ControlType.Window),
                AutomationSearchCondition.GetWindowSearchCondition(processId, ControlType.Pane)
            };
        }
    }
}