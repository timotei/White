using System.Windows;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems.Scrolling
{
    [PlatformSpecificItem(ReferAsType = typeof(IVScrollBar))]
    public class WpfTreeViewVScrollBar : WpfTreeViewScrollBar, IVScrollBar
    {
        private readonly IActionListener actionListener;

        public WpfTreeViewVScrollBar(AutomationElement parent, IActionListener actionListener)
            : base(parent)
        {
            this.actionListener = actionListener;
        }

        protected override double ScrollPercentage
        {
            get { return ScrollPattern.VerticalViewSize; }
        }

        public override double Value
        {
            get { return ScrollPattern.VerticalScrollPercent; }
        }

        public override Rect Bounds
        {
            get { return Rect.Empty; }
        }

        public virtual void ScrollUp()
        {
            Scroll(ScrollAmount.SmallDecrement);
        }

        public virtual void ScrollDown()
        {
            Scroll(ScrollAmount.SmallIncrement);
        }

        public virtual void ScrollUpLarge()
        {
            Scroll(ScrollAmount.LargeDecrement);
        }

        public virtual void ScrollDownLarge()
        {
            Scroll(ScrollAmount.LargeIncrement);
        }

        public virtual bool IsScrollable
        {
            get { return ScrollPattern.VerticallyScrollable; }
        }

        public virtual bool IsNotMinimum
        {
            get { return Value > 0; }
        }

        public override void SetToMinimum()
        {
            ScrollPattern.SetScrollPercent(ScrollPattern.HorizontalScrollPercent, 0);
        }

        public override void SetToMaximum()
        {
            ScrollPattern.SetScrollPercent(ScrollPattern.HorizontalScrollPercent, 100);
        }

        protected virtual void Scroll(ScrollAmount amount)
        {
            if (!IsScrollable) return;
            switch (amount)
            {
                case ScrollAmount.LargeDecrement:
                    ScrollPattern.SetScrollPercent(
                        ScrollPattern.HorizontalScrollPercent,
                        ValidPercentage(ScrollPattern.VerticalScrollPercent - ScrollPercentage));
                    break;
                case ScrollAmount.SmallDecrement:
                    ScrollPattern.SetScrollPercent(
                        ScrollPattern.HorizontalScrollPercent,
                        ValidPercentage(ScrollPattern.VerticalScrollPercent - SmallPercentage()));
                    break;
                case ScrollAmount.LargeIncrement:
                    ScrollPattern.SetScrollPercent(
                        ScrollPattern.HorizontalScrollPercent,
                        ValidPercentage(ScrollPattern.VerticalScrollPercent + ScrollPercentage));
                    break;
                case ScrollAmount.SmallIncrement:
                    ScrollPattern.SetScrollPercent(
                        ScrollPattern.HorizontalScrollPercent,
                        ValidPercentage(ScrollPattern.VerticalScrollPercent + SmallPercentage()));
                    break;
            }
            actionListener.ActionPerformed(Action.Scroll);
        }
    }
}