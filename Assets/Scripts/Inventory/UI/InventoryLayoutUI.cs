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
        this.steppers[this.currentSelectedStepperIdx].Select();

        InventoryStepperUI.OnSelect += UpdateCurrentStepperIdx;
    }

    private void OnDisable() {
        InventoryStepperUI.OnSelect -= UpdateCurrentStepperIdx;
    }

    private void UpdateCurrentStepperIdx(StepperUI stepperUI) {
        for(int i = 0; i < this.steppers.Length; i++) {
            if(this.steppers[i] == stepperUI) {
                this.currentSelectedStepperIdx = i;
            }
        }
    }
}
