using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Fields to complete manually")]
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private GameObject slotContainerPanel;

    private InventoryItem[] slots;

    public static InventoryManager instance;

    // Start is called before the first frame update
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

        this.slots = this.slotContainerPanel.GetComponentsInChildren<InventoryItem>();
    }

    public void SwitchDisplay()
    {
        this.inventoryCanvas.SetActive(!this.inventoryCanvas.activeSelf);
    }

    public bool AddItem(Item item)
    {
        InventoryItem slot = null;

        if (item.GetConfig().IsStackable())
        {
            slot = this.GetSlotWithItem(item);

            if (slot)
            {
                slot.Stack(item.GetStacks());
                return true;
            }
        }

        slot = this.GetEmptySlot();

        if (slot)
        {
            slot.Setup(item);
            return true;
        }


        Debug.Log("No slot found to add item");
        return false;
    }

    public void RemoveItem(Item item)
    {
        // TODO
    }

    private InventoryItem GetEmptySlot()
    {
        foreach (InventoryItem slot in this.slots)
        {
            if (!slot.GetAssociatedItem())
            {
                return slot;
            }
        }

        return null;
    }

    private InventoryItem GetSlotWithItem(Item item)
    {
        foreach (InventoryItem slot in this.slots)
        {
            // Check if its same item and addition of stacks is less than stackLimit
            if (slot.GetAssociatedItem() && item.GetConfig().GetId().Equals(slot.GetAssociatedItem().GetConfig().GetId()) && (slot.GetAssociatedItem().GetStacks() + item.GetStacks() <= item.GetConfig().GetStackLimit()))
            {
                return slot;
            }
        }

        return null;
    }
}
