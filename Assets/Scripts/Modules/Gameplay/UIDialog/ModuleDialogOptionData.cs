using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ModuleDialogOptionData))]
public class ModuleDialogOptionDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ModuleDialogOptionData myScript = (ModuleDialogOptionData)target;
        if (GUILayout.Button("SetName"))
        {
            myScript.SetName();
        }
    }
}
#endif

public enum DialogOptions
{
    CLOSE_DIALOG,
    SHOW_TARGET_DIALOG,
    GIVE_ITEM,
    ENTER_BUILDING,
    BLOCK_GAME
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
