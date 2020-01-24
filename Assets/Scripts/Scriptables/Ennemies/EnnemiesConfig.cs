using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class EnnemiesConfig : ScriptableObject {
    [Header("Ennemy Settings")]

    [SerializeField] private int id;

    [SerializeField] private GameObject prefab;

    [SerializeField] private string displayName;

    [SerializeField] private ItemType itemType;

    [SerializeField] private bool pooleable;

    // Prévoir le biome ou il peut apparaitre

    [Min(1)]
    [SerializeField] private int poolSize = 1;

    /////////////////////////////////////

    [Header("Drop Settings")]
    [SerializeField] private GameObject[] itemDrop;

    /////////////////////////////////////

    [Header("Light Settings")]
    [SerializeField] private bool emitLight;


    public int GetId() {
        return this.id;
    }

    public GameObject GetPrefab() {
        return this.prefab;
    }

    public string GetDisplayName() {
        return this.displayName;
    }

    public ItemType GetItemType() {
        return this.itemType;
    }

    public int GetPoolSize() {
        return this.poolSize;
    }

    public bool IsPooleable() {
        return this.pooleable;
    }

    public bool CanEmitLight() {
        return this.emitLight;
    }

}