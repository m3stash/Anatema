using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
    [Header("Fields to complete manually")]
    [SerializeField] protected Transform slotContainer;

    [Header("Don't touch it")]
    [SerializeField] protected InventoryCell[] cells;
    [SerializeField] protected InventoryItemData[] items;

    protected virtual void Awake() {
        this.cells = this.slotContainer.GetComponentsInChildren<InventoryCell>();
    }

    /// <summary>
    /// This methods is used by dragAndDropCell, it needs to be overriden for a specific need
    /// </summary>
    /// <param name="source"> source cell </param>
    /// <param name="target"> target cell </param>
    public virtual void SwapCells(InventoryCell source, InventoryCell target) {
        throw new System.Exception("Swap method not implemented");
    }

    /// <summary>
    /// This methods is used to drop item, it needs to be overriden for a specific need
    /// </summary>
    /// <param name="cell"></param>
    public virtual void DropItem(InventoryCell cell) {
        throw new System.Exception("Drop method not implemented");
    }

    /// <summary>
    /// This methods is used to delete item, it needs to be overriden for a specific need
    /// </summary>
    /// <param name="cell"></param>
    public virtual void DeleteItem(InventoryCell cell) {
        throw new System.Exception("Delete method not implemented");
    }

    protected void RefreshUI() {
        for(int i = 0; i < this.cells.Length; i++) {
            this.cells[i].UpdateItem(this.items[i]);
        }
    }

    protected int GetCellIdx(InventoryCell cell) {
        for(int i = 0; i < this.cells.Length; i++) {
            if(this.cells[i] == cell) {
                return i;
            }
        }

        return -1;
    }
}
