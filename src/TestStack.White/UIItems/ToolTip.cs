using System.Windows;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems
{
    public class ToolTip : UIItem
    {
        protected ToolTip() {}
        public ToolTip(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}

        public virtual string Text
        {
            get { return automationElement.Name; }
        }

        public static ToolTip GetFrom(Point point)
        {
            AutomationElement automationElement = Desktop.Automation.FromPoint(point);
            return automationElement.ControlType.Equals(ControlType.ToolTip) ? new ToolTip(automationElement, new NullActionListener()) : null;
        }

        public virtual Point LeftOutside(Rect rect)
        {
            return new Point((int) Bounds.Left - 1, (int) rect.Y);
        }
    }
}