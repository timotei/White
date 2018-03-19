using System;
using System.Windows;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using TestStack.White.UIItems.Actions;
using Action = TestStack.White.UIItems.Actions.Action;

namespace TestStack.White.UIItems.Scrolling
{
    [PlatformSpecificItem(ReferAsType = typeof (IVScrollBar))]
    public class WpfVScrollBar : WpfScrollBar, IVScrollBar
    {
        private readonly IActionListener actionListener;

        public WpfVScrollBar(AutomationElement parent, IActionListener actionListener) : base(parent)
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
            ScrollPattern.Scroll(0, amount);
            actionListener.ActionPerformed(Action.Scroll);
            throw new InvalidOperationException("Verifyu this (amount vs value)");
        }
    }
}
