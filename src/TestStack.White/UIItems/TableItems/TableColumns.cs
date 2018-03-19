using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.UIA3.Identifiers;
using TestStack.White.Configuration;
using TestStack.White.Factory;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems.TableItems
{
    public class TableColumns : UIItemList<TableColumn>
    {
        public TableColumns(IEnumerable<AutomationElement> automationElements, IActionListener actionListener)
        {
            int i = 0;
            foreach (AutomationElement automationElement in automationElements)
            {
                if (HeaderColumn(automationElement)) continue;
                Add(new TableColumn(automationElement, actionListener, i++));
            }
        }

        private static bool HeaderColumn(AutomationElement automationElement)
        {
            return automationElement.BasicAutomationElement.GetPropertyValue(AutomationObjectIds.NameProperty).Equals(UIItemIdAppXmlConfiguration.Instance.TableTopLeftHeaderCell);
        }

        public virtual TableColumn this[string text]
        {
            get
            {
                TableColumn column = Find(obj => obj.Name.Equals(text));
                if (column == null)
                {
                    throw new UIItemSearchException(
                        string.Format("Cannot find column with text {0}. Found columns: {1}", text,
                                      string.Join(",", this.Select(i=>i.ToString()).ToArray())));
                }
                return column;
            }
        }
    }
}