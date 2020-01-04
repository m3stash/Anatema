using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Fields to complete manually")]
    [SerializeField] private GameObject inventoryCanvas;

    [Header("Inventory Item databases")]
    [SerializeField] private Dictionary<ItemType, InventoryItemData[]> itemDatabases;

    private int size = 16;

    public static InventoryManager instance;

    public delegate void OnItemDatabaseChanged(ItemType itemType);
    public static event OnItemDatabaseChanged itemDatabaseChanged;

    void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;

            // Init all inventories type
            this.itemDatabases = new Dictionary<ItemType, InventoryItemData[]>();

            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                this.itemDatabases.Add(type, new InventoryItemData[this.size]);
            }
        }
    }

    public void SwitchDisplay()
    {
        this.inventoryCanvas.SetActive(!this.inventoryCanvas.activeSelf);
    }

    public InventoryItemData[] GetInventoryItems(ItemType itemType)
    {
        return this.itemDatabases[itemType];
    }

    public bool AddItem(Item item)
    {
        // Get reference to associated database of item type
        InventoryItemData[] itemDatabase = this.itemDatabases[item.GetConfig().GetItemType()];

        // If item is stackable try to find if we can stack it
        if (item.GetConfig().IsStackable())
        {
            int slotIdx = this.GetItemSlotIdx(itemDatabase, item, true);

            // If same item found in inventory and can be stacked so stack it
            if (slotIdx != -1)
            {
                itemDatabase[slotIdx].AddStacks(item.GetStacks());

                // Notify item database changed to refresh UI
                itemDatabaseChanged?.Invoke(item.GetConfig().GetItemType());
                return true;
            }
        }

        // If item isn't stackable or it couldn't be stacked so try to add it
        int freeSlotIdx = this.GetEmptySlotIdx(itemDatabase);

        if (freeSlotIdx != -1)
        {
            // Create new inventory item in this cell and setup it
            itemDatabase[freeSlotIdx] = new InventoryItemData(item.GetConfig(), item.GetStacks(), 100);

            // Notify item database changed to refresh UI
            itemDatabaseChanged?.Invoke(item.GetConfig().GetItemType());

            return true;
        }


        Debug.Log("No slot found to add item");
        return false;
    }


    private int GetEmptySlotIdx(InventoryItemData[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i]?.GetConfig() == null)
            {
                return i;
            }
        }

        return -1;
    }

    private int GetItemSlotIdx(InventoryItemData[] items, Item item, bool stackableFilter = false)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i]?.GetConfig() != null)
            {
                bool itemFoundById = item.GetConfig().GetId().Equals(items[i].GetConfig().GetId());

                // Check if its same item and addition of stacks is less than stackLimit
                if (itemFoundById && ((stackableFilter && items[i].CanStack(item.GetStacks())) || !stackableFilter))
                {
                    return i;
                }
            }
        }

        return -1;
    }

    //private int GetCellIdx(InventoryCell cell) {
    //    for(int i = 0; i < this.cells.Length; i++) {
    //        if(this.cells[i] == cell) {
    //            return i;
    //        }
    //    }

    //    return -1;
    //}

    /*private void ItemChangedInCell(InventoryCell cell) {
        int cellIdx = this.GetCellIdx(cell);

        if(cellIdx != -1) {
            this.itemDatas[cellIdx] = cell.GetInventoryItem() ? cell.GetInventoryItem().GetItem() : null;
        } else {
            Debug.LogError("Cell idx not found");
        }
    }*/

    /*private void DropItem(Item item) {
        int cellIdx = this.GetCellIdx(cell);

        if (cellIdx != -1) {
            // Create item in world
            InventoryItemData itemData = cell.GetInventoryItem().GetItem();
            Item item = ItemManager.instance.CreateItem(itemData.GetConfig().GetId(), ItemStatus.PICKABLE, Player.instance.transform.position + Vector3.right);
            item.SetStacks(itemData.GetStacks());

            // Delete from inventory and refresh ui
            this.itemDatas[cellIdx] = null;
            this.RefreshUI();
        } else {
            Debug.LogError("Cell idx not found");
        }
    }*/
}
