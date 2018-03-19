using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using FlaUI.Core.Patterns;
using FlaUI.UIA3.Patterns;
using TestStack.White.AutomationElementSearch;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems
{
    public class Spinner : UIItem
    {
        private readonly AutomationElementFinder finder;
        protected Spinner() {}

        public Spinner(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener)
        {
            finder = new AutomationElementFinder(automationElement);
        }

        public virtual double Value
        {
            get
            {
                IValuePattern valuePattern = GetValuePattern();
                string value = valuePattern.Value;
                return double.Parse(value);
            }
            set { GetValuePattern().SetValue(value.ToString()); }
        }

        private IValuePattern GetValuePattern()
        {
            AutomationElement spinnerElementContainingValue =
                finder.FindChildRaw(AutomationSearchCondition.ByAutomationId(automationElement.AutomationId).OfControlType(ControlType.Spinner));
            if (spinnerElementContainingValue == null) throw new WhiteAssertionException("Could not find Raw Spinner Element containing the value");
            return spinnerElementContainingValue.Patterns.Value.PatternOrDefault;
        }

        public virtual void Increment()
        {
            Button button = GetButton("SmallIncrement");
            button.Click();
        }

        private Button GetButton(string buttonName)
        {
            return new Button(finder.Child(AutomationSearchCondition.ByControlType(ControlType.Button).WithAutomationId(buttonName)), actionListener);
        }

        public virtual void Decrement()
        {
            GetButton("SmallDecrement").Click();
        }
    }
}