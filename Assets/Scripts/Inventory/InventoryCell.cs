﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryCell : MonoBehaviour, IDropHandler {

    [Header("Fields to complete manually")]
    [SerializeField] private GameObject slotItemPrefab;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite disabledSprite;
    [SerializeField] private CellType cellType;
    [SerializeField] private ItemType[] allowedItemTypes;

    [Header("Don't touch it")]
    [SerializeField] private InventoryItem inventoryItem;
    [SerializeField] private InventoryUI associatedInventoryUI;
    [SerializeField] private CellState state;

    [SerializeField] private new Image renderer;
    [SerializeField] private Button button;
    [SerializeField] private RectTransform rectTransform;

    protected virtual void Awake() {
        this.renderer = GetComponent<Image>();
        this.button = GetComponent<Button>();
        this.rectTransform = GetComponent<RectTransform>();
        this.associatedInventoryUI = GetComponentInParent<InventoryUI>();
    }

    void OnEnable() {
        this.RefreshUI();

        InventoryItem.OnItemDragStart += OnAnyItemDragStart;
        InventoryItem.OnItemDragEnd += OnAnyItemDragEnd;
    }

    void OnDisable() {
        InventoryItem.OnItemDragStart -= OnAnyItemDragStart;
        InventoryItem.OnItemDragEnd -= OnAnyItemDragEnd;
    }

    public void SetAllowedItemTypes(ItemType[] types) {
        this.allowedItemTypes = types;
    }

    public CellState GetState() {
        return this.state;
    }

    public RectTransform GetRectTransform() {
        if(!this.rectTransform) {
            this.rectTransform = GetComponent<RectTransform>();
        }

        return this.rectTransform;
    }


    /// <summary>
    /// Used to find the next interactable cell in direction
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public InventoryCell GetNeighbourCell(Vector3 dir) {
        Selectable neighbour = null;

        if(dir == new Vector3(1f, 0f)) {
            neighbour = this.button.FindSelectableOnRight();
        } else if(dir == new Vector3(-1f, 0f)) {
            neighbour = this.button.FindSelectableOnLeft();
        } else if(dir == new Vector3(0, 1f)) {
            neighbour = this.button.FindSelectableOnUp();
        } else if(dir == new Vector3(0, -1f)) {
            neighbour = this.button.FindSelectableOnDown();
        }

        return neighbour ? neighbour.GetComponent<InventoryCell>() : null;
    }

    /// <summary>
    /// On any item drag start need to disable all items raycast for correct drop operation
    /// </summary>
    /// <param name="item"> dragged item </param>
    private void OnAnyItemDragStart(InventoryItem item) {
        if(this.inventoryItem) {
            this.inventoryItem.MakeRaycast(false);

            if(item != this.inventoryItem && !this.inventoryItem.IsSameThan(item) && this.associatedInventoryUI != item.GetAssociatedCell().associatedInventoryUI) {
                this.SetState(CellState.DISABLED);
            }
        } else if(this.cellType == CellType.ITEM && !this.IsAllowedItemType(item.GetItem().GetConfig().GetItemType())) {
            this.SetState(CellState.DISABLED);
        }
    }

    /// <summary>
    /// On any item drag end enable all items raycast
    /// </summary>
    /// <param name="item"> dragged item </param>
    protected virtual void OnAnyItemDragEnd(InventoryItem item) {
        if(this.inventoryItem) {
            this.inventoryItem.MakeRaycast(true);
        }

        if(this.state != CellState.HIDDEN && this.cellType == CellType.ITEM) {
            this.SetState(CellState.ENABLED);
        }
    }

    /// <summary>
    /// Refresh cell renderer
    /// </summary>
    protected virtual void RefreshUI() {
        if(!this.renderer) {
            this.renderer = GetComponent<Image>();
        }

        switch(this.state) {
            case CellState.ENABLED:
                this.renderer.color = Color.white;
                this.renderer.sprite = this.defaultSprite;
                break;
            case CellState.SELECTED:
                this.renderer.sprite = this.selectedSprite;
                break;
            case CellState.DISABLED:
                if(this.inventoryItem) {
                    this.renderer.color = Color.red;
                } else {
                    this.renderer.sprite = this.disabledSprite;
                }
                break;
            case CellState.HIDDEN:
                this.renderer.color = new Color(0, 0, 0, 0.2f);
                this.renderer.sprite = this.defaultSprite;
                break;
        }
    }

    public InventoryUI GetAssociatedInventory() {
        return this.associatedInventoryUI;
    }

    /// <summary>
    /// Callback when drop action via mouse
    /// Drop item in this cell
    /// </summary>
    /// <param name="data"></param>
    public void OnDrop(PointerEventData data) {
        this.Drop();
    }

    public void Drop() {
        // Do something if an item is currently dragged
        if(InventoryItem.draggedObject) {
            InventoryItem inventoryItem = InventoryItem.draggedItem;
            InventoryCell sourceCell = InventoryItem.sourceCell;

            if(InventoryItem.draggedObject.activeSelf && this.state != CellState.DISABLED && inventoryItem && sourceCell != this) {
                // Do specific stuff in function of cell type
                switch(this.cellType) {
                    case CellType.ITEM:
                        if(this.IsAllowedItemType(inventoryItem.GetItem().GetConfig().GetItemType())) {
                            SwapItems(sourceCell, this);
                        }
                        break;

                    case CellType.DELETE:
                        sourceCell.DeleteItem();
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Swap items between two cells
    /// </summary>
    /// <param name="firstCell"> Cell </param>
    /// <param name="secondCell"> Cell </param>
    public void SwapItems(InventoryCell firstCell, InventoryCell secondCell) {
        InventoryItem firstInventoryItem = firstCell.GetInventoryItem();
        InventoryItem secondInventoryItem = secondCell.GetInventoryItem();
        InventoryItemData sourceItemData = null;
        InventoryItemData targetItemData = null;

        if(firstInventoryItem) {
            firstInventoryItem.MakeRaycast(true);
            sourceItemData = firstInventoryItem.GetItem();
        }

        if(secondInventoryItem) {
            secondInventoryItem.MakeRaycast(true);
            targetItemData = secondInventoryItem.GetItem();
        }

        if(!secondInventoryItem) { // Add item
            secondCell.ReplaceItem(sourceItemData);
            firstCell.DeleteItem();
        } else if(CanStackItem(firstInventoryItem.GetItem(), secondInventoryItem.GetItem())) { // Fill stacks
            // Add sources stacks
            targetItemData.AddStacks(sourceItemData.GetStacks());

            // Get overflow stacks
            int overflowStacks = targetItemData.GetOverflowStacks();

            // If greater than 0, target item has exceeded its stack limit
            if(overflowStacks > 0) {
                sourceItemData.SetStacks(overflowStacks);
                targetItemData.RemoveStacks(overflowStacks);

                firstCell.ReplaceItem(sourceItemData);
            } else {
                firstCell.DeleteItem();
            }

            secondCell.ReplaceItem(targetItemData);
        } else { // Swap
            secondCell.ReplaceItem(sourceItemData);
            firstCell.ReplaceItem(targetItemData);
        }

    }

    public bool CanStackItem(InventoryItemData source, InventoryItemData target) {
        return source.IsSameThan(target) && target.CanStack();
    }

    public void ReplaceItem(InventoryItemData item) {
        this.associatedInventoryUI.ReplaceItem(item, this);
    }

    public void DropItem() {
        if(this.inventoryItem) {
            this.associatedInventoryUI.DropItem(this);
        }
    }

    public void DeleteItem() {
        if(this.inventoryItem) {
            this.associatedInventoryUI.DeleteItem(this);
        }
    }

    public void SetState(CellState state) {
        this.state = state;
        this.RefreshUI();
    }

    public void UpdateItem(InventoryItemData item) {
        if(item != null && item.GetConfig() != null) {
            if(!this.inventoryItem) {
                GameObject obj = Instantiate(this.slotItemPrefab, this.transform);
                this.inventoryItem = obj.GetComponent<InventoryItem>();
            }

            this.inventoryItem.Setup(item, this);
        } else if(((item != null && item.GetConfig() == null) || item == null) && this.inventoryItem) {
            Destroy(this.inventoryItem.gameObject);
        }
    }

    private bool IsAllowedItemType(ItemType type) {
        foreach(ItemType itemType in this.allowedItemTypes) {
            if(itemType.Equals(type)) {
                return true;
            }
        }
        return false;
    }

    public InventoryItem GetInventoryItem() {
        return this.inventoryItem;
    }

    public CellType GetCellType() {
        return this.cellType;
    }
}

public enum CellType {
    ITEM,
    DELETE
}

public enum CellState {
    ENABLED,
    SELECTED,
    DISABLED,
    HIDDEN
}
