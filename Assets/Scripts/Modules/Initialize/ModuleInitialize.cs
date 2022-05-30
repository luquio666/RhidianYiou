using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModuleInitialize : Module
{
    public float TimeToNextScene = 5f;
    private void OnEnable()
    {
        Invoke(nameof(LoadMainMenu), TimeToNextScene);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
