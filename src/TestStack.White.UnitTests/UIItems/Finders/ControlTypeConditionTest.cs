using NUnit.Framework;
using System;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowStripControls;

namespace TestStack.White.UnitTests.UIItems.Finders
{
    [TestFixture]
    public class ControlTypeConditionTest
    {
        [Test]
        public void ControlTypeCondition()
        {
            Assert.That(SearchConditionFactory.CreateForControlType(typeof(Button), WindowsFramework.Wpf).ToString(),
                Is.EqualTo(String.Format("(ControlType={0} or ControlType={1})", ControlType.Button, ControlType.CheckBox)));
            Assert.That(SearchConditionFactory.CreateForControlType(typeof(TestCustomUIItem), WindowsFramework.Wpf).ToString(),
                Is.EqualTo(String.Format("ControlType={0}", ControlType.Pane)));
            Assert.That(SearchConditionFactory.CreateForControlType(typeof(MenuBar), WindowsFramework.WinForms).ToString(),
                Is.EqualTo(String.Format("ControlType={0}", ControlType.MenuBar)));
            Assert.That(SearchConditionFactory.CreateForControlType(typeof(MenuBar), WindowsFramework.Wpf).ToString(),
                Is.EqualTo(String.Format("ControlType={0}", ControlType.Menu)));
        }
    }
}