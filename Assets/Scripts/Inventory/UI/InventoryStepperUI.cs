using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryStepperUI : StepperUI {

    [Header("Fields bellow need to be completed manually")]
    [SerializeField] private ItemType itemType;

    public ItemType GetItemType() {
        return this.itemType;
    }
}
