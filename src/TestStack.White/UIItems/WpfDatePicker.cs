using System;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.UIA3.Patterns;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems
{
    [PlatformSpecificItem]
    public class WpfDatePicker : DateTimePicker
    {
        protected WpfDatePicker() {}
        public WpfDatePicker(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) { }

        public override void SetDate(DateTime? dateTime, DateFormat dateFormat)
        {
            var valuePattern = AutomationElement.Patterns.Value.Pattern;
            valuePattern.SetValue(dateTime != null ? dateTime.Value.ToShortDateString() : "");
        }
    }
}