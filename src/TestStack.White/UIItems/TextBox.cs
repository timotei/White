using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using FlaUI.Core.EventHandlers;
using FlaUI.UIA3.Patterns;
using TestStack.White.Recording;
using TestStack.White.UIA;
using TestStack.White.UIItemEvents;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems
{
    public class TextBox : UIItem, IScrollable
    {
        private IAutomationPropertyChangedEventHandler handler;
        protected TextBox() {}
        public TextBox(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}

        /// <summary>
        /// Enters the text in the textbox. The text would be cleared first. This is not as good performing as the BulkText method. 
        /// This does raise all keyboard events - that means that your string will consist of letters that match the letters
        /// of your string but in current input language.
        /// </summary>
        public virtual string Text
        {
            get
            {
                if (automationElement.Properties.IsPassword)
                    throw new WhiteException("Text cannot be retrieved from textbox which has secret text (e.g. password) stored in it");
                var pattern = automationElement.Patterns.Value.PatternOrDefault;
                if (pattern != null) return pattern.Value;

                var textPattern = automationElement.Patterns.Text.PatternOrDefault;
                if (textPattern != null) return textPattern.DocumentRange.GetText(int.MaxValue);

                throw new WhiteException(string.Format("AutomationElement for {0} supports neither ValuePattern or TextPattern", ToString()));
            }
            set { Enter(value); }
        }

        /// <summary>
        /// Sets the text in the textbox. The text would be cleared first. This is a better performing than the Text method. This doesn't raise all keyboard events.
        /// The string will be set exactly as it is in your code.
        /// </summary>
        public virtual string BulkText
        {
            set
            {
                try
                {
                    var pattern = automationElement.Patterns.Value.PatternOrDefault;
                    if (pattern != null) pattern.SetValue(value);
                    else
                    {
                        Logger.WarnFormat("BulkText failed, {0} does not support ValuePattern. Trying Text", ToString());
                        Text = value;
                        actionListener.ActionPerformed(Action.WindowMessage);
                    }
                }
                catch (System.InvalidOperationException)
                {
                    Logger.Warn("BulkText failed, now trying Text on " + ToString());
                    Text = value;
                    actionListener.ActionPerformed(Action.WindowMessage);
                }
            }
            get { return Text; }
        }

        public virtual bool IsReadOnly
        {
            get { return AutomationElement.Patterns.Value.PatternOrDefault.IsReadOnly; }
        }

        public virtual void ClickAtRightEdge()
        {
            mouse.Click(Bounds.ImmediateInteriorEast(), actionListener);
        }

        public virtual void ClickAtCenter()
        {
            mouse.Click(Bounds.Center(), actionListener);
        }

        public override void HookEvents(IUIItemEventListener eventListener)
        {
            handler = automationElement.RegisterPropertyChangedEvent(
                TreeScope.Element, delegate { eventListener.EventOccured(new TextBoxEvent(this)); },
                ValuePattern.ValueProperty);
        }

        public override void UnHookEvents()
        {
            automationElement.RemovePropertyChangedEventHandler(handler);
        }

        //TODO: This should be configurable
        public override void SetValue(object value)
        {
            BulkText = value.ToString();
        }
    }
}
