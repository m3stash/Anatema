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
    [SerializeField] protected int stacks;

    [SerializeField] protected new Rigidbody2D rigidbody;
    [SerializeField] protected new Collider2D collider;
    [SerializeField] protected new SpriteRenderer renderer;

    [SerializeField] private Vector2 defaultScale; // Used to reset scale when come back to his pool (Put it in ItemConfig ???)
    [SerializeField] private string defaultTag;
    [SerializeField] private Sprite defaultPrefabSprite;

    public virtual void Setup(ItemConfig config, int stacks, ItemPool associatedPool = null) {
        // Get components references
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.collider = GetComponent<Collider2D>();
        this.renderer = GetComponent<SpriteRenderer>();

        this.configuration = config;
        this.associatedPool = associatedPool;
        this.stacks = stacks;
        this.transform.name = this.configuration.GetDisplayName();
        this.defaultScale = this.transform.localScale;
        this.defaultTag = this.transform.tag;
        this.defaultPrefabSprite = this.renderer.sprite;

        // Do all other things in function of config....
    }

    /// <summary>
    /// Used to remove object from the map
    /// If object was get from a pool it is return to it;
    /// If object is just an simple instantiation it'll be destroyed completely
    /// </summary>
    public virtual void Destroy() {
        if (this.associatedPool) {
            Destroy(this.rigidbody);
            this.transform.localScale = this.defaultScale;
            this.transform.tag = this.defaultTag;
            this.renderer.sprite = this.defaultPrefabSprite;
            this.associatedPool.ReturnObject(this);
        } else {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Convert your item to a pickable item on the world
    /// </summary>
    public void TransformToPickableItem() {
        // Check rigidbody in case of item already have rigidbody in prefab
        if (!this.rigidbody) {
            this.rigidbody = this.gameObject.AddComponent<Rigidbody2D>();
        }

        this.transform.localScale = this.configuration.GetPickableScale();
        this.renderer.sprite = this.configuration.GetIcon();
        this.transform.tag = "Pickable";
        this.transform.parent = null;
    }

    public virtual void Use() {
        Debug.Log("Use method not implemented");
    }

    public ItemConfig GetConfig() {
        return this.configuration;
    }

    public int GetStacks() {
        return this.stacks;
    }
}
