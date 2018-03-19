using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Identifiers;

namespace TestStack.White.UIA
{
    public class AutomationPatterns : List<PatternId>
    {
        public AutomationPatterns(params PatternId[] collection) : base(collection) {}

        public AutomationPatterns(AutomationElement automationElement)
        {
            AddRange(automationElement.GetSupportedPatterns());
        }

        public virtual bool HasPattern(PatternId automationPattern)
        {
            return this.Any(pattern => pattern.Id.Equals(automationPattern.Id));
        }
    }
}