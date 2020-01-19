using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBag : InventoryUI {

    [Header("Fields to complete manually")]
    [SerializeField] private ItemType itemType;

    public delegate void SwapItemIdx(int sourceIdx, int targetIdx, ItemType itemType);
    public static event SwapItemIdx OnSwapItems;

    public delegate void DropItemIdx(int itemIdx, ItemType itemType);
    public static event DropItemIdx OnItemDrop;

    public delegate void DeleteItemIdx(int itemIdx, ItemType itemType);
    public static event DeleteItemIdx OnItemDelete;

    public delegate void ItemTypeChanged(ItemType itemType);
    public static event ItemTypeChanged OnItemTypeChanged;

    private void OnEnable() {
        StartCoroutine(this.Init());

        InventoryManager.OnItemDatabaseChanged += this.RefreshItems;
    }

    private void OnDisable() {
        InventoryManager.OnItemDatabaseChanged -= this.RefreshItems;
    }

    public void ChangeInventoryType(ItemType itemType) {
        this.itemType = itemType;
        this.RefreshItems(this.itemType);
        OnItemTypeChanged?.Invoke(this.itemType);
    }

    public ItemType GetCurrentItemType() {
        return this.itemType;
    }

    public override void SwapCells(InventoryCell source, InventoryCell target) {
        OnSwapItems?.Invoke(this.GetCellIdx(source), this.GetCellIdx(target), this.itemType);
    }

    public override void DropItem(InventoryCell cell) {
        OnItemDrop?.Invoke(this.GetCellIdx(cell), this.itemType);
    }

    public override void DeleteItem(InventoryCell cell) {
        OnItemDelete?.Invoke(this.GetCellIdx(cell), this.itemType);
    }

    private IEnumerator Init() {
        while(!InventoryManager.instance) {
            yield return null;
        }

        this.ChangeInventoryType(this.itemType);
    }

    private void RefreshItems(ItemType type) {
        // Continue if update concerns this inventory
        if(this.itemType.Equals(type)) {
            this.items = InventoryManager.instance.GetInventoryItems(this.itemType);
            this.RefreshUI();
        }
    }
}
