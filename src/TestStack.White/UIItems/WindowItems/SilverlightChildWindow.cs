﻿using System;
using System.Threading;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Exceptions;
using TestStack.White.Factory;
using TestStack.White.Sessions;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using NotSupportedException = System.NotSupportedException;

namespace TestStack.White.UIItems.WindowItems
{
    [PlatformSpecificItem]
    public class SilverlightChildWindow : Window
    {
        protected SilverlightChildWindow() {}

        public SilverlightChildWindow(AutomationElement automationElement, IActionListener actionListener)
            : base(automationElement, InitializeOption.NoCache, new NullWindowSession())
        {
            // We need to sleep to make sure animation is finished
            Thread.Sleep(600);
        }

        public override Window ModalWindow(string title, InitializeOption option)
        {
            throw new NotSupportedException();
        }

        public override Window ModalWindow(SearchCriteria searchCriteria, InitializeOption option)
        {
            throw new NotSupportedException();
        }

        public override PopUpMenu Popup
        {
            get { return null; }
        }

        public override void WaitWhileBusy()
        {
            try
            {
                WaitForProcess();
                HourGlassWait();
                CustomWait();
            }
            catch (Exception e)
            {
                if (!(e is InvalidOperationException || e is ElementNotAvailableException))
                    throw new UIActionException(string.Format("Window didn't respond" + Constants.BusyMessage), e);
            }
        }
    }
}