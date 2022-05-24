using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModuleMainMenuUIController : Module
{
    public ModuleIntro Intro;
    public ModuleMenuInput MainMenu;
    public ModuleNameInput NameInput;
    public ModuleOptions Options;

    private void OnEnable()
    {
        Initialize();
        GameEvents.OnShowIntro += ShowIntroUI;
        GameEvents.OnShowMainMenu += ShowMainMenuUI;
        GameEvents.OnShowNameInput += ShowNameInputUI;
        GameEvents.OnShowOptions += ShowOptionsUI;

        GameEvents.OnSaveName += SaveName;
        GameEvents.OnLoadGameplay += LoadGameplay;
    }

    private void OnDisable()
    {
        GameEvents.OnShowIntro -= ShowIntroUI;
        GameEvents.OnShowMainMenu -= ShowMainMenuUI;
        GameEvents.OnShowNameInput -= ShowNameInputUI;
        GameEvents.OnShowOptions -= ShowOptionsUI;

        GameEvents.OnSaveName -= SaveName;
        GameEvents.OnLoadGameplay -= LoadGameplay;
    }

    private void Initialize()
    {
        ShowUI(Intro.gameObject);
        Intro.PlayIntro();
    }

    private void SaveName(string s)
    {
        PlayerPrefs.SetString("PLAYER_NAME", s);
    }

    private void LoadGameplay()
    {
        SceneManager.LoadScene(2);
    }

    private void ShowIntroUI()
    {
        ShowUI(Intro.gameObject);
    }

    private void ShowMainMenuUI()
    {
        ShowUI(MainMenu.gameObject);
    }

    private void ShowNameInputUI()
    {
        ShowUI(NameInput.gameObject);
    }

    private void ShowOptionsUI()
    {
        ShowUI(Options.gameObject);
    }

    private void ShowUI(GameObject target)
    {
        Intro.gameObject.SetActive(target == Intro.gameObject);
        MainMenu.gameObject.SetActive(target == MainMenu.gameObject);
        NameInput.gameObject.SetActive(target == NameInput.gameObject);
        Options.gameObject.SetActive(target == Options.gameObject);
    }
}
