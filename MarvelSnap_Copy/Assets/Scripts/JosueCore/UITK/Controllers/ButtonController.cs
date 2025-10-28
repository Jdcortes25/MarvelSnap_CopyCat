using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace JosueCore.UITK.Controllers
{
    public class ButtonController : BaseUIController<Button>
    {
        [Header("Settings:")]
        [SerializeField] private bool isToggleButton = false;

        [Header("Button Events:")]
        [SerializeField] private UnityEvent onClickedButtonEvents = new();
        [SerializeField, ConditionalFieldVisibility("isToggleButton", "true")] private UnityEvent onSelecetedButtonEvents = new();
        [SerializeField, ConditionalFieldVisibility("isToggleButton", "true")] private UnityEvent onUnSelectedButtonEvents = new();

        public bool IsToggleButton 
        { 
            get { return isToggleButton; } 
            set { isToggleButton = value; }
        }

        private Action buttonClickedEvents;
        private Action buttonSelectedEvents;
        private Action buttonUnselectedEvents;
        private bool toggled = false;

        protected override void Initialize()
        {
            base.Initialize();
            SetupEvents();
        }

        private void SetupEvents()
        {
            RegisterClicked();
            RegisterButtonClickedEvent(onClickedButtonEvents.Invoke);
            RegisterButtonClickedEvent(OnButtonToggled);
            RegisterToggleEvents();
        }

        private void OnButtonToggled()
        {
            if(!isToggleButton)
            {
                return;
            }

            toggled = !toggled;
            Debugger?.Log($"button toggled. Toggled state = {toggled}");
            Action actionsToFire = toggled ? buttonSelectedEvents : buttonUnselectedEvents;
            actionsToFire.Invoke();
        }

        private void RegisterClicked()
        {
            Element.clicked -= InvokeButtonClickedEvents;
            Element.clicked += InvokeButtonClickedEvents;
        }

        private void RegisterToggleEvents()
        {
            RegisterAction(ref buttonSelectedEvents, onSelecetedButtonEvents.Invoke);
            RegisterAction(ref buttonUnselectedEvents, onUnSelectedButtonEvents.Invoke);
        }

        public void RegisterButtonClickedEvent(Action action)
        {
            RegisterAction(ref buttonClickedEvents, action);
            Debugger?.Log($"clicked event registered: {action}");
        }

        public void UnRegisterButtonClickedEvent(Action action)
        {
            buttonClickedEvents -= action;
            Debugger?.Log($"clicked event unregistered: {action}");
        }

        public void RegisterButtonSelectedEvent(Action action)
        {
            RegisterAction(ref buttonSelectedEvents, action);
            Debugger?.Log($"button selected event registered: {action}");
        }

        public void UnRegisterButtonSelectedEvent(Action action)
        {
            buttonSelectedEvents -= action;
            Debugger?.Log($"button selected event unregistered: {action}");
        }

        public void RegisterButtonUnSelectedEvent(Action action)
        {
            RegisterAction(ref buttonUnselectedEvents, action);
            Debugger?.Log($"button unselected event registered: {action}");
        }

        public void UnRegisterButtonUnSelectedEvent(Action action)
        {
            buttonUnselectedEvents -= action;
            Debugger?.Log($"button unselected event unregistered: {action}");
        }

        private void RegisterAction(ref Action actionHandler, Action action)
        {
            actionHandler -= action;
            actionHandler += action;
        }

        public void InvokeButtonClickedEvents() 
        {
            buttonClickedEvents?.Invoke();
            Debugger?.Log("Button Clicked!");
        }
    }
}