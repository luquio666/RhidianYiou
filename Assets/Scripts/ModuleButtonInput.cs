using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleButtonInput : Module
{

    public Text Name;
    public GameObject Selected;
    public string PressedEvent;

    public void SetSelectedState()
    {
        Selected.SetActive(true);
        Debug.Log($"Selected button: {Name.text}");
    }

    public void SetUnselectedState()
    {
        Selected.SetActive(false);
    }

    public void ButtonPressed()
    {
        Debug.Log($"Pressed button: {Name.text}");
        if(PressedEvent != null)
            GameEvents.ButtonInputPressed(PressedEvent);
    }

    [ContextMenu("SetName")]
    public void SetName()
    {
        this.name = $"Button ({Name.text})";
    }

    [ContextMenu("SetEventAsName")]
    public void SetEvent()
    {
        PressedEvent = Name.text;
    }

}
