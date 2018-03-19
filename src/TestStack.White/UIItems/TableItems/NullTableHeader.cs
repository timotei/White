using System.Collections.Generic;
using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems.TableItems
{
    public class NullTableHeader : TableHeader
    {
        public override TableColumns Columns
        {
            get { return new TableColumns(new List<AutomationElement>(), new NullActionListener()); }
        }
    }
}