using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    [Header("Fields below are filled in runtime")]
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Image iconImage;
    [SerializeField] private InventoryItemData item;

    private void Awake()
    {
        this.quantityText = GetComponentInChildren<TextMeshProUGUI>();
        this.iconImage = GetComponentInChildren<Image>();
    }

    public void Setup(InventoryItemData item)
    {
        this.item = item;
        this.UpdateUI();
    }

    public InventoryItemData GetItem()
    {
        return this.item;
    }

    private void UpdateUI()
    {
        this.quantityText.text = this.item.GetStacks().ToString();
        this.iconImage.color = new Color(1, 1, 1, 1);
        this.iconImage.sprite = this.item.GetConfig().GetIcon();
    }
}

