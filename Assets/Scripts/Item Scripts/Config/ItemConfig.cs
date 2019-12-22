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

    [Tooltip("Represent the icon in HUD")]
    [SerializeField] private Sprite icon;

    [SerializeField] private bool pooleable;

    [Min(1)]
    [SerializeField] private int poolSize = 1;

    /////////////////////////////////////

    [Space]

    [Header("Stack Settings")]

    [SerializeField] private bool stackable;

    [SerializeField] private Stacks stackLimit;

    [Space]

    /////////////////////////////////////

    [Space]

    [Header("Common Settings")]

    [SerializeField] private int width;

    [SerializeField] private int height;

    [SerializeField] private bool placeable;

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
}
