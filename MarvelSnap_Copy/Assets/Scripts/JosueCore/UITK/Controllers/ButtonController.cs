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

        private bool toggled = false;

        protected override void Initialize()
        {
            base.Initialize();
            SetupEvents();
        }

        private void SetupEvents()
        {
            RegisterClickedEvent(onClickedButtonEvents.Invoke);

            if (isToggleButton)
            {
                RegisterClickedEvent(OnButtonToggled);
            }
        }

        private void OnButtonToggled()
        {
            toggled = !toggled;
            Debugger.Log($"button toggled. Toggled state = {toggled}");
            UnityEvent actionsToFire = toggled ? onSelecetedButtonEvents : onUnSelectedButtonEvents;
            actionsToFire.Invoke();
        }

        private void RegisterClickedEvent(Action action)
        {
            Element.clicked -= action;
            Element.clicked += action;

            Debugger.Log($"click event registered: {action}");
        }
    }
}