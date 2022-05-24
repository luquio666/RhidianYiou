using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleOptions : Module
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameEvents.ShowMainMenu();
        }
    }
}
