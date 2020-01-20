using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryToolbarUI : InventoryUI
{
    [Header("Fields to complete manually")]
    [SerializeField] private ToolbarConfig toolbarConfig;

    private void OnEnable() {
        ToolbarManager.OnToolbarChanged += UpdateToolbar;
    }

    private void OnDisable() {
        ToolbarManager.OnToolbarChanged -= UpdateToolbar;
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
        ToolbarManager.instance.DropItem(this.GetCellIdx(cell), this.toolbarConfig.GetToolbarType());
    }

    public override void DeleteItem(InventoryCell cell) {
        ToolbarManager.instance.DeleteItem(this.GetCellIdx(cell), this.toolbarConfig.GetToolbarType());
    }

    public override void ReplaceItem(InventoryItemData inventoryItem, InventoryCell target) {
        ToolbarManager.instance.ReplaceItem(inventoryItem, this.GetCellIdx(target), this.toolbarConfig.GetToolbarType());
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
