using System.Runtime.Serialization;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using FlaUI.UIA3.Identifiers;

namespace TestStack.White.UIItems.Finders
{
    [DataContract]
    public class ControlTypeProperty : AutomationElementProperty
    {
        protected ControlTypeProperty()
        {
        }

        public ControlTypeProperty(ControlType controlType, string displayName)
            : base(controlType, displayName, AutomationObjectIds.ControlTypeProperty) { }

        public override string DisplayValue
        {
            get { return Value.ToString(); }
        }
    }
}