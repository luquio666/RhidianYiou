using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleNameInput : Module
{
    public ModuleNameInputButtons[] VerticalButtonFiles;
    public int VerticalIndex;
    public int HorizontalIndex;

    private void Start()
    {
        VerticalIndex = 0;
        HorizontalIndex = 0;

        RefreshButtonsState();
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

        // Horizontal movement
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveHorizontal(-1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveHorizontal(1);
        }

        // Button Pressed
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (HorizontalIndex != -1 && VerticalIndex != -1)
                VerticalButtonFiles[VerticalIndex].HorizontalButtonInputs[HorizontalIndex].ButtonPressed();
        }
    }

    private void MoveVertical(int direction)
    {
        VerticalIndex = GetVerticalIndex(direction);
        HorizontalIndex = GetHorizontalIndex(0);

        RefreshButtonsState();
    }

    private void MoveHorizontal(int direction)
    {
        VerticalIndex = GetVerticalIndex(0);
        HorizontalIndex = GetHorizontalIndex(direction);

        RefreshButtonsState();
    }

    private void RefreshButtonsState()
    {
        for (int i = 0; i < VerticalButtonFiles.Length; i++)
        {
            for (int j = 0; j < VerticalButtonFiles[i].HorizontalButtonInputs.Length; j++)
            {
                if (i == VerticalIndex && j == HorizontalIndex)
                    VerticalButtonFiles[i].HorizontalButtonInputs[j].SetSelectedState();
                else
                    VerticalButtonFiles[i].HorizontalButtonInputs[j].SetUnselectedState();
            }
        }
    }

    private int GetVerticalIndex(int direction)
    {
        int result = 0;

        if (VerticalIndex == -1)
            result = 0;
        else
            result = VerticalIndex + direction;

        if (result == VerticalButtonFiles.Length)
            result = 0;
        if (result < 0)
            result = VerticalButtonFiles.Length - 1;

        return result;
    }

    private int GetHorizontalIndex(int direction)
    {
        int result = 0;

        if (HorizontalIndex == -1)
            result = 0;
        else
            result = HorizontalIndex + direction;

        if (result == VerticalButtonFiles[VerticalIndex].HorizontalButtonInputs.Length)
            result = 0;
        if (result < 0)
            result = VerticalButtonFiles[VerticalIndex].HorizontalButtonInputs.Length - 1;

        return result;
    }
}
