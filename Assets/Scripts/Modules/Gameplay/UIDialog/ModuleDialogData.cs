using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ModuleDialogData))]
public class ModuleDialogDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ModuleDialogData myScript = (ModuleDialogData)target;
        if (GUILayout.Button("SetName"))
        {
            myScript.SetName();
        }
    }
}
#endif

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
