using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    [Header("Fields to complete manually")]
    [SerializeField] private int size = 32;

    [Header("Inventory Item databases")]
    [SerializeField] private Dictionary<ItemType, InventoryItemData[]> itemDatabases;

    public static InventoryManager instance;

    public delegate void ItemDatabaseChanged(ItemType itemType);
    public static event ItemDatabaseChanged OnItemDatabaseChanged;

    void Awake() {
        if(instance) {
            Destroy(this);
        } else {
            instance = this;

            // Init all inventories type
            this.itemDatabases = new Dictionary<ItemType, InventoryItemData[]>();

            foreach(ItemType type in Enum.GetValues(typeof(ItemType))) {
                this.itemDatabases.Add(type, new InventoryItemData[this.size]);
            }
        }
    }

    public InventoryItemData[] GetInventoryItems(ItemType itemType) {
        return this.itemDatabases[itemType];
    }

    /// <summary>
    /// Used to add an item to inventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool AddItem(Item item) {
        // Get reference to associated database of item type
        InventoryItemData[] itemDatabase = this.itemDatabases[item.GetConfig().GetItemType()];

        // If item is stackable try to find if we can stack it
        if(item.GetConfig().IsStackable()) {
            int slotIdx = InventoryUtils.GetItemSlotIdx(itemDatabase, item, true);

            // If same item found in inventory and can be stacked so stack it
            if(slotIdx != -1) {
                itemDatabase[slotIdx].AddStacks(item.GetStacks());

                // Get overflow stacks
                int overflowStacks = itemDatabase[slotIdx].GetOverflowStacks();

                // If greater than 0, target item has exceeded its stack limit so insert new item
                if(overflowStacks > 0) {
                    item.SetStacks(overflowStacks);
                    itemDatabase[slotIdx].RemoveStacks(overflowStacks);
                } else {
                    // Notify item database changed to refresh UI
                    OnItemDatabaseChanged?.Invoke(item.GetConfig().GetItemType());
                    return true;
                }
            }
        }

        // If item isn't stackable or it couldn't be stacked so try to add it
        int freeSlotIdx = InventoryUtils.GetEmptySlotIdx(itemDatabase);

        if(freeSlotIdx != -1) {
            // Create new inventory item in this cell and setup it
            itemDatabase[freeSlotIdx] = new InventoryItemData(item.GetConfig(), item.GetStacks(), 100);

            // Notify item database changed to refresh UI
            OnItemDatabaseChanged?.Invoke(item.GetConfig().GetItemType());

            return true;
        }


        Debug.Log("No slot found to add item");
        return false;
    }

    /// <summary>
    /// Used to replace an item to inventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public void ReplaceItem(InventoryItemData item, int targetIdx, ItemType itemType) {
        // Get reference to associated database of item type
        InventoryItemData[] itemDatabase = this.itemDatabases[itemType];

        itemDatabase[targetIdx] = item;

        // Notify item database changed to refresh UI
        OnItemDatabaseChanged?.Invoke(itemType);
    }

    /// <summary>
    /// Check if item can be added or not
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool CanAddItem(Item item) {
        // Get reference to associated database of item type
        InventoryItemData[] itemDatabase = this.itemDatabases[item.GetConfig().GetItemType()];

        return InventoryUtils.GetItemSlotIdx(itemDatabase, item, true) != -1 || InventoryUtils.GetEmptySlotIdx(itemDatabase) != -1;
    }

    /// <summary>
    /// Used to delete item at specific index
    /// </summary>
    /// <param name="itemIdx"></param>
    /// <param name="itemType"></param>
    public void DeleteItem(int itemIdx, ItemType itemType) {
        // Get reference to associated database of item type
        InventoryItemData[] itemDatabase = this.itemDatabases[itemType];

        // Remove item from database
        itemDatabase[itemIdx] = null;

        OnItemDatabaseChanged?.Invoke(itemType);
    }

    /// <summary>
    /// Used to drop an item of inventory in the world
    /// </summary>
    /// <param name="itemIdx"></param>
    /// <param name="itemType"></param>
    public void DropItem(int itemIdx, ItemType itemType) {
        // Get reference to associated database of item type
        InventoryItemData[] itemDatabase = this.itemDatabases[itemType];
        InventoryItemData itemData = itemDatabase[itemIdx];

        // Create item in world
        Vector3 positionToSpawn = Player.instance.transform.position + new Vector3(Player.instance.transform.localScale.x * 1.3f, 0);
        Item item = ItemManager.instance.CreateItem(itemData.GetConfig().GetId(), ItemStatus.PICKABLE, positionToSpawn);
        item.SetStacks(itemData.GetStacks());

        // Delete from inventory and refresh ui
        itemDatabase[itemIdx] = null;

        OnItemDatabaseChanged?.Invoke(itemType);
    }
}
