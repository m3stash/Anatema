using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Fields to complete manually")]
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private Transform inventorySlotContainer;
    [SerializeField] private Transform toolbarSlotContainer;

    [Header("Inventory slots")]
    [SerializeField] private InventoryItemData[] itemDatas;
    private int inventorySize = 16;
    private int toolbarSize = 8;

    private InventoryCell[] cells;

    public static InventoryManager instance;

    void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            this.itemDatas = new InventoryItemData[this.inventorySize + this.toolbarSize];
        }
    }

    private void Start()
    {
        this.UpdateCells();

        // Subscribe to click event on inventory cell
        InventoryCell.NotifyClickEvent += this.CellClicked;
        InventoryCell.NotifyItemChanged += this.ItemChangedInCell;
    }

    private void OnDestroy()
    {
        InventoryCell.NotifyClickEvent -= this.CellClicked;
        InventoryCell.NotifyItemChanged -= this.ItemChangedInCell;
    }

    public void SwitchDisplay()
    {
        this.inventoryCanvas.SetActive(!this.inventoryCanvas.activeSelf);
    }

    private void RefreshUI() {
        for(int i = 0; i < this.cells.Length; i++) {
            this.cells[i].UpdateItem(this.itemDatas[i]);
        }
    }

    private void UpdateCells() {
        this.cells = new InventoryCell[this.inventorySize + this.toolbarSize];

        for (int i = 0; i < this.toolbarSlotContainer.childCount; i++) {
            this.cells[i] = this.toolbarSlotContainer.GetChild(i).GetComponent<InventoryCell>();
        }

        for (int i = 0; i < this.inventorySlotContainer.childCount; i++) {
            this.cells[i + this.toolbarSize] = this.inventorySlotContainer.GetChild(i).GetComponent<InventoryCell>();
        }
    }

    public bool AddItem(Item item)
    {
        // If item is stackable try to find if we can stack it
        if (item.GetConfig().IsStackable())
        {
            InventoryItemData inventoryItemData = this.GetExistingInventoryItem(item);

            // If same item found in inventory and can be stacked so stack it
            if (inventoryItemData != null)
            {
                inventoryItemData.AddStacks(item.GetStacks());
                this.RefreshUI();
                return true;
            }
        }

        // If item isn't stackable or it couldn't be stacked so try to add it
        int freeSlotIdx = this.GetEmptySlotIdx();

        if (freeSlotIdx != -1)
        {
            // Create new inventory item in this cell and setup it
            this.itemDatas[freeSlotIdx] = new InventoryItemData(item.GetConfig(), item.GetStacks(), 100);
            this.RefreshUI();
            return true;
        }


        Debug.Log("No slot found to add item");
        return false;
    }


    private int GetEmptySlotIdx()
    {
        for (int i = 0; i < this.itemDatas.Length; i++)
        {
            if ((this.itemDatas[i] != null && this.itemDatas[i].GetConfig() == null) || this.itemDatas[i] == null)
            {
                return i;
            }
        }

        return -1;
    }

    private InventoryItemData GetExistingInventoryItem(Item item)
    {
        for(int i = 0; i < this.itemDatas.Length; i++)
        {
            if(this.itemDatas[i] != null && this.itemDatas[i].GetConfig() != null)
            {
                bool itemFoundById = item.GetConfig().GetId().Equals(this.itemDatas[i].GetConfig().GetId());

                // Check if its same item and addition of stacks is less than stackLimit
                if (itemFoundById && this.itemDatas[i].CanStack(item.GetStacks()))
                {
                    return this.itemDatas[i];
                }
            }
        }

        return null;
    }

    private int GetCellIdx(InventoryCell cell) {
        for(int i = 0; i < this.cells.Length; i++) {
            if(this.cells[i] == cell) {
                return i;
            }
        }

        return -1;
    }

    private void CellClicked(InventoryCell cell)
    {
        // Maybe display an action menu on this cell ?
    }

    private void ItemChangedInCell(InventoryCell cell) {
        int cellIdx = this.GetCellIdx(cell);

        if(cellIdx != -1) {
            this.itemDatas[cellIdx] = cell.GetInventoryItem() ? cell.GetInventoryItem().GetItem() : null;
        } else {
            Debug.LogError("Cell idx not found");
        }
    }
}
