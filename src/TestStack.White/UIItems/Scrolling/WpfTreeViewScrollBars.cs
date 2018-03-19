using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems.Scrolling
{
    public class WpfTreeViewScrollBars : AbstractScrollBars
    {
        private readonly AutomationElement parentElement;
        private readonly IActionListener actionListener;

        public WpfTreeViewScrollBars(AutomationElement parentElement, IActionListener actionListener)
        {
            this.parentElement = parentElement;
            this.actionListener = actionListener;
        }

        public override IHScrollBar Horizontal
        {
            get
            {
                return new WpfTreeViewHScrollBar(parentElement, actionListener);
            }
        }

        public override IVScrollBar Vertical
        {
            get
            {
                return new WpfTreeViewVScrollBar(parentElement, actionListener);
            }
        }

        public override bool CanScroll
        {
            get { return Horizontal.IsScrollable || Vertical.IsScrollable; }
        }
    }
}