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
        if (associatedItem != null)
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
        if (associatedItem != null)
        {
            associatedItem.MakeRaycast(true);                                  	// Enable item's raycast
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

                    // If this cell already have an item
                    if (associatedItem)
                    {
                        // Fill event descriptor
                        // TODO check stackable and empty
                        if (true)
                        {
                            SwapItems(sourceCell, this);                // Swap items between cells
                        }
                        else
                        {
                            PlaceItem(item);            // Delete old item and place dropped item into this cell
                        }
                    }
                    else
                    {
                        PlaceItem(item);                // Place dropped item into this empty cell
                    }
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
    /// Put item into this cell.
    /// </summary>
    /// <param name="item">Item.</param>
    private void PlaceItem(DragAndDropItem item)
    {
        DestroyItem();                                              // Remove current item from this cell
        item.transform.SetParent(transform, false);
        item.transform.localPosition = Vector3.zero;
        item.MakeRaycast(true);
        associatedItem = item;
    }

    /// <summary>
    /// Destroy item in this cell
    /// </summary>
    private void DestroyItem()
    {
        UpdateMyItem();

        if (associatedItem)
        {
            Destroy(associatedItem.gameObject);
        }

        associatedItem = null;
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
    /// Manualy add item into this cell
    /// </summary>
    /// <param name="newItem"> New item </param>
    public void AddItem(DragAndDropItem newItem)
    {
        PlaceItem(newItem);
    }

    /// <summary>
    /// Manualy delete item from this cell
    /// </summary>
    public void RemoveItem()
    {
        DestroyItem();
    }

    /// <summary>
    /// Swap items between two cells
    /// </summary>
    /// <param name="firstCell"> Cell </param>
    /// <param name="secondCell"> Cell </param>
    public void SwapItems(DragAndDropCell firstCell, DragAndDropCell secondCell)
    {
        if ((firstCell != null) && (secondCell != null))
        {
            DragAndDropItem firstItem = firstCell.GetItem();                // Get item from first cell
            DragAndDropItem secondItem = secondCell.GetItem();              // Get item from second cell
                                                                            // Swap items
            if (firstItem != null)
            {
                firstItem.transform.SetParent(secondCell.transform, false);
                firstItem.transform.localPosition = Vector3.zero;
                firstItem.MakeRaycast(true);
            }
            if (secondItem != null)
            {
                secondItem.transform.SetParent(firstCell.transform, false);
                secondItem.transform.localPosition = Vector3.zero;
                secondItem.MakeRaycast(true);
            }
            // Update states
            firstCell.UpdateMyItem();
            secondCell.UpdateMyItem();
        }
    }
}
