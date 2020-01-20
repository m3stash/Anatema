using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBagUI : InventoryUI {

    [Header("Fields to complete manually")]
    [SerializeField] private ItemType itemType;

    public delegate void DropItemIdx(int itemIdx, ItemType itemType);
    public static event DropItemIdx OnItemDrop;

    public delegate void DeleteItemIdx(int itemIdx, ItemType itemType);
    public static event DeleteItemIdx OnItemDelete;

    public delegate void ReplacedItem(InventoryItemData item, int targetIdx, ItemType itemType);
    public static event ReplacedItem OnItemReplace;

    public delegate void TypeChanged(ItemType itemType);
    public static event TypeChanged OnTypeChanged;

    private void OnEnable() {
        InventoryManager.OnItemDatabaseChanged += this.RefreshItems;
        InventoryStepperUI.OnInventoryTypeChanged += ChangeInventoryType;
    }

    private void OnDisable() {
        InventoryManager.OnItemDatabaseChanged -= this.RefreshItems;
        InventoryStepperUI.OnInventoryTypeChanged -= ChangeInventoryType;
    }

    public void ChangeInventoryType(ItemType itemType) {
        this.itemType = itemType;

        // Update allowed item types foreach cell
        this.ManageCells(new ItemType[1] { itemType });

        this.RefreshItems(this.itemType);
        OnTypeChanged?.Invoke(this.itemType);
    }

    public ItemType GetCurrentItemType() {
        return this.itemType;
    }

    public override void DropItem(InventoryCell cell) {
        OnItemDrop?.Invoke(this.GetCellIdx(cell), this.itemType);
    }

    public override void DeleteItem(InventoryCell cell) {
        OnItemDelete?.Invoke(this.GetCellIdx(cell), this.itemType);
    }

    public override void ReplaceItem(InventoryItemData item, InventoryCell target) {
        OnItemReplace?.Invoke(item, this.GetCellIdx(target), this.itemType);
    }

    private void RefreshItems(ItemType type) {
        // Continue if update concerns this inventory
        if(this.itemType.Equals(type)) {
            this.items = InventoryManager.instance.GetInventoryItems(this.itemType);
            this.RefreshUI();
        }
    }
}
