using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Image iconImage;

    private Item item;

    private void Awake() {
    }

    public void Setup(Item item) {
        this.item = this.gameObject.AddComponent<Item>();
        this.item.Setup(item);
        this.UpdateUI();
    }

    public void Stack(int quantityToAdd) {
        this.item.AddStacks(quantityToAdd);
        this.UpdateUI();
    }

    public Item GetItem() {
        return this.item;
    }

    public void RemoveItem() {
        Destroy(this.item);
    }

    public Item GetAssociatedItem() {
        return this.item;
    }

    private void UpdateUI() {
        if (this.item) {
            this.quantityText.text = this.item.GetStacks().ToString();
            this.iconImage.color = new Color(1, 1, 1, 1);
            this.iconImage.sprite = this.item.GetConfig().GetIcon();
        } else {
            this.quantityText.text = "";
            this.iconImage.sprite = null;
            this.iconImage.color = new Color(0, 0, 0, 0);
        }
    }
}

