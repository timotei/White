using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.UIA3.Identifiers;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems
{
    public class Label : UIItem
    {
        protected Label() {}
        public Label(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}

        public virtual string Text
        {
            get { return (string) Property(AutomationObjectIds.NameProperty); }
        }
    }
}