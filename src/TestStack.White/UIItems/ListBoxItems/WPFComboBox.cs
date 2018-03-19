using System.Linq;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.UIA3.Patterns;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.Scrolling;

namespace TestStack.White.UIItems.ListBoxItems
{
    [PlatformSpecificItem]
    public class WPFComboBox : ComboBox
    {
        protected WPFComboBox() {}
        public WPFComboBox(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}

        public override IScrollBars ScrollBars
        {
            get { return scrollBars ?? (scrollBars = new WPFScrollBars(AutomationElement, ActionListener)); }
        }

        public override VerticalSpan VerticalSpan
        {
            get
            {
                var scrollPattern = (ScrollPattern) Pattern(ScrollPattern.Pattern);
                var bounds = Bounds;
                var firstVisibleItem = (Items.FirstOrDefault(i=>!i.IsOffScreen) ?? Items[0]).Bounds;
                var lastItem = Items[Items.Count - 1].Bounds;
                var verticalViewSize = scrollPattern.VerticalViewSize;
                var calculator = new WPFComboBoxVerticalSpanCalculator(bounds, firstVisibleItem, lastItem, verticalViewSize);
                return calculator.VerticalSpan;
            }
        }
    }
}