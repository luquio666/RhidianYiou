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

    private void GiveItem(string itemName, int itemAmount)
    {
        AddItem(itemName, itemAmount);
    }

    public void ShowInventory()
    {
        GameEvents.ShowInventory(Inventory);
    }

    public void AddItem(string itemName, int itemAmount)
    {
        // Check if the item is already on the inventory
        var itemFound = Inventory.Find(x => x.ItemName == itemName);
        if (itemFound != null)
        {
            GameEvents.QuestEvent_PickItem(itemName, itemAmount);

            itemFound.ItemAmount += itemAmount;
        }
        else
        {
            // Check if we have space to store items
            if (Inventory.Count >= MaxItems)
            {
                // TODO: quickfix to avoid hide while this trying to be shown...
                Invoke(nameof(ShowMaxItemDialog), .1f);
            }
            else
            {
                GameEvents.QuestEvent_PickItem(itemName, itemAmount);

                InventoryItem newItem = new InventoryItem
                {
                    ItemName = itemName,
                    ItemAmount = itemAmount
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
