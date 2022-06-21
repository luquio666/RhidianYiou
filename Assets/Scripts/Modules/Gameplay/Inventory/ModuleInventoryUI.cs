using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleInventoryUI : Module
{
    public ModuleInventoryItemUI[] ItemButtons;
    public Text ItemAmount;
    public Text ItemsCount;

    private int _index;
    private List<InventoryItem> _inventory;

    private ModuleDialogData _dialogData;
    private ModuleDialogOptionData _option1;
    private ModuleDialogOptionData _option2;
    private ModuleDialogOptionData _option3;
    private ModuleDialogOptionData _option4;

    public void ShowInventory(List<InventoryItem> inventory)
    {
        _inventory = inventory;
        _index = 0;
        for (int i = 0; i < ItemButtons.Length; i++)
        {
            if (i > _inventory.Count - 1)
                ItemButtons[i].Description.text = $" - empty -";
            else
                ItemButtons[i].Description.text = _inventory[i].ItemName;
        }
        ClearButtons();
        SetSelectedItem(_index);
        UpdateItemCount();
    }

    private void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        if (UIController.Instance.DialogBoxIsActive())
            return;

        // Scroll through items
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetSelectedItem(-1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetSelectedItem(1);
        }

        // Actions
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameEvents.PlayAudio(AudioNames.CLICK_01);
            this.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameEvents.PlayAudio(AudioNames.CLICK_01);
            SetActionOverSelectedItem();
        }
    }

    private void SetSelectedItem(int i)
    {
        _index += i;
        if (_index > ItemButtons.Length - 1)
            _index = 0;
        if (_index < 0)
            _index = ItemButtons.Length - 1;

        if (_index > _inventory.Count - 1)
            ItemAmount.text = "-";
        else
            ItemAmount.text = _inventory[_index].ItemAmount.ToString();

        ClearButtons();
        UpdateItemCount();
        ItemButtons[_index].SetSelectedState();
    }

    private void SetActionOverSelectedItem()
    {
        if (_dialogData == null)
            _dialogData = gameObject.GetComponent<ModuleDialogData>();
        if (_dialogData == null)
            _dialogData = gameObject.AddComponent<ModuleDialogData>();



        if (_index > _inventory.Count - 1)
        {
            _dialogData.DialogText = $"this slot is empty...";
        }
        else
        {
            _dialogData.DialogText = $"You can do a number of things with this...";

            // Create options
            if (_option1 == null)
                _option1 = gameObject.AddComponent<ModuleDialogOptionData>();
            _option1.OptionText = $"Pick";
            _option1.OptionType = DialogOptions.CLOSE_DIALOG;

            if (_option2 == null)
                _option2 = gameObject.AddComponent<ModuleDialogOptionData>();
            _option2.OptionText = $"Craft";
            _option2.OptionType = DialogOptions.CLOSE_DIALOG;

            if (_option3 == null)
                _option3 = gameObject.AddComponent<ModuleDialogOptionData>();
            _option3.OptionText = $"Drop";
            _option3.OptionType = DialogOptions.CLOSE_DIALOG;

            if (_option4 == null)
                _option4 = gameObject.AddComponent<ModuleDialogOptionData>();
            _option4.OptionText = $"Leave";
            _option4.OptionType = DialogOptions.CLOSE_DIALOG;

            // Add option to dialogData
            _dialogData.Options = new ModuleDialogOptionData[] { _option1, _option2, _option3, _option4 };

        }

        GameEvents.ShowDialogData(_dialogData);
    }

    private void UpdateItemCount()
    {
        ItemsCount.text = $"{_index + 1}/{_inventory.Count}";
    }

    private void ClearButtons()
    {
        foreach (var item in ItemButtons)
        {
            item.SetUnselectedState();
        }
    }

}
