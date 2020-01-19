using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryStepper : MonoBehaviour, IPointerClickHandler {

    [Header("Fields bellow need to be completed manually")]
    [SerializeField] private ItemType itemType;

    [Header("Don't touch it")]
    [SerializeField] private InventoryBag inventoryBag;

    private Image image;

    private void Awake() {
        this.inventoryBag = GetComponentInParent<InventoryBag>();
        this.image = GetComponent<Image>();

        this.ItemTypeChanged(this.inventoryBag.GetCurrentItemType());
    }

    private void OnEnable() {
        InventoryBag.OnItemTypeChanged += this.ItemTypeChanged;
    }

    private void OnDisable() {
        InventoryBag.OnItemTypeChanged -= this.ItemTypeChanged;
    }

    public void OnPointerClick(PointerEventData eventData) {
        this.inventoryBag.ChangeInventoryType(this.itemType);
    }

    private void ItemTypeChanged(ItemType type) {
        if(this.itemType == type) {
            this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1);
        } else {
            this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 0.5f);
        }
    }
}
