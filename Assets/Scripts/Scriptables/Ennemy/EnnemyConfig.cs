using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class DropConfig {
    public ItemConfig item;
    [Range(0, 100)]
    [Tooltip("% field exemple 1 = 1% || 90 = 90%")]
    public int rate;

    public DropConfig(ItemConfig item, int rate) {
        this.item = item;
        this.rate = rate;
    }
}

public class EnnemyConfig : ScriptableObject {

    [Header("Ennemy Config")]
    [SerializeField] private int id;
    [Min(1)]
    [SerializeField] private int life = 1;
    [SerializeField] private GameObject prefab;
    [SerializeField] private string displayName;
    [SerializeField] private bool pooleable;
    [Min(1)]
    [SerializeField] private int poolSize = 1;
    [Header("Senses")]
    [SerializeField] private bool canSee;
    [SerializeField] private bool canEar;
    [Header("Biomes")]
    [SerializeField] private bool Cavernes;
    [SerializeField] private bool Underground;
    [SerializeField] private bool Ruined;
    [SerializeField] private bool Surface;

    [Header("Resistance")]
    [SerializeField] private bool Fire;
    [SerializeField] private bool Lightning;
    [SerializeField] private bool Sacred;
    [SerializeField] private bool Shadow;

    [Header("Drop Settings")]
    [SerializeField] private DropConfig[] itemDrop;
    [Header("Light Settings")]
    [SerializeField] private bool emitLight;

    public int GetId() {
        return this.id;
    }

    public GameObject GetPrefab() {
        return this.prefab;
    }

    public int GetLife() {
        return this.life;
    }

    public string GetDisplayName() {
        return this.displayName;
    }

    public bool IsPooleable() {
        return this.pooleable;
    }

    public int GetPoolSize() {
        return this.poolSize;
    }

    public bool CanSee() {
        return this.canSee;
    }

    public bool CanEmitLight() {
        return this.emitLight;
    }

}