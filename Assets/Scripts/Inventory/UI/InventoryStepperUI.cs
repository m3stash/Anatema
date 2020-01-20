using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryStepperUI : StepperUI {

    [Header("Fields bellow need to be completed manually")]
    [SerializeField] private ItemType itemType;

    public delegate void InventoryTypeChanged(ItemType type);
    public static InventoryTypeChanged OnInventoryTypeChanged;

    public override void Select() {
        base.Select();
        OnInventoryTypeChanged?.Invoke(this.itemType);
    }
}
