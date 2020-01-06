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

    [SerializeField] private string description;

    [SerializeField] private RarityLevel rarityLevel;

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

    public int GetId()
    {
        return this.id;
    }

    public GameObject GetPrefab()
    {
        return this.prefab;
    }

    public Sprite GetIcon()
    {
        return this.icon;
    }

    public string GetDisplayName()
    {
        return this.displayName;
    }

    public ItemType GetItemType()
    {
        return this.itemType;
    }

    public int GetPoolSize()
    {
        return this.poolSize;
    }

    public string GetDescription()
    {
        return this.description;
    }

    public bool IsStackable()
    {
        return this.stackable;
    }

    public int GetStackLimit()
    {
        return (int)Enum.Parse(typeof(Stacks), this.stackLimit.ToString());
    }

    public int GetWidth()
    {
        return this.width;
    }

    public int GetHeight()
    {
        return this.height;
    }

    public bool IsPlaceable()
    {
        return this.placeable;
    }

    public bool IsPooleable()
    {
        return this.pooleable;
    }

    public Vector2 GetPickableScale()
    {
        return this.scale;
    }

    public RarityLevel GetRarityLevel()
    {
        return this.rarityLevel;
    }

    public string GetRarityLevelToString()
    {
        return (string)Enum.Parse(typeof(RarityLevel), this.rarityLevel.ToString());
    }

    public Color32 GetRarityLevelColor()
    {
        Color32 color = new Color32();

        switch (this.rarityLevel)
        {
            case RarityLevel.COMMON:
                color = new Color32(255, 255, 255, 0); // Transparent
                break;

            case RarityLevel.UNCOMMON:
                color = new Color32(14, 191, 8, 255); // Green
                break;

            case RarityLevel.RARE:
                color = new Color32(8, 139, 191, 255); // Blue
                break;

            case RarityLevel.EPIC:
                color = new Color32(132, 3, 252, 255); // Purple
                break;

            case RarityLevel.LEGENDARY:
                color = new Color32(252, 198, 3, 255); // Gold
                break;
        }

        return color;
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

public enum RarityLevel
{
    COMMON,
    UNCOMMON,
    RARE,
    EPIC,
    LEGENDARY
}
