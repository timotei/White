using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.ListViewItems;

namespace TestStack.White.UIItems
{
    [PlatformSpecificItem]
    public class WinFormTextBox : TextBox
    {
        public WinFormTextBox(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}
        public WinFormTextBox() {}

        public virtual ISuggestionList SuggestionList
        {
            get { return SuggestionListView.WaitAndFind(actionListener); }
        }
    }
}