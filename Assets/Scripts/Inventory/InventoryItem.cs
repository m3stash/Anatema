using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [Header("Fields below are filled in runtime")]
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Image iconImage;
    [SerializeField] private InventoryCell cell;
    [SerializeField] private InventoryItemData item;

    public static InventoryItem draggedItem; // Item that is dragged now
    public static GameObject draggedObject; // Object used to show drag
    public static InventoryCell sourceCell;

    public delegate void DragEvent(InventoryItem item);
    public static event DragEvent OnItemDragStart;
    public static event DragEvent OnItemDragEnd;

    private void Awake() {
        this.quantityText = GetComponentInChildren<TextMeshProUGUI>();
        this.iconImage = GetComponentInChildren<Image>();
    }

    private void OnDisable() {
        if (!cell.GetAssociatedInventory().IsDisableDragDrop()) {
            this.ResetConditions();
        }
    }

    public void Setup(InventoryItemData item, InventoryCell cell) {
        this.cell = cell;
        this.item = item;
        this.RefreshUI();
    }

    public InventoryItemData GetItem() {
        return this.item;
    }

    public InventoryCell GetAssociatedCell() {
        return this.cell;
    }

    public bool IsSameThan(InventoryItem item) {
        return this.item.GetConfig().GetItemType() == item.GetItem().GetConfig().GetItemType();
    }

    /// <summary>
	/// Enable item's raycast.
	/// </summary>
	/// <param name="state"> true - enable, false - disable </param>
	public void MakeRaycast(bool state) {
        this.iconImage.raycastTarget = state;
    }

    /// <summary>
    /// Used to refresh item UI
    /// </summary>
    private void RefreshUI() {
        this.quantityText.text = this.item.GetStacks().ToString();
        this.iconImage.color = new Color32(255, 255, 255, 255);
        this.iconImage.sprite = this.item.GetConfig().GetIcon();
    }

    /// <summary>
    /// Resets all temporary conditions.
    /// </summary>
    private void ResetConditions() {
        // Destroy item object created for drag operation
        if(draggedObject) {
            Destroy(draggedObject);
        }

        // Notify all cells about item drag end
        OnItemDragEnd?.Invoke(this);

        draggedItem = null;
        sourceCell = null;

        // Change cursor state
        CursorManager.instance.SetCursorState(CursorState.INVENTORY_NAVIGATION);
    }

    /// <summary>
	/// This item started to drag with mouse action.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnBeginDrag(PointerEventData eventData) {
        this.StartDrag();
    }

    /// <summary>
	/// Drag action from mouse
	/// </summary>
	/// <param name="data"></param>
	public void OnDrag(PointerEventData data) {
        this.SetDraggedItemPosition(InputManager.mousePosition);
    }

    /// <summary>
    /// This item is dropped.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData) {
        // Check if item is dropped outside of a slot to drop it in the world
        if(!eventData.pointerCurrentRaycast.gameObject) {
            sourceCell.DropItem();
        }

        this.EndDrag();
    }

    public void StartDrag() {
        if(cell.GetAssociatedInventory().IsDisableDragDrop()) {
            return;
        }

        sourceCell = cell; // Set source cell for all instances
        draggedItem = this; // Set current dragged Item fo all instances

        // Create dragged object visible which follow the mouse
        draggedObject = new GameObject();
        draggedObject.transform.SetParent(this.GetAssociatedCell().GetAssociatedInventory().transform.parent);
        draggedObject.name = "Dragged Item";

        // Disable image raycast of child item to avoid drag conflict
        Image myImage = GetComponentInChildren<Image>();
        myImage.raycastTarget = false;

        // Modify draggedItem image to assign correct item icon
        Image draggedObjectImage = draggedObject.AddComponent<Image>();
        draggedObjectImage.raycastTarget = false;
        draggedObjectImage.sprite = myImage.sprite;
        draggedObjectImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 0.7f);

        // Set icon's dimensions
        RectTransform draggedObjectRect = draggedObject.GetComponent<RectTransform>();
        RectTransform myRect = GetComponent<RectTransform>();
        draggedObjectRect.anchorMin = new Vector2(0.5f, 0.5f);
        draggedObjectRect.anchorMax = new Vector2(.5f, 0.5f);
        draggedObjectRect.pivot = new Vector2(.5f, .5f);
        draggedObjectRect.sizeDelta = new Vector2(60f, 60f);
        draggedObject.transform.localScale = new Vector3(1, 1, 1);

        // Notify all items about drag start for raycast disabling
        OnItemDragStart?.Invoke(this);

        // Change cursor state
        CursorManager.instance.SetCursorState(CursorState.INVENTORY_DRAG);
    }

    public void SetDraggedItemPosition(Vector2 position) {
        if(draggedObject) {
            draggedObject.transform.position = position;
        }
    }

    public void EndDrag() {
        if (cell.GetAssociatedInventory().IsDisableDragDrop()) {
            return;
        }

        ResetConditions();
    }
}

