using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.Factory;
using TestStack.White.Sessions;

namespace TestStack.White.WebBrowser
{
    public class InternetExplorerWindow : BrowserWindow
    {
        protected InternetExplorerWindow() {}

        public InternetExplorerWindow(AutomationElement automationElement, WindowFactory windowFactory, InitializeOption option, WindowSession windowSession)
            : base(automationElement, windowFactory, option, windowSession) {}
    }
}