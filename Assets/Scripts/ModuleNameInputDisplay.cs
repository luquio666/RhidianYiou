using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleNameInputDisplay : MonoBehaviour
{

    public Text NameDisplay;

    private string _name = string.Empty;

    private const string PRE_NAME_TEXT = "Name:";
    private const string EMPTY_CHAR = "-";
    private const int MAX_CHARACTERS = 13;

    private void OnEnable()
    {
        GameEvents.OnButtonInputPressed += ButtonInputPressed;
    }

    private void OnDisable()
    {
        GameEvents.OnButtonInputPressed -= ButtonInputPressed;
    }

    private void Start()
    {
        FillNameWithEmpty();
    }

    private void ButtonInputPressed(string s)
    {
        if (s == "del")
        {
            if(_name.Length > 0)
                _name = _name.Remove(_name.Length - 1);
        }
        else if (s == "space")
        {
            if(_name.Length > 0)
                _name += " ";
        }
        else if (s == "save")
        {
            GameEvents.SaveName(_name);
        }
        else if (_name.Length < MAX_CHARACTERS && s.Length == 1)
        {
            _name += s;
        }

        FillNameWithEmpty();
    }

    private void FillNameWithEmpty()
    {
        NameDisplay.text = PRE_NAME_TEXT + _name;
        for (int i = _name.Length; i < MAX_CHARACTERS; i++)
        {
            NameDisplay.text += EMPTY_CHAR;
        }
    }
}
