using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : InventoryUI
{
    [Header("Fields to complete manually")]
    [SerializeField] private ToolbarType toolbarType;

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
        ToolbarManager.OnToolbarChanged += ToolbarChanged;

        this.UpdateCellState();
        this.RefreshItems();
    }

    private void OnDisable() {
        ToolbarManager.OnToolbarChanged -= ToolbarChanged;
    }

    public void SelectNextSlot() {
        if (this.cells.Length > 0) {
            currentSelectedIdx = (currentSelectedIdx < this.cells.Length - 1) ? currentSelectedIdx + 1 : 0;
        } else {
            currentSelectedIdx = 0;
        }

        ToolbarManager.instance.SetCurrentSelectedIdx(this.currentSelectedIdx);

        this.UpdateCellState();
    }

    public void SelectPreviousSlot() {
        if (this.cells.Length > 0) {
            currentSelectedIdx = (currentSelectedIdx > 0) ? currentSelectedIdx - 1 : this.cells.Length - 1;
        } else {
            currentSelectedIdx = 0;
        }

        ToolbarManager.instance.SetCurrentSelectedIdx(this.currentSelectedIdx);

        this.UpdateCellState();
    }

    public void UpdateCellState() {
        for (int i = 0; i < this.cells.Length; i++) {
            this.cells[i].SetState(i == this.currentSelectedIdx ? CellState.SELECTED : CellState.ENABLED);
        }
    }

    private void ToolbarChanged(ToolbarType type) {
        if (this.toolbarType.Equals(type)) {
            this.RefreshItems();
        }
    }

    private void RefreshItems() {
        this.items = ToolbarManager.instance.GetToolbarItems(this.toolbarType);
        this.RefreshUI();
    }
}
