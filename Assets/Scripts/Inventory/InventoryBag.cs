using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBag : MonoBehaviour
{
    [Header("Fields to complete manually")]
    [SerializeField] private ItemType itemType;
    [SerializeField] private Transform slotContainer;

    [Header("Don't touch it")]
    [SerializeField] private InventoryCell[] cells;
    [SerializeField] private InventoryItemData[] items;

    public delegate void SwapItemIdx(int sourceIdx, int targetIdx, ItemType itemType);
    public static event SwapItemIdx OnSwapItems;

    private void Awake()
    {
        this.cells = this.slotContainer.GetComponentsInChildren<InventoryCell>();
    }

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

    public void SwapCells(InventoryCell source, InventoryCell target)
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

    private void RefreshUI()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            this.cells[i].UpdateItem(this.items[i]);
        }
    }

    private int GetCellIdx(InventoryCell cell)
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            if (this.cells[i] == cell)
            {
                return i;
            }
        }

        return -1;
    }
}
