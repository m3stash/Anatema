using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    // KEEP only this properties bellow
    [Header("Base configuration (DON'T PUT SOMETHING IN BELOW FIELD)")]
    [SerializeField] private ItemConfig configuration;
    [SerializeField] private ItemPool associatedPool;

    [Header("Item Configuration")]
    [SerializeField] private int stacks;

    private new Collider2D collider;
    private new Rigidbody2D rigidbody;

    private Vector2 defaultScale; // Used to reset scale when come back to his pool (Put it in ItemConfig ???)

    private void Start()
    {
        this.collider = GetComponent<Collider2D>();
    }

    public virtual void Setup(ItemConfig config, int stacks, ItemPool associatedPool = null)
    {
        this.configuration = config;
        this.associatedPool = associatedPool;
        this.stacks = stacks;
        this.transform.name = this.configuration.GetDisplayName();
        this.defaultScale = this.transform.localScale;

        // Do all other things in function of config....
    }

    public virtual void Destroy()
    {
        if (this.associatedPool)
        {
            Destroy(this.rigidbody);
            this.transform.localScale = this.defaultScale;
            this.associatedPool.ReturnObject(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void TransformToPickableItem()
    {
        this.collider.isTrigger = false;
        this.transform.localScale = this.configuration.GetPickableScale();
        this.rigidbody = this.gameObject.AddComponent<Rigidbody2D>();
        // Maybe pass tag to "Pickable" to detect all pickable item easily from player
    }

    public virtual void Use()
    {
        Debug.Log("Use method not implemented");
    }

    public ItemConfig GetConfig()
    {
        return this.configuration;
    }

    public int GetStacks()
    {
        return this.stacks;
    }
}
