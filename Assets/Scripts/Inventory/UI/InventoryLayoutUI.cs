using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InventoryLayoutUI : MonoBehaviour {

    [SerializeField] private InventoryStepperUI[] steppers;
    [SerializeField] private InventoryBagUI inventoryBag;
    [SerializeField] private InventoryToolbarUI toolbar;

    private int currentSelectedStepperIdx;

    private void Awake() {
        this.steppers = GetComponentsInChildren<InventoryStepperUI>();
        this.inventoryBag = GetComponentInChildren<InventoryBagUI>();
        this.toolbar = GetComponentInChildren<InventoryToolbarUI>();
    }

    private void OnEnable() {
        InventoryStepperUI.OnSelect += SelectedStepperChanged;
        InputManager.gameplayControls.Inventory.StepperChanged.performed += StepperInputTriggered;

        // Select last opened stepper
        this.SelectStepper(this.currentSelectedStepperIdx);
    }

    private void OnDisable() {
        InventoryStepperUI.OnSelect -= SelectedStepperChanged;
        InputManager.gameplayControls.Inventory.StepperChanged.performed -= StepperInputTriggered;
    }

    private void SelectStepper(int idx) {
        this.steppers[idx].Select();
    }

    private void StepperInputTriggered(InputAction.CallbackContext ctx) {
        int stepperIdx = this.currentSelectedStepperIdx;

        if(ctx.ReadValue<float>() > 0) {
            stepperIdx = (this.currentSelectedStepperIdx < this.steppers.Length - 1) ? this.currentSelectedStepperIdx + 1 : 0;
        } else {
            stepperIdx = (this.currentSelectedStepperIdx > 0) ? this.currentSelectedStepperIdx - 1 : this.steppers.Length - 1;
        }

        this.SelectStepper(stepperIdx);

    }

    private void SelectedStepperChanged(StepperUI stepperUI) {
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

        this.inventoryBag.SelectCell(0);
    }
}
