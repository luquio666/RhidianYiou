using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ModuleDialogOptionsUI
{
    public GameObject Button;
    public Text ButtonText;
    public GameObject Selected;
}

public class ModuleDialogUI : Module
{
    public Text DialogBox;
    public ModuleDialogOptionsUI[] DialogOptions;
    public GameObject MoreTextArrow;
    public float TimeBetweenChars = 0.05f;

    private int _maxCharsPerLine = 18;
    private int _maxLines = 3;
    private Coroutine _showTextCo;
    private ModuleDialogData _dialogData;

    private int _verticalIndex;
    private bool _optionsEnabled;

    public void ShowText(ModuleDialogData data)
    {
        _dialogData = data;
        if (_showTextCo != null)
        {
            StopCoroutine(_showTextCo);
        }
        _showTextCo = StartCoroutine(ShowTextCo(_dialogData.DialogText));
    }

    private void ResetDefaultState()
    {
        DialogBox.text = string.Empty;
        MoreTextArrow.SetActive(false);
        ClearAndStopDialogOptions();
    }

    private void ClearAndStopDialogOptions()
    {
        _verticalIndex = 0;
        _optionsEnabled = false;
        foreach (var item in DialogOptions)
        {
            item.Button.SetActive(false);
            item.ButtonText.text = string.Empty;
            item.Selected.SetActive(false);
        }
    }

    private IEnumerator ShowTextCo(string s)
    {
        ResetDefaultState();

        // split text by spaces
        var words = s.Split(' ').ToList();

        List<string> allTextLines = new List<string>();
        string singleTextLine = string.Empty;

        // Create lines of text without exceeding _masCharsPerLine
        for (int i = 0; i < words.Count; i++)
        {
            bool lineExceedsLimit = singleTextLine.Length + 1 + words[i].Length > _maxCharsPerLine;
            if (!lineExceedsLimit)
            {
                if (singleTextLine != string.Empty)
                    singleTextLine += " ";
                singleTextLine += words[i];
                if (i + 1 == words.Count)
                    allTextLines.Add(singleTextLine);
            }
            else
            {
                allTextLines.Add(singleTextLine);
                // first line again
                singleTextLine = string.Empty;
                singleTextLine += words[i];

                // make sure to add line to lineList
                if (i + 1 == words.Count)
                    allTextLines.Add(singleTextLine);
            }
        }

        int linesCount = 0;
        for (int i = 0; i < allTextLines.Count; i++)
        {
            MoreTextArrow.SetActive(false);
            singleTextLine = allTextLines[i];
            for (int j = 0; j < singleTextLine.Length; j++)
            {
                // Check if line ended to add a return
                string addReturn = j == singleTextLine.Length - 1 ? "\n" : "";
                DialogBox.text += singleTextLine[j] + addReturn;
                yield return new WaitForSeconds(TimeBetweenChars);
            }
            linesCount++;
            if (linesCount == _maxLines || i == allTextLines.Count - 1)
            {
                bool showArrow = !(i == allTextLines.Count - 1);
                MoreTextArrow.SetActive(showArrow);
                while (!Input.GetKeyDown(KeyCode.L))
                    yield return null;
                linesCount = 0;

                //DialogBox.text = string.Empty;
            }
        }
        if (_dialogData.Options != null && _dialogData.Options.Length > 0)
        {
            SetOptions();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        yield return null;
    }

    
    private void SetOptions()
    {
        ClearAndStopDialogOptions();
        _optionsEnabled = true;

        var options = _dialogData.Options;
        for (int i = 0; i < _dialogData.Options.Length; i++)
        {
            if (i >= DialogOptions.Length - 1)
            {
                Debug.LogError("ModuleDialogUI :: more options than available to display.");
            }
            else
            {
                DialogOptions[i].Button.SetActive(true);
                DialogOptions[i].ButtonText.text = _dialogData.Options[i].OptionText;
                DialogOptions[i].Selected.SetActive(i == _verticalIndex);
            }
        }
    }

    private void Update()
    {
        if (!_optionsEnabled)
            return;

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
            ButtonPressed();
        }
    }

    private void MoveVertical(int direction)
    {
        _verticalIndex = GetVerticalIndex(direction);

        for (int i = 0; i < DialogOptions.Length; i++)
        {
            if (i == _verticalIndex)
                DialogOptions[i].Selected.SetActive(true);
            else
                DialogOptions[i].Selected.SetActive(false);
        }
    }

    private int GetVerticalIndex(int direction)
    {
        int optionsCount = GetOptionsCount();
        int result = 0;

        if (_verticalIndex == -1)
            result = 0;
        else
            result = _verticalIndex + direction;

        if (result == optionsCount)
            result = 0;
        if (result < 0)
            result = optionsCount - 1;

        return result;
    }

    private int GetOptionsCount()
    {
        int result = 0;
        for (int i = 0; i < DialogOptions.Length; i++)
        {
            if (DialogOptions[i].Button.activeInHierarchy)
                result++;
        }
        return result;
    }

    private void ButtonPressed()
    {
        // find dialog data and what it does
        var optionData = _dialogData.Options[_verticalIndex];
        Debug.Log($"{optionData.OptionType} : {optionData.TargetID}");
        switch (optionData.OptionType)
        {
            case global::DialogOptions.CLOSE_DIALOG:
                this.gameObject.SetActive(false);
                break;
            case global::DialogOptions.SHOW_TARGET_DIALOG:
                GameEvents.ShowDialog(optionData.TargetID);
                break;
            case global::DialogOptions.GIVE_ITEM:
                GameEvents.GiveItem(optionData.TargetID);
                this.gameObject.SetActive(false);
                break;
            default:
                break;
        }

        // hide options
        //ClearAndStopDialogOptions();
    }
}
