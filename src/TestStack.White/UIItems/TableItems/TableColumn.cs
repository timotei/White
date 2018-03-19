using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems.TableItems
{
    public class TableColumn : UIItem
    {
        private readonly int index;
        protected TableColumn() {}

        public TableColumn(AutomationElement automationElement, IActionListener actionListener, int index) : base(automationElement, actionListener)
        {
            this.index = index;
        }

        public virtual int Index
        {
            get { return index; }
        }
    }
}