using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using FlaUI.Core.EventHandlers;
using FlaUI.UIA3.Patterns;
using TestStack.White.Recording;
using TestStack.White.UIItemEvents;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems
{
    public class CheckBox : Button
    {
        private IAutomationPropertyChangedEventHandler handler;

        protected CheckBox()
        {
        }

        public CheckBox(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener)
        {
        }

        public virtual void Select()
        {
            Checked = true;
        }

        /// <summary>
        /// true when CheckBox is checked
        /// </summary>
        public virtual bool IsSelected
        {
            get { return Checked; }
        }

        public virtual bool Checked
        {
            get { return State.Equals(ToggleState.On); }
            set
            {
                if (Checked == value) return;
                Click();
            }
        }

        /// <summary>
        /// Unchecks the checkbox
        /// </summary>
        public virtual void UnSelect()
        {
            Checked = false;
        }

        public override void HookEvents(IUIItemEventListener eventListener)
        {
            handler = automationElement.RegisterPropertyChangedEvent(
                TreeScope.Element, delegate
                {
                    ActionPerformed();
                    eventListener.EventOccured(new CheckBoxEvent(this));
                }, TogglePattern.ToggleStateProperty);
        }

        public override void UnHookEvents()
        {
            automationElement.RemovePropertyChangedEventHandler(handler);
        }

        public override void SetValue(object value)
        {
            if (!(value is bool))
                throw new UIActionException("Cannot set non bool value to a checkbox. Trying to set: " + value);
            Checked = (bool) value;
        }
    }
}