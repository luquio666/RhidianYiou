using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMenuInput : Module
{

    public ModuleButtonInput[] ButtonInputs;
    public int VerticalIndex;

    private void OnEnable()
    {
        Initialize();
        GameEvents.OnButtonInputPressed += ButtonInputPressed;
    }

    private void OnDisable()
    {
        GameEvents.OnButtonInputPressed -= ButtonInputPressed;
    }

    private void ButtonInputPressed(string s)
    {
        switch (s)
        {
            case "Continue":
                if (SavedDataAvailable())
                {
                    GameEvents.PlayAudio(AudioNames.CLICK_01);
                    GameEvents.LoadGameplay();
                }
                else
                    Debug.LogError("NO SAVED NAME FOUND");

                break;
            case "New game":
                GameEvents.PlayAudio(AudioNames.CLICK_01);
                GameEvents.ShowNameInput();
                break;
            case "Options":
                GameEvents.ShowOptions();
                break;
            default:
                break;
        }
    }

    private bool SavedDataAvailable()
    {
        return !string.IsNullOrEmpty(PlayerPrefs.GetString("PLAYER_NAME", string.Empty));
    }

    private void Initialize()
    {
        // when saved data available, position selection on continue[0]. Otherwise on new game [1]
        VerticalIndex = SavedDataAvailable() ? 0 : 1;

        RefreshButtonsState();
    }

    private void RefreshButtonsState()
    {
        for (int i = 0; i < ButtonInputs.Length; i++)
        {
            if (i == VerticalIndex)
                ButtonInputs[i].SetSelectedState();
            else
                ButtonInputs[i].SetUnselectedState();
        }
    }

    private void Update()
    {
        // Vertical movement
        if (Input.GetKeyDown(KeyCode.W))
        {
            GameEvents.PlayAudio(AudioNames.CLICK_01);
            MoveVertical(-1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameEvents.PlayAudio(AudioNames.CLICK_01);
            MoveVertical(1);
        }

        // Button Pressed
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameEvents.PlayAudio(AudioNames.CLICK_01);
            if (VerticalIndex != -1)
                ButtonInputs[VerticalIndex].ButtonPressed();
        }
    }

    private void MoveVertical(int direction)
    {
        VerticalIndex = GetVerticalIndex(direction);

        for (int i = 0; i < ButtonInputs.Length; i++)
        {
            if (i == VerticalIndex)
                ButtonInputs[i].SetSelectedState();
            else
                ButtonInputs[i].SetUnselectedState();
        }
    }

    private int GetVerticalIndex(int direction)
    {
        int result = 0;

        if (VerticalIndex == -1)
            result = 0;
        else
            result = VerticalIndex + direction;

        if (result == ButtonInputs.Length)
            result = 0;
        if (result < 0)
            result = ButtonInputs.Length - 1;

        return result;
    }

}
