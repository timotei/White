using NUnit.Framework;
using System.Windows;
using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.UIItems.Finders;

namespace TestStack.White.UnitTests.UIItems.Finders
{
    [TestFixture]
    public class SearchConditionFactoryTest
    {
        private readonly AutomationElement element;

        public SearchConditionFactoryTest()
        {
            element = Desktop.Automation.FromPoint(new Point(100, 100));
        }

        [Test]
        public void Create()
        {
            Assert.That(SearchConditionFactory.CreateForControlType(element.ControlType).AppliesTo(element), Is.True);
            Assert.That(SearchConditionFactory.CreateForAutomationId(element.AutomationId).AppliesTo(element), Is.True);
            Assert.That(SearchConditionFactory.CreateForFrameworkId(element.Properties.FrameworkId).AppliesTo(element), Is.True);
            Assert.That(SearchConditionFactory.CreateForClassName(element.ClassName).AppliesTo(element), Is.True);
        }
    }
}