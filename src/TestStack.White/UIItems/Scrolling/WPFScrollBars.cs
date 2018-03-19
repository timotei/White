using System.Collections.ObjectModel;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Identifiers;
using FlaUI.UIA3.Patterns;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems.Scrolling
{
    public class WPFScrollBars : AbstractScrollBars
    {
        private readonly AutomationElement parentElement;
        private readonly IActionListener actionListener;

        public WPFScrollBars(AutomationElement parentElement, IActionListener actionListener)
        {
            this.parentElement = parentElement;
            this.actionListener = actionListener;
        }

        public override IHScrollBar Horizontal
        {
            get
            {
                var patterns = new Collection<PatternId>(parentElement.GetSupportedPatterns());
                return patterns.Contains(ScrollPattern.Pattern)
                           ? (IHScrollBar)new WpfHScrollBar(parentElement, actionListener)
                           : new NullHScrollBar();
            }
        }

        public override IVScrollBar Vertical
        {
            get
            {
                var patterns = new Collection<PatternId>(parentElement.GetSupportedPatterns());
                return patterns.Contains(ScrollPattern.Pattern)
                           ? (IVScrollBar)new WpfVScrollBar(parentElement, actionListener)
                           : new NullVScrollBar();
            }
        }

        public override bool CanScroll
        {
            get { return Horizontal.IsScrollable || Vertical.IsScrollable; }
        }
    }
}