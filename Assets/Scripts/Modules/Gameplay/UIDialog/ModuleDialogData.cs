using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleDialogData : Module
{
    public string ID;
    public string DialogText;
    public ModuleDialogOptionData[] Options; // no options should close dialog

    [ContextMenu("SetName")]
    public void SetName()
    {
        this.name = $"DialogData [ID: {ID}]";
    }
}
