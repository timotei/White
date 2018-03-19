using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestStack.White.AutomationElementSearch
{
    public class DescendantFinder : IDescendantFinder
    {
        private readonly AutomationElement automationElement;

        public DescendantFinder(AutomationElement automationElement)
        {
            if (automationElement == null) throw new ArgumentNullException("automationElement");
            this.automationElement = automationElement;
        }

        public virtual AutomationElement Descendant(AutomationSearchCondition automationSearchCondition)
        {
            return Descendant(automationSearchCondition.Condition);
        }

        public virtual AutomationElement Descendant(ConditionBase condition)
        {
            return automationElement.FindFirst(TreeScope.Descendants, condition);
        }

        public virtual List<AutomationElement> Descendants(AutomationSearchCondition automationSearchCondition)
        {
            var collection = automationElement.FindAll(TreeScope.Descendants, automationSearchCondition.Condition);
            var enumerable = collection.Cast<AutomationElement>();
            return new List<AutomationElement>(enumerable);
        }
    }
}