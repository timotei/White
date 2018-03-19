using System.Diagnostics;
using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.Configuration;

namespace TestStack.White.UIItems.Actions
{
    public class ProcessActionListener : IActionListener
    {
        private readonly Process process;

        public ProcessActionListener(AutomationElement automationElement)
        {
            int processId = automationElement.Properties.ProcessId;
            process = Process.GetProcessById(processId);
        }

        public virtual void ActionPerforming(UIItem uiItem) {}

        public virtual void ActionPerformed(Action action)
        {
            process.WaitForInputIdle(CoreAppXmlConfiguration.Instance.BusyTimeout);
        }
    }
}