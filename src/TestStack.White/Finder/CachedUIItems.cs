using System;
using System.Collections.Generic;
using FlaUI.Core.AutomationElements.Infrastructure;
using TestStack.White.AutomationElementSearch;
using TestStack.White.Factory;
using TestStack.White.Mappings;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.Finders;

namespace TestStack.White.Finder
{
    public class CachedUIItems
    {
        private readonly List<AutomationElement> list = new List<AutomationElement>();
        private readonly DictionaryMappedItemFactory dictionaryMappedItemFactory = new DictionaryMappedItemFactory();
        private UIItemCollection uiItemCollection;
        private CachedUIItems() {}

        public static CachedUIItems CreateAndCachePrimaryChildControls(AutomationElement parent, InitializeOption option)
        {
            var cachedUIItems = new CachedUIItems();
            cachedUIItems.FindAll(parent);
            cachedUIItems.list.Sort(new AutomationElementPositionComparer());
            return cachedUIItems;
        }

        private void FindAll(AutomationElement automationElement)
        {
            var finder = new AutomationElementFinder(automationElement);
            List<AutomationElement> children = finder.Children(AutomationSearchCondition.All);
            ControlDictionary controlDictionary = ControlDictionary.Instance;
            foreach (AutomationElement child in children)
            {
                if (!controlDictionary.IsControlTypeSupported(child.ControlType)) continue;
                if (controlDictionary.IsPrimaryControl(child.ControlType, child.ClassName, null)) list.Add(child);
                if (!controlDictionary.HasPrimaryChildren(child.ControlType)) continue;
                if (!controlDictionary.IsExcluded(child.ControlType)) FindAll(child);
            }
        }

        public virtual UIItemCollection GetAll(Predicate<AutomationElement> predicate, IUIItemFactory factory, IActionListener actionListener)
        {
            List<AutomationElement> foundElements = list.FindAll(predicate);
            return new UIItemCollection(foundElements, factory, actionListener);
        }

        public virtual UIItemCollection UIItems(IActionListener actionListener)
        {
            if (uiItemCollection != null) return uiItemCollection;
            uiItemCollection = new UIItemCollection();
            foreach (AutomationElement automationElement in list)
                uiItemCollection.Add(dictionaryMappedItemFactory.Create(automationElement, actionListener));
            return uiItemCollection;
        }

        public virtual int Count
        {
            get { return list.Count; }
        }

        public virtual IUIItem Get(SearchCriteria searchCriteria, IActionListener actionListener)
        {
            return Get(searchCriteria, actionListener, dictionaryMappedItemFactory);
        }

        public virtual UIItemCollection GetAll(SearchCriteria searchCriteria, IActionListener actionListener)
        {
            return GetAll(searchCriteria, actionListener, dictionaryMappedItemFactory);
        }

        public virtual IUIItem Get(SearchCriteria searchCriteria, IActionListener actionListener, IUIItemFactory factory)
        {
            List<AutomationElement> automationElements = searchCriteria.Filter(list);
            if (automationElements.Count == 0) return null;
            return factory.Create(automationElements[0], actionListener);
        }

        public virtual T Get<T>(SearchCriteria searchCriteria, IActionListener actionListener) where T : UIItem
        {
            return Get<T>(searchCriteria, actionListener, dictionaryMappedItemFactory);
        }

        public virtual T Get<T>(SearchCriteria searchCriteria, IActionListener actionListener, IUIItemFactory factory) where T : UIItem
        {
            return (T) Get(searchCriteria.AndControlType(typeof (T), WindowsFramework.None), actionListener, factory);
        }

        private UIItemCollection GetAll(SearchCriteria searchCriteria, IActionListener actionListener, IUIItemFactory factory)
        {
            List<AutomationElement> automationElements = searchCriteria.Filter(list);
            return new UIItemCollection(automationElements.ToArray(), factory, actionListener);
        }
    }
}