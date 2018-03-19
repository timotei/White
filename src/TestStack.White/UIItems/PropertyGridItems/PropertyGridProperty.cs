using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Patterns;
using FlaUI.UIA3.Patterns;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems.PropertyGridItems
{
    public class PropertyGridProperty : UIItem
    {
        private readonly PropertyGridElementFinder gridElementFinder;

        protected PropertyGridProperty() {}
        public PropertyGridProperty(AutomationElement automationElement, PropertyGridElementFinder gridElementFinder, IActionListener actionListener) : base(automationElement, actionListener)
        {
            this.gridElementFinder = gridElementFinder;
        }

        public virtual string Value
        {
            get { return ValuePattern().Value; }
            set { ValuePattern().SetValue(value); }
        }

        private IValuePattern ValuePattern()
        {
            return AutomationElement.Patterns.Value.PatternOrDefault;
        }

        public virtual bool IsReadOnly
        {
            get { return ValuePattern().IsReadOnly; }
        }

        public virtual string Text
        {
            get { return Name; }
        }

        public virtual void BrowseForValue()
        {
            Click();
            AutomationElement browseButtonElement = gridElementFinder.FindBrowseButton();
            if (browseButtonElement == null) throw new WhiteException(string.Format("Property {0} isn't browsable.", Text));
            var button = new Button(browseButtonElement, actionListener);
            button.Click();
        }
    }
}