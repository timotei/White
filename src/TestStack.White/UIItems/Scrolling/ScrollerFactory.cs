using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems.Scrolling
{
    internal class ScrollerFactory
    {
        internal static IScrollBars CreateBars(AutomationElement parentElement, IActionListener listener)
        {
            var frameworkId = parentElement.Properties.FrameworkId;
            if (frameworkId == WindowsFramework.Wpf.FrameworkId())
                return new WPFScrollBars(parentElement, listener);
            if (frameworkId == WindowsFramework.Silverlight.FrameworkId())
                return new ScrollBars(parentElement, listener, new SilverlightHScrollBarButtonAutomationIds(), new SilverlightVScrollBarButtonAutomationIds());
            return new ScrollBars(parentElement, listener, new DefaultScrollBarButtonAutomationIds(), new DefaultScrollBarButtonAutomationIds());
        }
    }
}
