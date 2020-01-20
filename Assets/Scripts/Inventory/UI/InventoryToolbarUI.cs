using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryToolbarUI : InventoryUI
{
    [Header("Fields to complete manually")]
    [SerializeField] private ToolbarConfig toolbarConfig;

    public delegate void DropItemIdx(int itemIdx, ToolbarType toolbarType);
    public static event DropItemIdx OnItemDrop;

    public delegate void DeleteItemIdx(int itemIdx, ToolbarType toolbarType);
    public static event DeleteItemIdx OnItemDelete;

    public delegate void ReplacedItem(InventoryItemData item, int targetIdx, ToolbarType toolbarType);
    public static event ReplacedItem OnItemReplace;

    private void OnEnable() {
        ToolbarManager.OnToolbarChanged += UpdateToolbar;
        InventoryStepperUI.OnInventoryTypeChanged += ChangeToolbarType;
    }

    private void OnDisable() {
        ToolbarManager.OnToolbarChanged -= UpdateToolbar;
        InventoryStepperUI.OnInventoryTypeChanged -= ChangeToolbarType;
    }

    public void ChangeToolbarType(ItemType itemType) {
        this.toolbarConfig = ToolbarManager.instance.GetToolbarConfig(itemType);

        // Update allowed item types foreach cell
        this.ManageCells(this.toolbarConfig.GetAllowedItemTypes(), this.toolbarConfig.GetSize());

        this.RefreshItems(this.toolbarConfig.GetToolbarType());
    }

    public ToolbarType GetCurrentToolbarType() {
        return this.toolbarConfig.GetToolbarType();
    }

    public override void DropItem(InventoryCell cell) {
        OnItemDrop?.Invoke(this.GetCellIdx(cell), this.toolbarConfig.GetToolbarType());
    }

    public override void DeleteItem(InventoryCell cell) {
        OnItemDelete?.Invoke(this.GetCellIdx(cell), this.toolbarConfig.GetToolbarType());
    }

    public override void ReplaceItem(InventoryItemData inventoryItem, InventoryCell target) {
        OnItemReplace?.Invoke(inventoryItem, this.GetCellIdx(target), this.toolbarConfig.GetToolbarType());
    }

    private void UpdateToolbar(ToolbarType toolbarType) {
        // Continue if update concerns this inventory
        if (this.toolbarConfig.GetToolbarType().Equals(toolbarType)) {
            this.RefreshItems(toolbarType);
        }
    }

    private void RefreshItems(ToolbarType toolbarType) {
        if (this.toolbarConfig.GetToolbarType().Equals(toolbarType)) {
            this.items = ToolbarManager.instance.GetToolbarItems(toolbarType);
            this.RefreshUI();
        }
    }
}
