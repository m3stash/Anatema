using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Item))]
public class InventoryItem : MonoBehaviour
{
    [Header("Fields below are filled in runtime")]
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Image iconImage;
    [SerializeField] private Item item;

    private void Awake()
    {
        this.quantityText = GetComponentInChildren<TextMeshProUGUI>();
        this.iconImage = GetComponentInChildren<Image>();
        this.item = GetComponent<Item>();
    }

    public void Setup(Item item)
    {
        this.item.Setup(item);
        this.UpdateUI();
    }

    public void Stack(int quantityToAdd)
    {
        this.item.AddStacks(quantityToAdd);
        this.UpdateUI();
    }

    public Item GetItem()
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

