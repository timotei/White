using System.Collections.Generic;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using FlaUI.Core.EventHandlers;
using FlaUI.UIA3.Patterns;
using TestStack.White.AutomationElementSearch;
using TestStack.White.Factory;
using TestStack.White.Recording;
using TestStack.White.UIItemEvents;
using TestStack.White.UIItems.Actions;

namespace TestStack.White.UIItems.TabItems
{
    public class Tab : UIItem
    {
        private TabPages pages;
        private IAutomationPropertyChangedEventHandler handler;
        protected Tab() {}

        public Tab(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}

        public virtual ITabPage SelectedTab
        {
            get { return Pages.Find(tabPage => tabPage.IsSelected); }
        }

        public virtual int TabCount
        {
            get { return Pages.Count; }
        }

        public virtual TabPages Pages
        {
            get
            {
                if (pages == null)
                {
                    pages = new TabPages();
                    var finder = new AutomationElementFinder(automationElement);
                    List<AutomationElement> collection = finder.Children(AutomationSearchCondition.ByControlType(ControlType.TabItem));
                    foreach (AutomationElement tabItemElement in collection)
                        pages.Add(new TabPage(tabItemElement, actionListener));
                }

                return pages;
            }
        }

        public virtual void SelectTabPage(int index)
        {
            ITabPage tabPage = Pages[index];
            tabPage.Select();
        }

        public virtual void SelectTabPage(string tabTitle)
        {
            var oldTab = SelectedTab;
            var tabPage = Pages.Find(tabItem => tabItem.NameMatches(tabTitle));
            if (tabPage == null) throw new UIItemSearchException(string.Format("No tab found with title{0}", tabTitle));

            tabPage.Select();
            if (!oldTab.Equals(SelectedTab)) actionListener.ActionPerformed(new Action(ActionType.NewControls));
        }

        public override void HookEvents(IUIItemEventListener eventListener)
        {
            handler = automationElement.RegisterPropertyChangedEvent(
                TreeScope.Descendants, delegate { eventListener.EventOccured(new TabEvent(this)); },
                SelectionItemPattern.IsSelectedProperty);
        }

        public override void UnHookEvents()
        {
            automationElement.RemovePropertyChangedEventHandler(handler);
        }
    }
}