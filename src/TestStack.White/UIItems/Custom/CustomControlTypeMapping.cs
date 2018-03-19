using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.Definitions;

namespace TestStack.White.UIItems.Custom
{
    public class CustomControlTypeMapping
    {
        private static readonly Dictionary<CustomUIItemType, ControlType> Mappings = new Dictionary<CustomUIItemType, ControlType>();

        static CustomControlTypeMapping()
        {
            Mappings[CustomUIItemType.Pane] = FlaUI.Core.Definitions.ControlType.Pane;
            Mappings[CustomUIItemType.Custom] = FlaUI.Core.Definitions.ControlType.Custom;
            Mappings[CustomUIItemType.Group] = FlaUI.Core.Definitions.ControlType.Group;
            Mappings[CustomUIItemType.Window] = FlaUI.Core.Definitions.ControlType.Window;
            Mappings[CustomUIItemType.Table] = FlaUI.Core.Definitions.ControlType.Table;
            Mappings[CustomUIItemType.Button] = FlaUI.Core.Definitions.ControlType.Button;
            Mappings[CustomUIItemType.Calendar] = FlaUI.Core.Definitions.ControlType.Calendar;
            Mappings[CustomUIItemType.CheckBox] = FlaUI.Core.Definitions.ControlType.CheckBox;
            Mappings[CustomUIItemType.ComboBox] = FlaUI.Core.Definitions.ControlType.ComboBox;
            Mappings[CustomUIItemType.DataGrid] = FlaUI.Core.Definitions.ControlType.DataGrid;
            Mappings[CustomUIItemType.DataItem] = FlaUI.Core.Definitions.ControlType.DataItem;
            Mappings[CustomUIItemType.Document] = FlaUI.Core.Definitions.ControlType.Document;
            Mappings[CustomUIItemType.Edit] = FlaUI.Core.Definitions.ControlType.Edit;
            Mappings[CustomUIItemType.Header] = FlaUI.Core.Definitions.ControlType.Header;
            Mappings[CustomUIItemType.HeaderItem] = FlaUI.Core.Definitions.ControlType.HeaderItem;
            Mappings[CustomUIItemType.Hyperlink] = FlaUI.Core.Definitions.ControlType.Hyperlink;
            Mappings[CustomUIItemType.Image] = FlaUI.Core.Definitions.ControlType.Image;
            Mappings[CustomUIItemType.List] = FlaUI.Core.Definitions.ControlType.List;
            Mappings[CustomUIItemType.ListItem] = FlaUI.Core.Definitions.ControlType.ListItem;
            Mappings[CustomUIItemType.Menu] = FlaUI.Core.Definitions.ControlType.Menu;
            Mappings[CustomUIItemType.MenuBar] = FlaUI.Core.Definitions.ControlType.MenuBar;
            Mappings[CustomUIItemType.MenuItem] = FlaUI.Core.Definitions.ControlType.MenuItem;
            Mappings[CustomUIItemType.ProgressBar] = FlaUI.Core.Definitions.ControlType.ProgressBar;
            Mappings[CustomUIItemType.RadioButton] = FlaUI.Core.Definitions.ControlType.RadioButton;
            Mappings[CustomUIItemType.ScrollBar] = FlaUI.Core.Definitions.ControlType.ScrollBar;
            Mappings[CustomUIItemType.Separator] = FlaUI.Core.Definitions.ControlType.Separator;
            Mappings[CustomUIItemType.Slider] = FlaUI.Core.Definitions.ControlType.Slider;
            Mappings[CustomUIItemType.Spinner] = FlaUI.Core.Definitions.ControlType.Spinner;
            Mappings[CustomUIItemType.SplitButton] = FlaUI.Core.Definitions.ControlType.SplitButton;
            Mappings[CustomUIItemType.StatusBar] = FlaUI.Core.Definitions.ControlType.StatusBar;
            Mappings[CustomUIItemType.Tab] = FlaUI.Core.Definitions.ControlType.Tab;
            Mappings[CustomUIItemType.TabItem] = FlaUI.Core.Definitions.ControlType.TabItem;
            Mappings[CustomUIItemType.Text] = FlaUI.Core.Definitions.ControlType.Text;
            Mappings[CustomUIItemType.Thumb] = FlaUI.Core.Definitions.ControlType.Thumb;
            Mappings[CustomUIItemType.TitleBar] = FlaUI.Core.Definitions.ControlType.TitleBar;
            Mappings[CustomUIItemType.ToolBar] = FlaUI.Core.Definitions.ControlType.ToolBar;
            Mappings[CustomUIItemType.ToolTip] = FlaUI.Core.Definitions.ControlType.ToolTip;
            Mappings[CustomUIItemType.Tree] = FlaUI.Core.Definitions.ControlType.Tree;
            Mappings[CustomUIItemType.TreeItem] = FlaUI.Core.Definitions.ControlType.TreeItem;
        }

        public static ControlType ControlType(CustomUIItemType customUIItemType)
        {
            return Mappings[customUIItemType];
        }

        public static ControlType ControlType(Type type, WindowsFramework framework)
        {
            var controlTypeMappingAttribute = type.GetCustomAttributes(typeof(ControlTypeMappingAttribute), true)
                .OfType<ControlTypeMappingAttribute>()
                .ToArray();
            if (!controlTypeMappingAttribute.Any())
                throw new CustomUIItemException("ControlTypeMappingAttribute needs to be defined for this type: " + type.FullName);

            var frameworkSpecific = controlTypeMappingAttribute.FirstOrDefault(c => c.AppliesToFramework == framework);
            if (frameworkSpecific != null)
                return ControlType(frameworkSpecific.CustomUIItemType);

            return ControlType(controlTypeMappingAttribute.Single(a=>a.AppliesToFramework == null).CustomUIItemType);
        }
    }
}