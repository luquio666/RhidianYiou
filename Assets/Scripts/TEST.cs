using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TEST))]
public class TESTEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TEST myScript = (TEST)target;
        if (GUILayout.Button("TEST DIALOG"))
        {
            myScript.TestDialog();
        }
        if (GUILayout.Button("DELETE SAVE DATA"))
        {
            myScript.DeletePlayerPrefs();
        }
    }
}

public class TEST : MonoBehaviour
{
    public string DialogDataID;
    public void TestDialog()
    {
        GameEvents.ShowDialog(DialogDataID);
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
