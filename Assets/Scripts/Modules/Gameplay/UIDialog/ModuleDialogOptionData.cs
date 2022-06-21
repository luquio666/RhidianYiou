using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogOptions
{
    CLOSE_DIALOG,
    SHOW_TARGET_DIALOG,
    GIVE_ITEM,
    ENTER_BUILDING
}

public class ModuleDialogOptionData : MonoBehaviour
{
    public string OptionText; // what will be displayed
    public DialogOptions OptionType; // how will react when option selected
    public string TargetID; // dialog ID or item ID

    [ContextMenu("SetName")]
    public void SetName()
    {
        this.name = $"Option [{OptionText}]";
        if (OptionType == DialogOptions.CLOSE_DIALOG)
        {
            this.name += $" [{OptionType}]";
        }
        else 
        {
            this.name += $" [{OptionType}: {TargetID}]";
        }
    }
}
