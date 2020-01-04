using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ItemConfig : ScriptableObject
{
    [Header("Main Settings")]

    [SerializeField] private int id;

    [SerializeField] private GameObject prefab;

    [SerializeField] private string displayName;

    [SerializeField] private ItemType itemType;

    [Tooltip("Represent the icon in HUD")]
    [SerializeField] private Sprite icon;

    [SerializeField] private bool pooleable;

    [Min(1)]
    [SerializeField] private int poolSize = 1;

    /////////////////////////////////////

    [Header("Stack Settings")]

    [SerializeField] private bool stackable;

    [SerializeField] private Stacks stackLimit;

    /////////////////////////////////////

    [Header("Common Settings")]

    [SerializeField] private int width;

    [SerializeField] private int height;

    [SerializeField] private bool placeable;

    /////////////////////////////////////

    [Tooltip("Properties used to configure item state when pickable mode")]
    [Header("Pickable State Settings")]

    [SerializeField] private Vector2 scale; // Represent the scale size of item when it's pickable

    public int GetId() {
        return this.id;
    }

    public GameObject GetPrefab() {
        return this.prefab;
    }

    public Sprite GetIcon() {
        return this.icon;
    }

    public string GetDisplayName() {
        return this.displayName;
    }

    public ItemType GetItemType()
    {
        return this.itemType;
    }

    public int GetPoolSize() {
        return this.poolSize;
    }

    public bool IsStackable() {
        return this.stackable;
    }

    public int GetStackLimit() {
        return (int)Enum.Parse(typeof(Stacks), this.stackLimit.ToString());
    }

    public int GetWidth() {
        return this.width;
    }

    public int GetHeight() {
        return this.height;
    }

    public bool IsPlaceable() {
        return this.placeable;
    }

    public bool IsPooleable()
    {
        return this.pooleable;
    }

    public Vector2 GetPickableScale() {
        return this.scale;
    }
}

public enum Stacks
{
    potions = 10,
    consumables = 99,
    blocks = 5, // 999
    weapons = 1,
}

public enum ItemType
{
    BLOCK,
    TOOL,
    WEAPON,
    FURNITURE,
    CONSUMABLE,
    EQUIPMENT
}
