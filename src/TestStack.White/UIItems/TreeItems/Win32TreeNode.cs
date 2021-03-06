using System.Windows;
using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.UIA;
using TestStack.White.UIItems.Actions;
using TestStack.White.WindowsAPI;

namespace TestStack.White.UIItems.TreeItems
{
    [PlatformSpecificItem]
    public class Win32TreeNode : TreeNode
    {
        protected Win32TreeNode() {}
        public Win32TreeNode(AutomationElement automationElement, IActionListener actionListener) : base(automationElement, actionListener) {}

        //TODO Expand and Collapse also Select the node because it uses DoubleClick. Even the pattern does the same. Clicking to the left of node,
        // would not work when there is an icon to left of the node.
        protected override void DoExpand()
        {
            Click();
            KeyIn(KeyboardInput.SpecialKeys.RIGHT);
            if (Nodes.Count == 0)
                throw new AutomationException(string.Format("Cannot expand TreeNode {0}, expand button not visible", this), Debug.Details(AutomationElement));
        }

        protected override void DoCollapse()
        {
            Click();
            KeyIn(KeyboardInput.SpecialKeys.LEFT);
        }

        protected override Point SelectPoint
        {
            get { return automationElement.BoundingRectangle.Center; }
        }
    }
}