﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryCell : MonoBehaviour
{
    [Header("Fields to complete manually")]
    [SerializeField] private GameObject slotItemPrefab;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite selectedSprite;

    [Header("Don't touch it")]
    [SerializeField] private InventoryItem inventoryItem;

    private Button button;
    private new Image renderer;

    public delegate void OnClick(InventoryCell cell);
    public static event OnClick NotifyClickEvent;

    public delegate void OnItemChanged(InventoryCell cell);
    public static event OnItemChanged NotifyItemChanged;

    public delegate void OnItemDrop(InventoryCell cell);
    public static event OnItemDrop NotifyItemDrop;

    private void Awake()
    {
        this.button = GetComponent<Button>();
        this.renderer = GetComponent<Image>();
    }

    private void Start()
    {
        this.button.onClick.AddListener(() => this.NotifyClick());
    }

    private void NotifyClick()
    {
        // Notify if this cell contains an item and event handler have atleast one subscriber
        if (this.inventoryItem && NotifyClickEvent != null)
        {
            NotifyClickEvent(this);
        }
    }

    public void RefreshItemReference()
    {
        this.inventoryItem = GetComponentInChildren<InventoryItem>();

        NotifyItemChanged?.Invoke(this);
    }

    public void DropItem()
    {
        NotifyItemDrop?.Invoke(this);
    }

    public void Select()
    {
        if (!this.renderer)
        {
            this.renderer = GetComponent<Image>();
        }

        this.renderer.sprite = this.selectedSprite;
    }

    public void UnSelect()
    {
        if (!this.renderer)
        {
            this.renderer = GetComponent<Image>();
        }

        this.renderer.sprite = this.defaultSprite;
    }


    public void UpdateItem(InventoryItemData item)
    {
        if (item != null && item.GetConfig() != null)
        {
            if (!inventoryItem)
            {
                GameObject obj = Instantiate(this.slotItemPrefab, this.transform);
                this.inventoryItem = obj.GetComponent<InventoryItem>();
            }

            inventoryItem.Setup(item);
        }
        else if (((item != null && item.GetConfig() == null) || item == null) && inventoryItem)
        {
            Destroy(this.inventoryItem.gameObject);
        }
    }

    public InventoryItem GetInventoryItem()
    {
        return this.inventoryItem;
    }

    private void OnDestroy()
    {
        this.button.onClick.RemoveAllListeners();
    }
}