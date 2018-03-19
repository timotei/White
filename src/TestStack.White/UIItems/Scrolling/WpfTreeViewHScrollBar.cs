using System.Windows;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems.Scrolling
{
    [PlatformSpecificItem(ReferAsType = typeof(IHScrollBar))]
    public class WpfTreeViewHScrollBar : WpfTreeViewScrollBar, IHScrollBar
    {
        private readonly IActionListener actionListener;

        public WpfTreeViewHScrollBar(AutomationElement parent, IActionListener actionListener)
            : base(parent)
        {
            this.actionListener = actionListener;
        }

        public override double Value
        {
            get { return ScrollPattern.HorizontalScrollPercent; }
        }

        protected override double ScrollPercentage
        {
            get { return ScrollPattern.HorizontalViewSize; }
        }

        public override Rect Bounds
        {
            get { return Rect.Empty; }
        }

        protected virtual void Scroll(ScrollAmount amount)
        {
            if (!IsScrollable) return;
            switch (amount)
            {
                case ScrollAmount.LargeDecrement:
                    ScrollPattern.SetScrollPercent(
                        ValidPercentage(ScrollPattern.HorizontalScrollPercent - ScrollPercentage),
                        ScrollPattern.VerticalScrollPercent);
                    break;
                case ScrollAmount.SmallDecrement:
                    ScrollPattern.SetScrollPercent(
                        ValidPercentage(ScrollPattern.HorizontalScrollPercent - SmallPercentage()),
                        ScrollPattern.VerticalScrollPercent);
                    break;
                case ScrollAmount.LargeIncrement:
                    ScrollPattern.SetScrollPercent(
                        ValidPercentage(ScrollPattern.HorizontalScrollPercent + ScrollPercentage),
                        ScrollPattern.VerticalScrollPercent);
                    break;
                case ScrollAmount.SmallIncrement:
                    ScrollPattern.SetScrollPercent(
                        ValidPercentage(ScrollPattern.HorizontalScrollPercent + SmallPercentage()),
                        ScrollPattern.VerticalScrollPercent);
                    break;
            }
            actionListener.ActionPerformed(Action.WindowMessage);
        }

        public virtual void ScrollLeft()
        {
            Scroll(ScrollAmount.SmallDecrement);
        }

        public virtual void ScrollRight()
        {
            Scroll(ScrollAmount.SmallIncrement);
        }

        public virtual void ScrollLeftLarge()
        {
            Scroll(ScrollAmount.LargeDecrement);
        }

        public virtual void ScrollRightLarge()
        {
            Scroll(ScrollAmount.LargeIncrement);
        }

        public virtual bool IsScrollable
        {
            get { return ScrollPattern.HorizontallyScrollable; }
        }

        public override void SetToMinimum()
        {
            ScrollPattern.SetScrollPercent(0, ScrollPattern.VerticalScrollPercent);
        }

        public override void SetToMaximum()
        {
            ScrollPattern.SetScrollPercent(100, ScrollPattern.VerticalScrollPercent);
        }
    }
}