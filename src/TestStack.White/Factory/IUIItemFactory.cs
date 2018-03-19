using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.Factory
{
    public interface IUIItemFactory
    {
        IUIItem Create(AutomationElement automationElement, IActionListener actionListener);
    }
}