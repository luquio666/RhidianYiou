using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Singleton<UIController>
{

    public ModuleDialogUI DialogBox;

    public List<ModuleDialogData> DialogData;

    private void OnEnable()
    {
        ResetDefaultState();

        GameEvents.OnShowDialog += ShowDialog;
        GameEvents.OnShowDialogData += ShowDialogData;
    }

    private void OnDisable()
    {
        GameEvents.OnShowDialog -= ShowDialog;
        GameEvents.OnShowDialogData -= ShowDialogData;
    }


    public bool IsUIActive()
    {
        if (DialogBox.gameObject.activeSelf)
            return true;

        return false;
    }

    private void ResetDefaultState()
    {
        DialogBox.gameObject.SetActive(false);
    }

    private void ShowDialog(string id)
    {
        var dialogData = GetDialogData(id);
        if (dialogData != null)
        {
            DialogBox.gameObject.SetActive(true);
            DialogBox.ShowText(dialogData);
        }
    }

    private void ShowDialogData(ModuleDialogData dialogData)
    {
        if (dialogData != null)
        {
            DialogBox.gameObject.SetActive(true);
            DialogBox.ShowText(dialogData);
        }
    }

    private ModuleDialogData GetDialogData(string id)
    {
        ModuleDialogData result = null;

        foreach (var item in DialogData)
        {
            if (item.ID == id)
                return item;
        }

        return result;
    }
}
