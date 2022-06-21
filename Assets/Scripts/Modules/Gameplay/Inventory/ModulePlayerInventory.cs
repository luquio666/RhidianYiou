using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string ItemName;
    public int ItemAmount;
}

public class ModulePlayerInventory : Module
{
    public List<InventoryItem> Inventory;
    public int MaxItems = 10; // TODO: locked cuz UI has only 10 slots

    private ModuleDialogData _dialogData;

    private void Awake()
    {
        Inventory = new List<InventoryItem>();
    }

    private void OnEnable()
    {
        GameEvents.OnGiveItem += GiveItem;
    }

    private void OnDisable()
    {
        GameEvents.OnGiveItem -= GiveItem;
    }

    private void GiveItem(string itemName)
    {
        AddItem(itemName);
    }

    public void ShowInventory()
    {
        GameEvents.ShowInventory(Inventory);
    }

    public void AddItem(string itemName)
    {
        var itemFound = Inventory.Find(x => x.ItemName == itemName);
        if (itemFound != null)
        {
            itemFound.ItemAmount++;
        }
        else
        {
            if (Inventory.Count >= MaxItems)
            {
                // TODO: quickfix to avoid hide while this trying to be shown...
                Invoke(nameof(ShowMaxItemDialog), .1f);
            }
            else
            {
                InventoryItem newItem = new InventoryItem
                {
                    ItemName = itemName,
                    ItemAmount = 1
                };
                Inventory.Add(newItem);
            }
        }
    }

    public void RemoveItem(InventoryItem item)
    {

    }

    private void ShowMaxItemDialog()
    {
        if (_dialogData == null)
            _dialogData = gameObject.GetComponent<ModuleDialogData>();
        if (_dialogData == null)
            _dialogData = gameObject.AddComponent<ModuleDialogData>();
        _dialogData.DialogText = $"I only have {MaxItems} slots. This will be lost...";
        GameEvents.ShowDialogData(_dialogData);
    }
}
