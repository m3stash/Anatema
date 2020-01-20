using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // Select last opened stepper
        this.SelectStepper(this.currentSelectedStepperIdx);
    }

    private void OnDisable() {
        InventoryStepperUI.OnSelect -= SelectedStepperChanged;
    }

    private void SelectStepper(int idx) {
        this.steppers[idx].Select();
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
    }
}
