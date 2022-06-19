using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EType
{
    MSG, // Just a message or descriptions
    PICKABLE, // Something you can pick
    DOOR // Door that could give you access to places
}

public class ModuleInteractable : Module
{
    public string Name = "Unknown";
    public EType InteractableType;

    [Space]

    public string[] Messages;
    private string _defaultMsg;
    private ModuleDialogData _dialogData;

    private void Awake()
    {
        _defaultMsg = $"{Name} it is...";
    }

    public void Interact()
    {
        ShowDialogBox();
    }

    private void ShowDialogBox()
    {
        if (_dialogData == null)
        {
            _dialogData = gameObject.AddComponent<ModuleDialogData>();
            _dialogData.DialogText = GetRandomMessage();
        }

        GameEvents.ShowDialogData(_dialogData);

        Debug.Log(_dialogData.DialogText);
    }

    private string GetRandomMessage()
    {
        string value = _defaultMsg;
        if (Messages.Length >= 1)
        {
            int randomIndex = UnityEngine.Random.Range(0, Messages.Length);
            value = Messages[randomIndex];
        }
        return value;
    }

}
