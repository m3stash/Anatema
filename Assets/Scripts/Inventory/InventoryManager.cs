using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    [Header("Fields to complete manually")]
    [SerializeField] private GameObject inventoryPanelUI;
    [SerializeField] private int size = 16;

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

            InventoryBag.OnSwapItems += SwapItems;
            InventoryBag.OnItemDrop += DropItem;

            // Init all inventories type
            this.itemDatabases = new Dictionary<ItemType, InventoryItemData[]>();

            foreach(ItemType type in Enum.GetValues(typeof(ItemType))) {
                this.itemDatabases.Add(type, new InventoryItemData[this.size]);
            }
        }
    }

    private void OnDestroy() {
        InventoryBag.OnSwapItems -= SwapItems;
        InventoryBag.OnItemDrop -= DropItem;
    }

    public void SwitchDisplay() {
        this.inventoryPanelUI.SetActive(!this.inventoryPanelUI.activeSelf);
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
            int slotIdx = this.GetItemSlotIdx(itemDatabase, item, true);

            // If same item found in inventory and can be stacked so stack it
            if(slotIdx != -1) {
                itemDatabase[slotIdx].AddStacks(item.GetStacks());

                // Notify item database changed to refresh UI
                OnItemDatabaseChanged?.Invoke(item.GetConfig().GetItemType());
                return true;
            }
        }

        // If item isn't stackable or it couldn't be stacked so try to add it
        int freeSlotIdx = this.GetEmptySlotIdx(itemDatabase);

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
    /// Used to delete item at specific index
    /// </summary>
    /// <param name="itemIdx"></param>
    /// <param name="itemType"></param>
    private void DeleteItem(int itemIdx, ItemType itemType) {
        // Get reference to associated database of item type
        InventoryItemData[] itemDatabase = this.itemDatabases[itemType];

        // Remove item from database
        itemDatabase[itemIdx] = null;

        OnItemDatabaseChanged?.Invoke(itemType);
    }

    /// <summary>
    /// Get idx of empty slot of inventory
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    private int GetEmptySlotIdx(InventoryItemData[] items) {
        for(int i = 0; i < items.Length; i++) {
            if(items[i]?.GetConfig() == null) {
                return i;
            }
        }

        return -1;
    }


    /// <summary>
    /// Get item idx of item found in function of parameters
    /// </summary>
    /// <param name="items"></param>
    /// <param name="item"></param>
    /// <param name="stackableFilter"></param>
    /// <returns></returns>
    private int GetItemSlotIdx(InventoryItemData[] items, Item item, bool stackableFilter = false) {
        for(int i = 0; i < items.Length; i++) {
            if(items[i]?.GetConfig() != null) {
                bool itemFoundById = item.GetConfig().GetId().Equals(items[i].GetConfig().GetId());

                // Check if its same item ID and addition of stacks is less than stackLimit
                if(itemFoundById && ((stackableFilter && items[i].CanStack(item.GetStacks())) || !stackableFilter)) {
                    return i;
                }
            }
        }

        return -1;
    }

    /// <summary>
    /// Used to swap item slot in inventory
    /// </summary>
    /// <param name="sourceIdx"></param>
    /// <param name="targetIdx"></param>
    /// <param name="itemType"></param>
    private void SwapItems(int sourceIdx, int targetIdx, ItemType itemType) {
        // Get reference to associated database of item type
        InventoryItemData[] itemDatabase = this.itemDatabases[itemType];

        InventoryItemData sourceItem = itemDatabase[sourceIdx];
        InventoryItemData targetItem = itemDatabase[targetIdx];

        // If items are same and target cell can be stacked
        if(targetItem != null && targetItem.IsSameThan(sourceItem) && targetItem.CanStack(sourceItem.GetStacks())) {
            targetItem.AddStacks(sourceItem.GetStacks());

            itemDatabase[sourceIdx] = null;
            itemDatabase[targetIdx] = targetItem;
        } else {
            itemDatabase[targetIdx] = sourceItem;
            itemDatabase[sourceIdx] = targetItem;
        }


        OnItemDatabaseChanged?.Invoke(itemType);
    }

    /// <summary>
    /// Used to drop an item of inventory in the world
    /// </summary>
    /// <param name="itemIdx"></param>
    /// <param name="itemType"></param>
    private void DropItem(int itemIdx, ItemType itemType) {
        // Get reference to associated database of item type
        InventoryItemData[] itemDatabase = this.itemDatabases[itemType];
        InventoryItemData itemData = itemDatabase[itemIdx];

        // Create item in world
        Vector3 positionToSpawn = Player.instance.transform.position + new Vector3(Player.instance.transform.localScale.x, 0);
        Item item = ItemManager.instance.CreateItem(itemData.GetConfig().GetId(), ItemStatus.PICKABLE, positionToSpawn);
        item.SetStacks(itemData.GetStacks());

        // Delete from inventory and refresh ui
        itemDatabase[itemIdx] = null;

        OnItemDatabaseChanged?.Invoke(itemType);
    }
}
