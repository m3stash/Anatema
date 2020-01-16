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
    public static event DragEvent OnItemDragStartEvent;
    public static event DragEvent OnItemDragEndEvent;

    private static Canvas canvas;
        
    private void Awake() {
        this.quantityText = GetComponentInChildren<TextMeshProUGUI>();
        this.iconImage = GetComponentInChildren<Image>();

        // Create a dedicated canvas to drag&drop to avoid layer conflict during drag&drop
        if(!canvas) {
            GameObject canvasObj = new GameObject("Drag And Drop Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 100;
        }
    }

    private void OnDisable() {
        this.ResetConditions();
    }

    public void Setup(InventoryItemData item, InventoryCell cell) {
        this.cell = cell;
        this.item = item;
        this.RefreshUI();
    }

    public InventoryItemData GetItem() {
        return this.item;
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
        OnItemDragEndEvent?.Invoke(this);

        draggedItem = null;
        sourceCell = null;
    }

    /// <summary>
	/// This item started to drag.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnBeginDrag(PointerEventData eventData) {
        sourceCell = cell; // Set source cell for all instances
        draggedItem = this; // Set current dragged Item fo all instances

        // Create dragged object visible which follow the mouse
        draggedObject = new GameObject();
        draggedObject.transform.SetParent(canvas.transform);
        draggedObject.name = "Dragged Icon";

        // Disable image raycast of child item to avoid drag conflict
        Image myImage = GetComponentInChildren<Image>();
        myImage.raycastTarget = false;

        // Modify draggedItem image to assign correct item icon
        Image draggedObjectImage = draggedObject.AddComponent<Image>();
        draggedObjectImage.raycastTarget = false;
        draggedObjectImage.sprite = myImage.sprite;
        draggedObjectImage.color = myImage.color;


        // Set icon's dimensions
        RectTransform draggedObjectRect = draggedObject.GetComponent<RectTransform>();
        RectTransform myRect = GetComponent<RectTransform>();
        draggedObjectRect.pivot = new Vector2(0.5f, 0.5f);
        draggedObjectRect.anchorMin = new Vector2(0.5f, 0.5f);
        draggedObjectRect.anchorMax = new Vector2(0.5f, 0.5f);
        draggedObjectRect.sizeDelta = new Vector2(myRect.rect.width, myRect.rect.height);

        // Notify all items about drag start for raycast disabling
        OnItemDragStartEvent?.Invoke(this);
    }

    /// <summary>
	/// Follow mouse position every frame
	/// </summary>
	/// <param name="data"></param>
	public void OnDrag(PointerEventData data) {
        if(draggedObject) {
            draggedObject.transform.position = InputManager.mousePosition;
        }
    }

    /// <summary>
    /// This item is dropped.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData) {
        // Check if item is dropped outside of a slot to drop it in the world
        if(!eventData.pointerCurrentRaycast.gameObject?.GetComponentInParent<InventoryCell>()) {
            sourceCell.DropItem();
        }

        ResetConditions();
    }
}

