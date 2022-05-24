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
                string name = PlayerPrefs.GetString("PLAYER_NAME", string.Empty);
                if (string.IsNullOrEmpty(name))
                    Debug.LogError("NO SAVED NAME FOUND");
                else
                    GameEvents.LoadGameplay();

                break;
            case "New game":
                GameEvents.ShowNameInput();
                break;
            case "Options":
                GameEvents.ShowOptions();
                break;
            default:
                break;
        }
    }

    private void Initialize()
    {
        VerticalIndex = -1;
        foreach (var item in ButtonInputs)
        {
            item.SetUnselectedState();
        }
    }
    private void Update()
    {
        // Vertical movement
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveVertical(-1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveVertical(1);
        }

        // Button Pressed
        if (Input.GetKeyDown(KeyCode.L))
        {
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
