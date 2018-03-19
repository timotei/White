using System.Linq;
using System.Windows;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.UIA3.Patterns;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.Scrolling;

namespace TestStack.White.UIItems.ListBoxItems
{
    [PlatformSpecificItem]
    public class SilverlightComboBox : ComboBox
    {
        protected SilverlightComboBox() {}
        public SilverlightComboBox(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) { }

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
                var firstVisibleItem = Items.First(i=>!i.IsOffScreen).Bounds;
                var lastItem = Items.Last(i=>i.Bounds != Rect.Empty).Bounds;
                var verticalViewSize = scrollPattern.VerticalViewSize;
                var calculator = new SilverlightComboBoxVerticalSpanCalculator(bounds, firstVisibleItem, lastItem, verticalViewSize);
                return calculator.VerticalSpan;
            }
        }
    }
}