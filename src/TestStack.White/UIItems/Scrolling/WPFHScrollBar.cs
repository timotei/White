using System;
using System.Windows;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using TestStack.White.UIItems.Actions;
using Action = TestStack.White.UIItems.Actions.Action;

namespace TestStack.White.UIItems.Scrolling
{
    [PlatformSpecificItem(ReferAsType = typeof (IHScrollBar))]
    public class WpfHScrollBar : WpfScrollBar, IHScrollBar
    {
        private readonly IActionListener actionListener;

        public WpfHScrollBar(AutomationElement parent, IActionListener actionListener) : base(parent)
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
            ScrollPattern.Scroll(amount, 0);
            actionListener.ActionPerformed(Action.WindowMessage);
            throw new InvalidOperationException("Verify the above, we scroll by enum value instead of actula value?");
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
