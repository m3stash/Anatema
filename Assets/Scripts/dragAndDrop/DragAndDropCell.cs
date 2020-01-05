using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Every item's cell must contain this script
/// </summary>
public class DragAndDropCell : MonoBehaviour, IDropHandler
{
    private DragAndDropItem associatedItem;
    private 

    void OnEnable()
    {
        DragAndDropItem.OnItemDragStartEvent += OnAnyItemDragStart;         // Handle any item drag start
        DragAndDropItem.OnItemDragEndEvent += OnAnyItemDragEnd;             // Handle any item drag end
        UpdateMyItem();
    }

    void OnDisable()
    {
        DragAndDropItem.OnItemDragStartEvent -= OnAnyItemDragStart;
        DragAndDropItem.OnItemDragEndEvent -= OnAnyItemDragEnd;
    }

    /// <summary>
    /// On any item drag start need to disable all items raycast for correct drop operation
    /// </summary>
    /// <param name="item"> dragged item </param>
    private void OnAnyItemDragStart(DragAndDropItem item)
    {
        UpdateMyItem();

        if (associatedItem)
        {
            associatedItem.MakeRaycast(false);                                   // Disable item's raycast for correct drop handling
        }
    }

    /// <summary>
    /// On any item drag end enable all items raycast
    /// </summary>
    /// <param name="item"> dragged item </param>
    private void OnAnyItemDragEnd(DragAndDropItem item)
    {
        UpdateMyItem();

        if (associatedItem)
        {
            associatedItem.MakeRaycast(true);
        }
    }

    /// <summary>
    /// Item is dropped in this cell
    /// </summary>
    /// <param name="data"></param>
    public void OnDrop(PointerEventData data)
    {
        if (DragAndDropItem.icon != null)
        {
            DragAndDropItem item = DragAndDropItem.draggedItem;
            DragAndDropCell sourceCell = DragAndDropItem.sourceCell;
            if (DragAndDropItem.icon.activeSelf == true)                    // If icon inactive do not need to drop item into cell
            {
                if (item && sourceCell != this)
                {
                    UpdateMyItem();
                    SwapItems(sourceCell, this);
                }
            }
            if (item != null)
            {
                if (item.GetComponentInParent<DragAndDropCell>() == null)   // If item have no cell after drop
                {
                    Destroy(item.gameObject);                               // Destroy it
                }
            }
            UpdateMyItem();
            sourceCell.UpdateMyItem();

            // Refresh sourceCell and currentCell
            sourceCell.GetComponent<InventoryCell>().RefreshItemReference();
            GetComponent<InventoryCell>().RefreshItemReference();
        }
    }

    /// <summary>
    /// Updates my item
    /// </summary>
    public void UpdateMyItem()
    {
        associatedItem = GetComponentInChildren<DragAndDropItem>();
    }

    /// <summary>
    /// Get item from this cell
    /// </summary>
    /// <returns> Item </returns>
    public DragAndDropItem GetItem()
    {
        return associatedItem;
    }

    /// <summary>
    /// Swap items between two cells
    /// </summary>
    /// <param name="firstCell"> Cell </param>
    /// <param name="secondCell"> Cell </param>
    public void SwapItems(DragAndDropCell firstCell, DragAndDropCell secondCell)
    {
        if (firstCell && secondCell)
        {
            DragAndDropItem firstItem = firstCell.GetItem();                // Get item from first cell
            DragAndDropItem secondItem = secondCell.GetItem();              // Get item from second cell
                                                                            // Swap items
            if (firstItem)
            {
                firstItem.MakeRaycast(true);
            }
            if (secondItem)
            {
                secondItem.MakeRaycast(true);
            }

            InventoryBag inventory = GetComponentInParent<InventoryBag>();
            inventory.SwapCells(firstCell.GetComponent<InventoryCell>(), secondCell.GetComponent<InventoryCell>());

        }
    }
}
