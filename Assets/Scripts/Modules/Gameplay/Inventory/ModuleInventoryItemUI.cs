using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleInventoryItemUI : Module
{
    public Text Description;
    [Space]
    public GameObject Selected;

    public void SetSelectedState()
    {
        Selected.SetActive(true);
    }

    public void SetUnselectedState()
    {
        Selected.SetActive(false);
    }
}
