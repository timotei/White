using System.Windows;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Patterns;
using FlaUI.UIA3.Patterns;

namespace TestStack.White.UIItems.Scrolling
{
    public abstract class WpfScrollBar : IScrollBar
    {
        protected readonly IScrollPattern ScrollPattern;

        protected WpfScrollBar(AutomationElement parent)
        {
            ScrollPattern = parent.Patterns.Scroll.PatternOrDefault;
        }

        public abstract double Value { get; }
        protected abstract double ScrollPercentage { get; }

        public virtual double MinimumValue
        {
            get { return 0; }
        }

        public virtual double MaximumValue
        {
            get { return 100; }
        }

        public abstract void SetToMinimum();
        public abstract void SetToMaximum();
        public abstract Rect Bounds { get; }
    }
}
