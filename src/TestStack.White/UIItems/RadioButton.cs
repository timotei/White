using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using FlaUI.Core.EventHandlers;
using FlaUI.UIA3.Patterns;
using TestStack.White.Recording;
using TestStack.White.UIItemEvents;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems
{
    public class RadioButton : SelectionItem
    {
        private IAutomationEventHandler handler;

        protected RadioButton() {}
        public RadioButton(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}

        public override void HookEvents(IUIItemEventListener eventListener)
        {
            handler = automationElement.RegisterEvent(SelectionItemPattern.ElementSelectedEvent, TreeScope.Element, delegate { eventListener.EventOccured(new RadioButtonEvent(this)); });
        }

        public override void UnHookEvents()
        {
            automationElement.RemoveAutomationEventHandler(SelectionItemPattern.ElementSelectedEvent, handler);
        }

        public override void SetValue(object value)
        {
            if (!(value is bool)) throw new UIActionException("Cannot set non bool value to a RadioButton. Trying to set: " + value);
            IsSelected = (bool) value;
        }
    }
}