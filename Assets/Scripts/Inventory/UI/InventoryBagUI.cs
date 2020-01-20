using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBagUI : InventoryUI {

    [Header("Fields to complete manually")]
    [SerializeField] private ItemType itemType;

    private void OnEnable() {
        InventoryManager.OnItemDatabaseChanged += this.RefreshItems;
    }

    private void OnDisable() {
        InventoryManager.OnItemDatabaseChanged -= this.RefreshItems;
    }

    public void ChangeInventoryType(ItemType itemType) {
        this.itemType = itemType;

        // Update allowed item types foreach cell
        this.ManageCells(new ItemType[1] { itemType });

        this.RefreshItems(this.itemType);
    }

    public ItemType GetCurrentItemType() {
        return this.itemType;
    }

    public override void DropItem(InventoryCell cell) {
        InventoryManager.instance.DropItem(this.GetCellIdx(cell), this.itemType);
    }

    public override void DeleteItem(InventoryCell cell) {
        InventoryManager.instance.DeleteItem(this.GetCellIdx(cell), this.itemType);
    }

    public override void ReplaceItem(InventoryItemData item, InventoryCell target) {
        InventoryManager.instance.ReplaceItem(item, this.GetCellIdx(target), this.itemType);
    }

    private void RefreshItems(ItemType type) {
        // Continue if update concerns this inventory
        if(this.itemType.Equals(type)) {
            this.items = InventoryManager.instance.GetInventoryItems(this.itemType);
            this.RefreshUI();
        }
    }
}
