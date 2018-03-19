using System.Windows;
using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.Recording;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems
{
    public class Hyperlink : UIItem
    {
        protected Hyperlink() {}
        public Hyperlink(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}

        public virtual void Click(int xOffset, int yOffset)
        {
            double x = automationElement.BoundingRectangle.X + xOffset;
            double y = automationElement.BoundingRectangle.Y + yOffset;
            mouse.Click(new Point((int) x, (int) y), actionListener);
        }

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