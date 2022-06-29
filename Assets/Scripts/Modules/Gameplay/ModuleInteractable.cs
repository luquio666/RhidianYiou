using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EType
{
    MSG, // Just a message or descriptions
    PICKABLE, // Something you can pick
    DOOR, // Door that could give you access to places
    MSG_ID
}

public class ModuleInteractable : Module
{
    public string InteractableName = "Unknown";
    public EType InteractableType;

    [Space]
    public string MessageID;
    public string[] Messages;
    public string[] Pickables;
    public string DoorId;

    private ModuleDialogData _dialogData;
    private ModuleDialogOptionData _option1;
    private ModuleDialogOptionData _option2;

    public void Interact()
    {
        ShowDialogBox();
    }

    private void ShowDialogBox()
    {
        if (_dialogData == null)
            _dialogData = gameObject.GetComponent<ModuleDialogData>();
        if (_dialogData == null)
            _dialogData = gameObject.AddComponent<ModuleDialogData>();

        switch (InteractableType)
        {
            case EType.MSG:
                _dialogData.DialogText = GetRandomMessage();
                break;
            case EType.PICKABLE:
                {
                    string pickable = GetRandomPickable();

                    _dialogData.DialogText = $"Seems I've found {pickable}";

                    // Create option
                    if(_option1 == null)
                        _option1 = gameObject.AddComponent<ModuleDialogOptionData>();
                    _option1.OptionText = $"Pick {pickable}";
                    _option1.OptionType = DialogOptions.GIVE_ITEM;
                    _option1.TargetID = pickable;

                    if (_option2 == null)
                        _option2 = gameObject.AddComponent<ModuleDialogOptionData>();
                    _option2.OptionText = $"Leave it";
                    _option2.OptionType = DialogOptions.CLOSE_DIALOG;

                    // Add option to dialogData
                    _dialogData.Options = new ModuleDialogOptionData[] { _option1, _option2 };
                }
                break;
            case EType.DOOR:
                {
                    _dialogData.DialogText = $"{InteractableName} seems to be open. May I...";

                    // Create options
                    if (_option1 == null)
                        _option1 = gameObject.AddComponent<ModuleDialogOptionData>();
                    _option1.OptionText = $"Enter";
                    _option1.OptionType = DialogOptions.ENTER_BUILDING;

                    if (_option2 == null)
                        _option2 = gameObject.AddComponent<ModuleDialogOptionData>();
                    _option2.OptionText = $"Leave";
                    _option2.OptionType = DialogOptions.CLOSE_DIALOG;

                    // Add option to dialogData
                    _dialogData.Options = new ModuleDialogOptionData[] { _option1, _option2 };
                }
                break;
            default:
                break;
        }

        if (InteractableType == EType.MSG_ID)
        {
            this.GetComponent<ModuleAIInput>().Behaviour = AIBehaviour.NONE;
            GameEvents.StopCurrentMusic();
            GameEvents.PlayAudio(AudioNames.MUSIC_02);
            GameEvents.ShowDialog(MessageID);
        }
        else
            GameEvents.ShowDialogData(_dialogData);
    }

    private string GetRandomMessage()
    {
        string value = $"nothing";
        if (Messages != null && Messages.Length >= 1)
        {
            int randomIndex = UnityEngine.Random.Range(0, Messages.Length);
            value = Messages[randomIndex];
        }
        return value;
    }

    private string GetRandomPickable()
    {
        string value = $"{InteractableName} it is...";
        if (Pickables != null && Pickables.Length >= 1)
        {
            int randomIndex = UnityEngine.Random.Range(0, Pickables.Length);
            value = Pickables[randomIndex];
        }
        return value;
    }

    private void GetIntoBuild()
    {
        
    }

}
