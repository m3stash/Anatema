using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    // KEEP only this properties bellow
    [Header("Base configuration (DON'T PUT SOMETHING IN BELOW FIELD)")]
    [SerializeField] private ItemConfig configuration;
    [SerializeField] private ItemPool associatedPool;

    [Header("Item Configuration")]
    [SerializeField] private int stacks;

    public virtual void Setup(ItemConfig config, ItemPool associatedPool = null) {
        this.configuration = config;
        this.associatedPool = associatedPool;
        this.stacks = 1;
        this.transform.name = this.configuration.GetDisplayName();

        // Do all other things in function of config....
    }

    public virtual void Destroy() {
        if (this.associatedPool) {
            this.associatedPool.ReturnObject(this);
        } else {
            Destroy(this.gameObject);
        }
    }

    public virtual void Use()
    {
        Debug.Log("Use method not implemented");
    }

    public ItemConfig GetConfig() {
        return this.configuration;
    }

    public int GetStacks()
    {
        return this.stacks;
    }
}