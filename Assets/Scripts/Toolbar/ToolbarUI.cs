using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : InventoryUI
{
    private int currentSelectedIdx;

    public static ToolbarUI instance;

    protected override void Awake() {
        base.Awake();

        if (instance) {
            Destroy(this);
        } else {
            instance = this;
        }
    }

    private void OnEnable() {
        this.SelectCurrentSlot();

        // Temporaly as lons as toolbar refactored
        //InputManager.controls.Toolbar.Navigate.performed += ctx => this.ManageMouseScroll(ctx.ReadValue<float>());
    }
    private void ManageMouseScroll(float value) {
        if (value > 0f) {
            this.UnSelectCurrentSlot();
            currentSelectedIdx = (currentSelectedIdx < this.cells.Length - 1) ? currentSelectedIdx + 1 : 0;
            this.SelectCurrentSlot();
        } else if (value < 0f) {
            this.UnSelectCurrentSlot();
            currentSelectedIdx = (currentSelectedIdx > 0) ? currentSelectedIdx - 1 : this.cells.Length - 1;
            this.SelectCurrentSlot();
        }
    }

    private void SelectCurrentSlot() {
        this.cells[currentSelectedIdx].SetState(CellState.SELECTED);
    }

    private void UnSelectCurrentSlot() {
        this.cells[currentSelectedIdx].SetState(CellState.ENABLED);
    }
}
