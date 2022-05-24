using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModuleInitialize : Module
{
    private void OnEnable()
    {
        Invoke(nameof(LoadMainMenu), 10f);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
