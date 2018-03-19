using System.Collections.Generic;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using TestStack.White.AutomationElementSearch;
using TestStack.White.Configuration;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems.TableItems
{
    public class TableHeader : UIItem
    {
        protected TableHeader() {}

        public TableHeader(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}

        public virtual TableColumns Columns
        {
            get
            {
                var descendants = new AutomationElementFinder(automationElement)
                    .Descendants(AutomationSearchCondition.ByControlType(ControlType.Header));
                var columnElements = new List<AutomationElement>(descendants)
                    .FindAll(obj => !obj.Name.StartsWith(UIItemIdAppXmlConfiguration.Instance.TableColumn));
                return new TableColumns(columnElements, actionListener);
            }
        }
    }
}