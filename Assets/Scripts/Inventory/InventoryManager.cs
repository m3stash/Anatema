using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Fields to complete manually")]
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private Transform inventorySlotContainer;
    [SerializeField] private GameObject slotItemPrefab;

    [Header("Inventory slots")]
    [SerializeField] private InventoryItemData[] itemDatas;
    private int inventorySize = 16;

    private List<InventoryItem> items;
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
        }
    }

    private void Start()
    {
        this.itemDatas = new InventoryItemData[this.inventorySize];
        this.items = new List<InventoryItem>();
        this.cells = this.inventorySlotContainer.GetComponentsInChildren<InventoryCell>();

        // Subscribe to click event on inventory cell
        InventoryCell.NotifyClickEvent += this.CellClicked;
    }

    private void OnDestroy()
    {
        InventoryCell.NotifyClickEvent -= this.CellClicked;
    }

    public void SwitchDisplay()
    {
        this.inventoryCanvas.SetActive(!this.inventoryCanvas.activeSelf);
    }

    //public bool AddItem(Item item)
    //{
    //    // If item is stackable try to find if we can stack it
    //    if (item.GetConfig().IsStackable())
    //    {
    //        InventoryItem inventoryItem = this.GetExistingInventoryItem(item);

    //        // If same item found in inventory and can be stacked so stack it
    //        if (inventoryItem)
    //        {
    //            inventoryItem.Stack(item.GetStacks());
    //            return true;
    //        }
    //    }

    //    // If item isn't stackable or it couldn't be stacked so try to add it
    //    InventoryCell cell = this.GetEmptyCell();

    //    if (cell)
    //    {
    //        // Create new inventory item in this cell and setup it
    //        GameObject obj = Instantiate(this.slotItemPrefab, cell.transform);
    //        InventoryItem inventoryItem = obj.GetComponent<InventoryItem>();

    //        inventoryItem.Setup(item);

    //        this.items.Add(inventoryItem);

    //        // Refresh cell to update inventory item reference
    //        cell.Refresh();

    //        return true;
    //    }


    //    Debug.Log("No cell found to add item");
    //    return false;
    //}

    //private InventoryCell GetEmptyCell()
    //{
    //    foreach (InventoryCell cell in this.cells)
    //    {
    //        // If a cell haven't inventory item reference, it's free cell
    //        if (!cell.GetInventoryItem())
    //        {
    //            return cell;
    //        }
    //    }

    //    return null;
    //}

    //private InventoryItem GetExistingInventoryItem(Item item)
    //{
    //    foreach (InventoryItem inventoryItem in this.items)
    //    {
    //        bool itemFoundById = item.GetConfig().GetId().Equals(inventoryItem.GetItem().GetConfig().GetId());

    //        // Check if its same item and addition of stacks is less than stackLimit
    //        if (itemFoundById && inventoryItem.GetItem().CanStack(item.GetStacks()))
    //        {
    //            return inventoryItem;
    //        }
    //    }

    //    return null;
    //}

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
                return true;
            }
        }

        // If item isn't stackable or it couldn't be stacked so try to add it
        int freeSlotIdx = this.GetEmptySlotIdx();

        if (freeSlotIdx != -1)
        {
            // Create new inventory item in this cell and setup it
            this.itemDatas[freeSlotIdx] = new InventoryItemData(item.GetConfig(), item.GetStacks(), 100);

            return true;
        }


        Debug.Log("No slot found to add item");
        return false;
    }


    private int GetEmptySlotIdx()
    {
        for (int i = 0; i < this.itemDatas.Length; i++)
        {
            if (this.itemDatas[i].GetConfig() == null)
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
            if(this.itemDatas[i].GetConfig() != null)
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

    private void CellClicked(InventoryCell cell)
    {
        Debug.Log("Cell cliked");
        // Maybe display an action menu on this cell ?
    }
}
