using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems
{
    public class ListViewCell : Label
    {
        protected ListViewCell() {}
        public ListViewCell(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}
    }
}