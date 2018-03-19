using System;
using System.Text;
using FlaUI.Core.AutomationElements.Infrastructure;
using Castle.Core.Logging;
using FlaUI.Core;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Identifiers;
using FlaUI.UIA3;
using FlaUI.UIA3.Identifiers;
using FlaUI.UIA3.Patterns;
using TestStack.White.Configuration;
using TestStack.White.UIItems;

namespace TestStack.White
{
    public static class Debug
    {
        private static readonly ILogger Logger = CoreAppXmlConfiguration.Instance.LoggerFactory.Create(typeof(Debug));
        private const string Tab = "  ";

        public static void ProcessDetails(string processName)
        {
            try
            {
                AutomationElement element = Desktop.Automation.GetDesktop().FindFirst(TreeScope.Children, new PropertyCondition(AutomationObjectIds.NameProperty, processName));
                Details(element);
            }
            catch (Exception)
            {
                Logger.Warn("Error happened while creating error report");
            }
        }

        public static string Details(AutomationElement automationElement)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine();
                Details(stringBuilder, automationElement, string.Empty);
                return stringBuilder.ToString();
            }
            catch (Exception)
            {
                Logger.Warn("Error happened while creating error report");
                return string.Empty;
            }
        }

        private static void Details(StringBuilder stringBuilder, AutomationElement automationElement, string displayPadding)
        {
            WriteDetail(automationElement, stringBuilder, displayPadding);
            DisplayPattern(automationElement, stringBuilder, displayPadding);
            AutomationElement[] children = automationElement.FindAll(TreeScope.Children, TrueCondition.Default);
            foreach (AutomationElement child in children)
                Details(stringBuilder, child, displayPadding + Tab + Tab);
        }

        private static void WriteDetail(AutomationElement automationElement, StringBuilder stringBuilder, string displayPadding)
        {
            WriteDetail(stringBuilder, "AutomationId: " + automationElement.AutomationId, displayPadding);
            WriteDetail(stringBuilder, "ControlType: " + automationElement.ControlType, displayPadding);
            WriteDetail(stringBuilder, "Name: " + automationElement.Name, displayPadding);
            WriteDetail(stringBuilder, "HelpText: " + automationElement.HelpText, displayPadding);
            WriteDetail(stringBuilder, "Bounding rectangle: " + automationElement.BoundingRectangle, displayPadding);
            WriteDetail(stringBuilder, "ClassName: " + automationElement.ClassName, displayPadding);
            WriteDetail(stringBuilder, "IsOffScreen: " + automationElement.IsOffscreen, displayPadding);
            WriteDetail(stringBuilder, "FrameworkId: " + automationElement.Properties.FrameworkId, displayPadding);
            WriteDetail(stringBuilder, "ProcessId: " + automationElement.Properties.ProcessId, displayPadding);
            stringBuilder.AppendLine();
        }

        private static void WriteDetail(StringBuilder stringBuilder, string message, string padding)
        {
            stringBuilder.AppendLine(padding + message);
        }

        public static string GetAllWindows()
        {
            try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("All windows:");
                stringBuilder.AppendLine();
                GetAllWindows(stringBuilder, 0, Desktop.Automation.GetDesktop());
                return stringBuilder.ToString();
            }
            catch (Exception)
            {
                Logger.Warn("Error happened while creating error report");
            }
            return string.Empty;
        }

        private static void GetAllWindows(StringBuilder stringBuilder, int level, AutomationElement baseElement)
        {
            string padding = level == 0 ? string.Empty : Tab;
            var windowCondition = new PropertyCondition(AutomationObjectIds.ControlTypeProperty, ControlType.Window);
            AutomationElement[] allWindows = baseElement.FindAll(TreeScope.Children, windowCondition);
            foreach (AutomationElement windowElement in allWindows)
            {
                var pattern = windowElement.Patterns.Window.PatternOrDefault;
                string modalText = pattern == null ? "(null)" : pattern.IsModal.ToString();
                stringBuilder.AppendFormat("{0}Name: {1},  Bounds: {2} ProcessId: {3}, Modal: {4}", padding, windowElement.Name,
                                           windowElement.BoundingRectangle, windowElement.Properties.ProcessId, modalText);
                stringBuilder.AppendLine();

                if (level == 0) GetAllWindows(stringBuilder, 1, windowElement);
            }
        }

        private static void DisplayPattern(AutomationElement automationElement, StringBuilder stringBuilder, string displayPadding)
        {
            throw new NotImplementedException("Patterns can't be provided dynamically");
            //PatternId[] supportedPatterns = automationElement.GetSupportedPatterns();
            //foreach (PatternId automationPattern in supportedPatterns)
            //{
            //    var pattern = (BasePattern) automationElement.Patterns.GetCustomPattern().GetCurrentPattern(automationPattern);
            //    stringBuilder.Append(displayPadding).AppendLine(pattern.ToString());
            //}
            //stringBuilder.AppendLine();
        }

        public static void LogProperties(UIItem uiItem)
        {
            LogProperties(uiItem.AutomationElement);
        }

        public static void LogProperties(AutomationElement element)
        {
            throw new NotImplementedException("Porperties can't be retrieved dynamically");
            //AutomationProperty[] automationProperties = element.GetSupportedProperties();
            //foreach (AutomationProperty automationProperty in automationProperties)
            //{
            //    Logger.Info(automationProperty.Name + ":" + element.GetCurrentPropertyValue(automationProperty));
            //}
        }

        public static void LogPatterns(UIItem uiItem)
        {
            LogPatterns(uiItem.AutomationElement);
        }

        public static void LogPatterns(AutomationElement automationElement)
        {
            var builder = new StringBuilder();
            DisplayPattern(automationElement, builder, string.Empty);
            Logger.Info(builder.ToString());
        }
    }
}