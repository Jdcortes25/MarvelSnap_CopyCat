using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ButtonController : VisualElementController<Button>
{
    [Header("Settings:")]
    [SerializeField] private bool isToggleButton = false;

    [Header("Button Events:")]
    [SerializeField] private List<UnityEvent> onClickedButtonEvents = new ();
    [SerializeField] private List<UnityEvent> onSelecetedButtonEvents = new ();
    [SerializeField] private List<UnityEvent> onUnSelectedButtonEvents = new ();

    private bool toggled = false;

    protected override void Initialize()
    {
        base.Initialize();
        SetupEvents();
    }

    private void SetupEvents()
    {
        foreach (UnityEvent action in onClickedButtonEvents)
        {
            RegisterClickedEvent(action.Invoke);
        }

        if(isToggleButton)
        {
            RegisterClickedEvent(OnButtonToggled);
        }
    }

    private void OnButtonToggled()
    {
        toggled = !toggled;
        UnityEvent[] actionsToFire = toggled ? onSelecetedButtonEvents.ToArray() : onUnSelectedButtonEvents.ToArray();

        foreach (UnityEvent action in actionsToFire)
        {
            action.Invoke();
        }
    }

    private void RegisterClickedEvent(Action action)
    {
        Element.clicked -= action;
        Element.clicked += action;
    }
}
