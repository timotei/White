using System;
using System.Collections.Generic;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using TestStack.White.AutomationElementSearch;
using TestStack.White.Configuration;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.TableItems;

namespace TestStack.White.Factory
{
    public class TableRowFactory
    {
        private readonly AutomationElementFinder automationElementFinder;
        private static readonly Predicate<AutomationElement> RowPredicate;
        private static int result;

        static TableRowFactory()
        {
            RowPredicate =
                element =>
                element.Name.Split(' ').Length == 2 &&
                // row header containes no Numbers
                (int.TryParse(element.Name.Split(' ')[0], out result) ||
                int.TryParse(element.Name.Split(' ')[1], out result));
        }

        public TableRowFactory(AutomationElementFinder automationElementFinder)
        {
            this.automationElementFinder = automationElementFinder;
        }

        public virtual TableRows CreateRows(IActionListener actionListener, TableHeader tableHeader)
        {
            List<AutomationElement> rowElements = GetRowElements();
            return new TableRows(rowElements, actionListener, tableHeader, new TableCellFactory(automationElementFinder.AutomationElement, actionListener));
        }

        private List<AutomationElement> GetRowElements()
        {
            // this will find only first level children of out element - rows
            List<AutomationElement> descendants = automationElementFinder.Children(AutomationSearchCondition.ByControlType(ControlType.Custom));
            var automationElements = new List<AutomationElement>(descendants.FindAll(RowPredicate));
            return automationElements;
        }

        public virtual int NumberOfRows
        {
            get { return GetRowElements().Count; }
        }
    }
}