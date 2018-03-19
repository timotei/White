using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;

namespace TestStack.White.UIItems.WindowStripControls
{
    public class StatusStrip : ContainerStrip, IMenuContainer
    {
        private readonly Menus topLevelMenu;
        protected StatusStrip() {}

        public StatusStrip(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener)
        {
            topLevelMenu = new Menus(automationElement, actionListener);
        }

        public virtual Menu MenuItem(params string[] path)
        {
            return topLevelMenu.Find(path);
        }

        public virtual Menu MenuItemBy(params SearchCriteria[] path)
        {
            return topLevelMenu.Find(path);
        }
    }
}