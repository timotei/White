using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using FlaUI.Core.EventHandlers;
using FlaUI.UIA3.Patterns;
using TestStack.White.Recording;
using TestStack.White.UIItemEvents;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems.ListBoxItems
{
    public class ListBox : ListControl
    {
        private IAutomationPropertyChangedEventHandler handler;

        protected ListBox() {}
        public ListBox(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}

        public virtual bool IsChecked(string itemText)
        {
            return Item(itemText).Checked;
        }

        public virtual void Check(string itemText)
        {
            Item(itemText).Check();
        }

        public virtual bool IsSelected(string itemText)
        {
            return Item(itemText).IsSelected;
        }

        public virtual void UnCheck(string itemText)
        {
            Item(itemText).UnCheck();
        }

        public override void HookEvents(IUIItemEventListener eventListener)
        {
            handler = automationElement.RegisterPropertyChangedEvent(
                TreeScope.Descendants, (sender, _, value) =>
                {
                    if (value.Equals(1)) return;
                    eventListener.EventOccured(new ListBoxEvent(this, SelectedItemText));
                }, SelectionItemPattern.IsSelectedProperty);
        }

        public override void UnHookEvents()
        {
            automationElement.RemovePropertyChangedEventHandler(handler);
        }
    }
}