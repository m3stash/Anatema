using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;

public class InventoryLayoutUI : MonoBehaviour {

    [Header("Don't touch it")]
    [SerializeField] private InventoryStepperUI[] steppers;
    [SerializeField] private InventoryBagUI inventoryBag;
    [SerializeField] private InventoryToolbarUI toolbar;
    [SerializeField] private InventoryCell currentSelectedCell;

    private int currentSelectedStepperIdx;

    private void Awake() {
        this.steppers = GetComponentsInChildren<InventoryStepperUI>();
        this.inventoryBag = GetComponentInChildren<InventoryBagUI>();
        this.toolbar = GetComponentInChildren<InventoryToolbarUI>();
    }

    private void OnEnable() {
        InventoryStepperUI.OnSelect += SelectedStepperChanged;
        InputManager.gameplayControls.Inventory.ChangeStepper.performed += ChangeStepperAction;
        InputManager.gameplayControls.Inventory.DropItem.performed += DropItemAction;
        InputManager.gameplayControls.Inventory.DeleteItem.performed += DeleteItemAction;
        InputManager.gameplayControls.Inventory.Navigate.performed += NavigateAction;
        InputManager.gameplayControls.Inventory.Interact.performed += InteractAction;
        InputManager.gameplayControls.Inventory.Cancel.performed += CancelAction;

        // Select last opened stepper
        this.SelectStepper(this.currentSelectedStepperIdx);
    }

    private void OnDisable() {
        InventoryStepperUI.OnSelect -= SelectedStepperChanged;
        InputManager.gameplayControls.Inventory.ChangeStepper.performed -= ChangeStepperAction;
        InputManager.gameplayControls.Inventory.DropItem.performed -= DropItemAction;
        InputManager.gameplayControls.Inventory.DeleteItem.performed -= DeleteItemAction;
        InputManager.gameplayControls.Inventory.Navigate.performed -= NavigateAction;
        InputManager.gameplayControls.Inventory.Interact.performed -= InteractAction;
        InputManager.gameplayControls.Inventory.Cancel.performed -= CancelAction;
    }

    private void SelectStepper(int idx) {
        this.steppers[idx].Select();
    }

    /// <summary>
    /// Used to force drag ending (Used for XBOX controller)
    /// </summary>
    private void StopItemDrag() {
        if(InventoryItem.draggedItem) {
            InventoryItem.draggedItem.EndDrag();
        }
    }

    /// <summary>
    /// Used to move dragged item to the current cell position
    /// </summary>
    private void SetItemDragPositionToCurrentCell() {
        InventoryItem.draggedItem.SetDraggedItemPosition(this.currentSelectedCell.GetRectTransform().position);
    }

    /// <summary>
    /// Set current cell and move cursor to it (XBOX controller)
    /// Manage eraser cell animations for XBOX controller
    /// </summary>
    /// <param name="cell"></param>
    private void SetCurrentSelectedCell(InventoryCell cell) {
        // If current cell is eraser cell so change opened state
        if(this.currentSelectedCell != null && this.currentSelectedCell.GetCellType().Equals(CellType.DELETE)) {
            this.currentSelectedCell.GetComponent<EraserCell>().SetOpenedState(false);
        }

        this.currentSelectedCell = cell;

        // If current cell is eraser cell and player is currently drag so changed opened state
        if(InventoryItem.draggedItem && this.currentSelectedCell.GetCellType().Equals(CellType.DELETE)) {
            this.currentSelectedCell.GetComponent<EraserCell>().SetOpenedState(true);
        }

        CursorManager.instance.SetPosition(this.currentSelectedCell.GetRectTransform().position);
    }

    #region Callbacks

    /// <summary>
    /// Action callback when navigation triggered
    /// Find neighbour cell from current selected cell to navigate into it
    /// If currently dragging, move dragged item to the new current cell
    /// </summary>
    /// <param name="ctx"></param>
    private void NavigateAction(InputAction.CallbackContext ctx) {
        Vector2 direction = ctx.ReadValue<Vector2>();

        if(direction != Vector2.zero) {
            InventoryCell neighbourCell = this.currentSelectedCell.GetNeighbourCell(new Vector3(direction.x, direction.y, 0));

            if(neighbourCell) {
                this.SetCurrentSelectedCell(neighbourCell);

                if(InventoryItem.draggedItem) {
                    this.SetItemDragPositionToCurrentCell();
                }
            }
        }
    }

    /// <summary>
    /// Action callback when interact button triggered
    /// Used to manage drag&drop system for controllers input such as XBOX / SWITCH / PS4
    /// </summary>
    /// <param name="ctx"></param>
    private void InteractAction(InputAction.CallbackContext ctx) {
        // If not dragging and cell have an item. Start drag and set dragged item position to current cell
        if(!InventoryItem.draggedItem && this.currentSelectedCell && this.currentSelectedCell.GetInventoryItem()) {
            this.currentSelectedCell.GetInventoryItem().StartDrag();
            this.SetItemDragPositionToCurrentCell();
        } else if(InventoryItem.draggedItem) { // If dragging. Drop item in current cell and stop item drag
            this.currentSelectedCell.Drop();
            this.StopItemDrag();
        }
    }


    /// <summary>
    /// Action callback when cancel button triggered
    /// Used to stop dragging
    /// </summary>
    /// <param name="ctx"></param>
    private void CancelAction(InputAction.CallbackContext ctx) {
        this.StopItemDrag();
    }

    /// <summary>
    /// Action callback when drop button triggered
    /// Used to drop item in the world
    /// </summary>
    /// <param name="ctx"></param>
    private void DropItemAction(InputAction.CallbackContext ctx) {
        currentSelectedCell.DropItem();
    }


    /// <summary>
    /// Action callback when delete button triggered
    /// Used to delete item
    /// </summary>
    /// <param name="ctx"></param>
    private void DeleteItemAction(InputAction.CallbackContext ctx) {
        currentSelectedCell.DeleteItem();
    }


    /// <summary>
    /// Action callback when stepper change button triggered (LB/RB for controllers)
    /// Used to select next/previous stepper in function of input value
    /// </summary>
    /// <param name="ctx"></param>
    private void ChangeStepperAction(InputAction.CallbackContext ctx) {
        int stepperIdx;

        if(ctx.ReadValue<float>() > 0) {
            stepperIdx = (this.currentSelectedStepperIdx < this.steppers.Length - 1) ? this.currentSelectedStepperIdx + 1 : 0;
        } else {
            stepperIdx = (this.currentSelectedStepperIdx > 0) ? this.currentSelectedStepperIdx - 1 : this.steppers.Length - 1;
        }

        this.SelectStepper(stepperIdx);

    }

    /// <summary>
    /// Callback when stepper has been selected by mouse click
    /// Used to unselect other steppers and update inventory bag + toolbar item type
    /// </summary>
    /// <param name="stepperUI"></param>
    private void SelectedStepperChanged(StepperUI stepperUI) {
        this.StopItemDrag();

        for(int i = 0; i < this.steppers.Length; i++) {
            if(this.steppers[i] == stepperUI) {
                this.currentSelectedStepperIdx = i;

                // Notify inventory bag / toolbar to change its type
                InventoryStepperUI inventoryStepperUI = (InventoryStepperUI)stepperUI;
                this.inventoryBag.ChangeInventoryType(inventoryStepperUI.GetItemType());
                this.toolbar.ChangeToolbarType(inventoryStepperUI.GetItemType());
            } else {
                this.steppers[i].UnSelect();
            }
        }

        this.SetCurrentSelectedCell(this.inventoryBag.GetInventoryCell(0));
    }

    #endregion
}
