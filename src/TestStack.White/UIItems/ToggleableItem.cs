using System;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using FlaUI.UIA3.Patterns;

namespace TestStack.White.UIItems
{
    public class ToggleableItem : UIItem
    {
        public ToggleableItem(UIItem uiItem) : base(uiItem.AutomationElement, uiItem.ActionListener)
        {
        }

        public virtual ToggleState State
        {
            get { return AutomationElement.Patterns.Toggle.PatternOrDefault.ToggleState; }
            set
            {
                for (int i = 0; i < Enum.GetNames(typeof (ToggleState)).Length; i++)
                {
                    if (State == value) break;
                    Toggle();
                    ActionPerformed();
                }
            }
        }

        public virtual void Toggle()
        {
            var pattern = AutomationElement.Patterns.Toggle.PatternOrDefault; 
            pattern.Toggle();
        }
    }
}