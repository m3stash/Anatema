using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Fields to complete manually")]
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private Transform inventorySlotContainer;
    [SerializeField] private GameObject slotItemPrefab;

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
      
    public bool AddItem(Item item)
    {
        // If item is stackable try to find if we can stack it
        if (item.GetConfig().IsStackable())
        {
            InventoryItem inventoryItem = this.GetExistingInventoryItem(item);

            // If same item found in inventory and can be stacked so stack it
            if (inventoryItem)
            {
                inventoryItem.Stack(item.GetStacks());
                return true;
            }
        }

        // If item isn't stackable or it couldn't be stacked so try to add it
        InventoryCell cell = this.GetEmptyCell();

        if (cell)
        {
            // Create new inventory item in this cell and setup it
            GameObject obj = Instantiate(this.slotItemPrefab, cell.transform);
            InventoryItem inventoryItem = obj.GetComponent<InventoryItem>();

            inventoryItem.Setup(item);

            this.items.Add(inventoryItem);

            // Refresh cell to update inventory item reference
            cell.Refresh();

            return true;
        }


        Debug.Log("No cell found to add item");
        return false;
    }

    private InventoryCell GetEmptyCell()
    {
        foreach (InventoryCell cell in this.cells)
        {
            // If a cell haven't inventory item reference, it's free cell
            if (!cell.GetInventoryItem())
            {
                return cell;
            }
        }

        return null;
    }

    private InventoryItem GetExistingInventoryItem(Item item)
    {
        foreach (InventoryItem inventoryItem in this.items)
        {
            bool itemFoundById = item.GetConfig().GetId().Equals(inventoryItem.GetItem().GetConfig().GetId());

            // Check if its same item and addition of stacks is less than stackLimit
            if (itemFoundById && inventoryItem.GetItem().CanStack(item.GetStacks()))
            {
                return inventoryItem;
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
