using System;
using System.Linq;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using FlaUI.Core.Identifiers;
using FlaUI.UIA3.Identifiers;
using TestStack.White.Mappings;
using TestStack.White.UIItems.Custom;

namespace TestStack.White.UIItems.Finders
{
    public static class SearchConditionFactory
    {
        public static SimpleSearchCondition CreateForControlType(ControlType controlType)
        {
            return new SimpleSearchCondition(automationElement => automationElement.ControlType, new ControlTypeProperty(controlType, "ControlType"));
        }

        public static SearchCondition CreateForControlType(Type testControlType, WindowsFramework framework)
        {
            if (testControlType.IsCustomType())
                return CreateForControlType(CustomControlTypeMapping.ControlType(testControlType, framework));
            var controlTypes = ControlDictionary.Instance.GetControlType(testControlType, framework.FrameworkId());
            if (controlTypes.Length == 1)
                return CreateForControlType(controlTypes[0]);

            return new OrSearchCondition(controlTypes.Select(CreateForControlType).ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frameworkId">List available from WindowsFramework class or Constants class</param>
        /// <returns></returns>
        public static SearchCondition CreateForFrameworkId(string frameworkId)
        {
            return new SimpleSearchCondition(automationElement => automationElement.Properties.FrameworkId,
                                             new AutomationElementProperty(frameworkId, "FrameworkId", AutomationObjectIds.FrameworkIdProperty));
        }

        public static SearchCondition CreateForAutomationId(string id)
        {
            return new SimpleSearchCondition(automationElement => automationElement.AutomationId,
                                             new AutomationElementProperty(id, "AutomationId", AutomationObjectIds.AutomationIdProperty));
        }

        public static SearchCondition CreateForName(string name)
        {
            return new SimpleSearchCondition(automationElement => automationElement.Name, new AutomationElementProperty(name, "Name", AutomationObjectIds.NameProperty));
        }

        public static SearchCondition CreateForClassName(string className)
        {
            return new SimpleSearchCondition(automationElement => automationElement.ClassName,
                                             new AutomationElementProperty(className, "ClassName", AutomationObjectIds.ClassNameProperty));
        }

        public static SearchCondition CreateForNativeProperty(PropertyId automationProperty, string value)
        {
            var automationElementProperty = new AutomationElementProperty(value, automationProperty.Name, automationProperty);
            return new SimpleSearchCondition(automationElement => automationElement.BasicAutomationElement.GetPropertyValue(automationProperty), automationElementProperty);
        }

        public static SearchCondition CreateForNativeProperty(PropertyId automationProperty, bool value)
        {
            var automationElementProperty = new AutomationElementProperty(value, automationProperty.Name, automationProperty);
            return new SimpleSearchCondition(automationElement => automationElement.BasicAutomationElement.GetPropertyValue(automationProperty), automationElementProperty);
        }

        public static SearchCondition CreateForNativeProperty(PropertyId automationProperty, object value)
        {
            var automationElementProperty = new AutomationElementProperty(value, automationProperty.Name, automationProperty);
            return new SimpleSearchCondition(automationElement => automationElement.BasicAutomationElement.GetPropertyValue(automationProperty), automationElementProperty);
        }
    }
}