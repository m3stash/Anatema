using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBag : InventoryUI
{
    [Header("Fields to complete manually")]
    [SerializeField] private ItemType itemType;

    public delegate void SwapItemIdx(int sourceIdx, int targetIdx, ItemType itemType);
    public static event SwapItemIdx OnSwapItems;

    private void OnEnable()
    {
        if (InventoryManager.instance)
        {
            this.RefreshItems(this.itemType);
        }

        InventoryManager.itemDatabaseChanged += this.RefreshItems;
    }

    private void OnDisable()
    {
        InventoryManager.itemDatabaseChanged -= this.RefreshItems;
    }

    public override void SwapCells(InventoryCell source, InventoryCell target)
    {
        OnSwapItems(this.GetCellIdx(source), this.GetCellIdx(target), this.itemType);
    }

    private void RefreshItems(ItemType type)
    {
        // Continue if update concerns this inventory
        if (this.itemType.Equals(type))
        {
            this.items = InventoryManager.instance.GetInventoryItems(this.itemType);
            this.RefreshUI();
        }
    }
}
