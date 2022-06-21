using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
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
#endif

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
