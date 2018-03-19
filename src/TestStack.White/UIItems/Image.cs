using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.Recording;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems
{
    public class Image : UIItem
    {
        protected Image() {}
        public Image(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}

        public override void HookEvents(IUIItemEventListener eventListener)
        {
            HookClickEvent(eventListener);
        }

        public override void UnHookEvents()
        {
            UnHookClickEvent();
        }
    }
}