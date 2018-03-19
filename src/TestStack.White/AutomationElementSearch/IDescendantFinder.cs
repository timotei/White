using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Conditions;
using System.Collections.Generic;

namespace TestStack.White.AutomationElementSearch
{
    public interface IDescendantFinder
    {
        AutomationElement Descendant(AutomationSearchCondition automationSearchCondition);

        AutomationElement Descendant(ConditionBase condition);

        List<AutomationElement> Descendants(AutomationSearchCondition automationSearchCondition);
    }
}