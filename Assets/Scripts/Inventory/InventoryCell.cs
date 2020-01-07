using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryCell : MonoBehaviour, IDropHandler, IPointerClickHandler {

    [Header("Fields to complete manually")]
    [SerializeField] private GameObject slotItemPrefab;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite selectedSprite;

    [Header("Don't touch it")]
    [SerializeField] private InventoryItem inventoryItem;
    [SerializeField] private InventoryUI associatedInventoryUI;
    [SerializeField] private bool selected;

    private new Image renderer;

    public delegate void OnClick(InventoryCell cell);
    public static event OnClick NotifyClickEvent;

    private void Awake() {
        this.renderer = GetComponent<Image>();
        this.associatedInventoryUI = GetComponentInParent<InventoryUI>();
    }

    void OnEnable() {
        this.RefreshUI();

        InventoryItem.OnItemDragStartEvent += OnAnyItemDragStart;         // Handle any item drag start
        InventoryItem.OnItemDragEndEvent += OnAnyItemDragEnd;             // Handle any item drag end
    }

    void OnDisable() {
        InventoryItem.OnItemDragStartEvent -= OnAnyItemDragStart;
        InventoryItem.OnItemDragEndEvent -= OnAnyItemDragEnd;
    }

    /// <summary>
    /// On any item drag start need to disable all items raycast for correct drop operation
    /// </summary>
    /// <param name="item"> dragged item </param>
    private void OnAnyItemDragStart(InventoryItem item) {
        if(this.inventoryItem) {
            this.inventoryItem.MakeRaycast(false);
        }
    }

    /// <summary>
    /// On any item drag end enable all items raycast
    /// </summary>
    /// <param name="item"> dragged item </param>
    private void OnAnyItemDragEnd(InventoryItem item) {
        if(this.inventoryItem) {
            this.inventoryItem.MakeRaycast(true);
        }
    }

    private void NotifyClick() {
        // Notify if this cell contains an item
        if(this.inventoryItem) {
            NotifyClickEvent?.Invoke(this);
        }
    }

    /// <summary>
    /// Refresh cell renderer
    /// </summary>
    private void RefreshUI() {
        if(!this.renderer) {
            this.renderer = GetComponent<Image>();
        }

        this.renderer.sprite = this.selected ? this.selectedSprite : this.defaultSprite;
    }

    /// <summary>
    /// Notify when user click on this cell
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) {
        NotifyClickEvent?.Invoke(this);
    }

    /// <summary>
    /// Item is dropped in this cell
    /// </summary>
    /// <param name="data"></param>
    public void OnDrop(PointerEventData data) {
        // Do something if an item is currently dragged
        if(InventoryItem.draggedObject) {
            InventoryItem item = InventoryItem.draggedItem;
            InventoryCell sourceCell = InventoryItem.sourceCell;

            if(InventoryItem.draggedObject.activeSelf && item && sourceCell != this) {
                // TODO check cell type
                SwapItems(sourceCell, this);
            }
        }
    }

    /// <summary>
    /// Swap items between two cells
    /// </summary>
    /// <param name="firstCell"> Cell </param>
    /// <param name="secondCell"> Cell </param>
    public void SwapItems(InventoryCell firstCell, InventoryCell secondCell) {
        InventoryItem firstItem = firstCell.GetInventoryItem();                // Get item from first cell
        InventoryItem secondItem = secondCell.GetInventoryItem();              // Get item from second cell
                                                                               // Swap items
        if(firstItem) {
            firstItem.MakeRaycast(true);
        }

        if(secondItem) {
            secondItem.MakeRaycast(true);
        }

        this.associatedInventoryUI.SwapCells(firstCell, secondCell);
    }

    public void DropItem() {
        this.associatedInventoryUI.DropItem(this);
    }

    public void Select() {
        this.selected = true;
        this.RefreshUI();
    }

    public void UnSelect() {
        this.selected = false;
        this.RefreshUI();
    }


    public void UpdateItem(InventoryItemData item) {
        if(item != null && item.GetConfig() != null) {
            if(!inventoryItem) {
                GameObject obj = Instantiate(this.slotItemPrefab, this.transform);
                this.inventoryItem = obj.GetComponent<InventoryItem>();
            }

            inventoryItem.Setup(item, this);
        } else if(((item != null && item.GetConfig() == null) || item == null) && inventoryItem) {
            Destroy(this.inventoryItem.gameObject);
        }
    }

    public InventoryItem GetInventoryItem() {
        return this.inventoryItem;
    }
}
