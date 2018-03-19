using System.Linq;
using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.Finders;

namespace TestStack.White.UIItems.WindowStripControls
{
    public class WPFStatusBar : UIItem
    {
        protected WPFStatusBar() {}
        public WPFStatusBar(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}

        public virtual UIItemCollection Items
        {
            get
            {
                var uiItemCollection = factory.CreateAll(SearchCriteria.All, actionListener)
                    .Where(i=>i.AutomationElement.ClassName == "StatusBarItem");
                return new UIItemCollection(uiItemCollection);
            }
        }
    }
}