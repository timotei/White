using System;
using System.Threading;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.UIA3.Patterns;
using TestStack.White.UIA;
using TestStack.White.UIItems.Actions;
using TestStack.White.Utility;
using Action = TestStack.White.UIItems.Actions.Action;

namespace TestStack.White.UIItems.ListBoxItems
{
    public abstract class ListItem : UIItem
    {
        protected ListItem() {}
        protected ListItem(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}

        public virtual bool IsSelected
        {
            get { return (bool) Property(SelectionItemPattern.IsSelectedProperty); }
        }

        public virtual string Text
        {
            get { return Name; }
        }

        public abstract bool Checked { get; }

        public virtual void Select()
        {
            Select(!IsSelected);
        }

        public virtual void Select(bool shouldSelect)
        {
            if (!shouldSelect) return;

            actionListener.ActionPerforming(this);

            if (Bounds.IsEmpty)
            {
                Logger.Debug("Bounds empty, falling back to automation patterns");
                var selectionItemPattern =
                    (SelectionItemPattern) automationElement.Patterns.SelectionItem.Pattern;
                selectionItemPattern.Select();
            }
            else
            {
                Logger.Debug("Selecting item with Click");
                WaitForBoundsToStabilise(this);
                mouse.Click(Bounds.ImmediateInteriorEast(), actionListener);
                if (!IsSelected)
                {
                    Logger.Debug("Failed to select list item via click. Falling back to automation patterns");
                    automationElement.Patterns.SelectionItem.PatternOrDefault.Select();
                }
            }

            actionListener.ActionPerformed(Action.WindowMessage);
        }

        /// <summary>
        /// When the dropdown is animating, this can stop White from clicking too soon and not selecting properly
        /// </summary>
        /// <param name="item"></param>
        private static void WaitForBoundsToStabilise(IUIItem item)
        {
            Retry.For(() =>
            {
                var oldBounds = item.Bounds;
                Thread.Sleep(10);
                return oldBounds.Equals(item.Bounds);
            }, TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(10));
        }

        public abstract void Check();
        public abstract void UnCheck();
    }
}