using System;
using System.Collections.Generic;
using System.Text;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3.Identifiers;

namespace TestStack.White.AutomationElementSearch
{
    public class AutomationSearchCondition
    {
        static readonly Dictionary<int, Func<AutomationElement, object, bool>> ValueMatchers =
            new Dictionary<int, Func<AutomationElement, object, bool>>();
        readonly List<ConditionBase> conditions = new List<ConditionBase>();

        static AutomationSearchCondition()
        {
            ValueMatchers[AutomationObjectIds.NameProperty.Id] = (information, value) => information.Name.Equals(value);
            ValueMatchers[AutomationObjectIds.AutomationIdProperty.Id] = (information, value) => information.AutomationId.Equals(value);
            ValueMatchers[AutomationObjectIds.ClassNameProperty.Id] = (information, value) => information.ClassName.Equals(value);
            ValueMatchers[AutomationObjectIds.ProcessIdProperty.Id] =
                (element, value) => element.Properties.ProcessId.ToString().Equals(value.ToString());
            ValueMatchers[AutomationObjectIds.ControlTypeProperty.Id] = (information, value) => information.ControlType.Equals(value);
        }

        public AutomationSearchCondition(ConditionBase condition)
        {
            conditions.Add(condition);
        }

        public AutomationSearchCondition()
        {
        }

        public static AutomationSearchCondition ByName(string name)
        {
            var automationSearchCondition = new AutomationSearchCondition();
            automationSearchCondition.OfName(name);
            return automationSearchCondition;
        }

        public virtual AutomationSearchCondition OfName(string name)
        {
            conditions.Add(new PropertyCondition(AutomationObjectIds.NameProperty, name));
            return this;
        }

        public static AutomationSearchCondition ByAutomationId(string id)
        {
            var automationSearchCondition = new AutomationSearchCondition();
            automationSearchCondition.WithAutomationId(id);
            return automationSearchCondition;
        }

        public virtual AutomationSearchCondition WithAutomationId(string id)
        {
            conditions.Add(new PropertyCondition(AutomationObjectIds.AutomationIdProperty, id));
            return this;
        }

        public static AutomationSearchCondition ByControlType(ControlType controlType)
        {
            var automationSearchCondition = new AutomationSearchCondition();
            automationSearchCondition.OfControlType(controlType);
            return automationSearchCondition;
        }

        public static AutomationSearchCondition All
        {
            get
            {
                var automationSearchCondition = new AutomationSearchCondition();
                automationSearchCondition.conditions.Add(TrueCondition.Default);
                return automationSearchCondition;
            }
        }

        public virtual AutomationSearchCondition OfControlType(ControlType controlType)
        {
            conditions.Add(new PropertyCondition(AutomationObjectIds.ControlTypeProperty, controlType));
            return this;
        }

        public virtual AutomationSearchCondition WithProcessId(int processId)
        {
            conditions.Add(new PropertyCondition(AutomationObjectIds.ProcessIdProperty, processId));
            return this;
        }

        public virtual ConditionBase Condition
        {
            get
            {
                if (conditions.Count == 1) return conditions[0];
                if (conditions.Count == 0) return TrueCondition.Default;
                return new AndCondition(conditions.ToArray());
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (PropertyCondition condition in conditions)
                stringBuilder.AppendFormat("{0}:{1}", condition.Property.Name, condition.Value);
            return stringBuilder.ToString();
        }

        public static AutomationSearchCondition ByClassName(string className)
        {
            var automationSearchCondition = new AutomationSearchCondition();
            automationSearchCondition.conditions.Add(new PropertyCondition(AutomationObjectIds.ClassNameProperty, className));
            return automationSearchCondition;
        }

        public static string ToString(AutomationSearchCondition[] conditions)
        {
            var builder = new StringBuilder();
            foreach (AutomationSearchCondition condition in conditions)
                builder.AppendLine(condition.ToString());
            return builder.ToString();
        }

        public virtual bool Satisfies(AutomationElement element)
        {
            return Satisfies(element, conditions.ToArray(), true);
        }

        private bool Satisfies(AutomationElement element, IEnumerable<ConditionBase> testConditions, bool and)
        {
            foreach (ConditionBase condition in testConditions)
            {
                if (condition is AndCondition && !Satisfies(element, ((AndCondition) condition).Conditions, true)) return false;
                if (condition is OrCondition && !Satisfies(element, ((OrCondition) condition).Conditions, false)) return false;

                if (condition is PropertyCondition)
                {
                    var match = ValueMatchers[((PropertyCondition) condition).Property.Id](element, ((PropertyCondition) condition).Value);
                    if (!match && and) return false;
                    if (match && !and) return true;
                }
            }
            return true;
        }

        public virtual void Add(ConditionBase condition)
        {
            conditions.Add(condition);
        }

        public static AutomationSearchCondition GetWindowWithTitleBarSearchCondition(int processId)
        {
            return GetWindowSearchCondition(processId, ControlType.Window);
        }

        public static AutomationSearchCondition GetWindowSearchCondition(int processId, ControlType controlType)
        {
            AutomationSearchCondition windowSearchCondition = ByControlType(controlType);
            if (processId > 0) windowSearchCondition.WithProcessId(processId);
            return windowSearchCondition;
        }
    }
}