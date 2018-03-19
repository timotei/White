using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.Factory;
using TestStack.White.Sessions;
using TestStack.White.UIItems.WindowItems;

namespace TestStack.White.WebBrowser
{
    public class FirefoxFactory : ISpecializedWindowFactory
    {
        public static void Plugin()
        {
            WindowFactory.AddSpecializedWindowFactory(new FirefoxFactory());
        }

        public virtual bool DoesSpecializeInThis(AutomationElement windowElement)
        {
            return windowElement.ClassName.Contains("MozillaUIWindowClass");
        }

        public virtual Window Create(AutomationElement automationElement, InitializeOption initializeOption, WindowSession session)
        {
            return new FirefoxWindow(automationElement, WindowFactory.Desktop, initializeOption, session);
        }
    }
}