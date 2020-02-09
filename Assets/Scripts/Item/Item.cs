using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Item : MonoBehaviour {
    // KEEP only this properties bellow
    [Header("Base configuration (DON'T PUT SOMETHING IN BELOW FIELD)")]
    [SerializeField] private ItemConfig configuration;
    [SerializeField] private ItemPool associatedPool;

    protected int stacks;
    protected ItemStatus status;
    protected ParticleSystem lootParticle;

    // Fields bellow are only used for pickable items
    protected Vector3 oldPosition;
    protected float timeInMove;

    [SerializeField] protected new Rigidbody2D rigidbody;
    [SerializeField] protected new Collider2D collider;
    [SerializeField] protected new SpriteRenderer renderer;
    [SerializeField] protected Attractor attractor;

    [SerializeField] private Vector3 defaultScale; // Used to reset scale when come back to his pool (Put it in ItemConfig ???)
    [SerializeField] private string defaultTag;
    [SerializeField] private Sprite defaultPrefabSprite;

    public delegate void ItemMoved(Item item);
    public static ItemMoved OnItemMoved;

    public delegate void ItemDestroyed(Item item);
    public static ItemDestroyed OnItemDestroyed;

    public virtual void Setup(ItemConfig config, ItemStatus status, int stacks, ItemPool associatedPool = null) {
        // Get components references
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.renderer = GetComponent<SpriteRenderer>();

        this.configuration = config;
        this.associatedPool = associatedPool;
        this.stacks = stacks;
        this.transform.name = this.configuration.GetDisplayName();
        this.defaultScale = this.transform.localScale;
        this.defaultTag = this.transform.tag;
        this.defaultPrefabSprite = this.renderer.sprite;
        this.status = status;

        // Manage default status
        switch(this.status) {
            case ItemStatus.ACTIVE:
                this.gameObject.SetActive(true);
                break;

            case ItemStatus.PICKABLE:
                this.gameObject.SetActive(true);
                this.TransformToPickableItem();
                break;

            case ItemStatus.INACTIVE:
                this.gameObject.SetActive(false);
                break;
        }

    }

    private void Update() {
        if(this.status == ItemStatus.PICKABLE && Vector3.Distance(this.transform.position, this.oldPosition) >= 0.2f) {
            this.timeInMove += Time.deltaTime;

            // Destroy this item if it's falling since more 5seconds to prevent missing chunk
            if(this.timeInMove >= 5f) {
                this.Destroy();
            } else {
                this.oldPosition = this.transform.position;
                OnItemMoved?.Invoke(this);
            }
        } else {
            this.timeInMove = 0;
        }
    }

    public virtual void Setup(Item item) {
        this.configuration = item.configuration;
        this.stacks = item.stacks;
    }

    /// <summary>
    /// Used to remove object from the map
    /// If object was get from a pool it is return to it;
    /// If object is just an simple instantiation it'll be destroyed completely
    /// </summary>
    public virtual void Destroy() {
        // Check if this is not null to avoid error after game stopped
        if(this) {
            // TODO check performance when chunk clear its items
            OnItemDestroyed?.Invoke(this);

            if(this.associatedPool) {
                Destroy(this.rigidbody);

                if(this.lootParticle) {
                    Destroy(this.lootParticle.gameObject);
                }

                if(this.attractor) {
                    this.attractor.Reset();
                }

                this.transform.localScale = this.defaultScale;
                this.transform.tag = this.defaultTag;
                this.renderer.sprite = this.defaultPrefabSprite;
                this.status = ItemStatus.INACTIVE;
                this.associatedPool.ReturnObject(this);
            } else {
                Destroy(this.gameObject);
            }
        }
    }

    /// <summary>
    /// Convert your item to a pickable item on the world
    /// </summary>
    public void TransformToPickableItem() {
        // Check rigidbody in case of item already have rigidbody in prefab
        if(!this.rigidbody) {
            this.rigidbody = this.gameObject.AddComponent<Rigidbody2D>();
        }

        if(!this.attractor) {
            this.attractor = this.gameObject.AddComponent<Attractor>();
        }

        // Add particle for loot when rarity level is greater than common
        if(!this.configuration.GetRarityLevel().Equals(RarityLevel.COMMON)) {
            this.CreateLootParticle();
        }

        // Other settings
        this.transform.localScale = this.configuration.GetPickableScale();
        this.renderer.sprite = this.configuration.GetIcon();
        this.transform.tag = "Pickable";
        this.transform.parent = null;
    }

    /// <summary>
    /// Need to be implemented to be specific per item
    /// </summary>
    public virtual void Use() {
        Debug.Log("Use method not implemented");
    }

    /// <summary>
    /// Used to show or hide item
    /// </summary>
    /// <param name="visible"></param>
    public virtual void SetVisible(bool visible) {
        this.gameObject.SetActive(visible);
    }

    public ItemConfig GetConfig() {
        return this.configuration;
    }

    public int GetStacks() {
        return this.stacks;
    }

    public void SetStacks(int value) {
        this.stacks = value;
    }

    public void AddStacks(int quantityToAdd) {
        this.stacks += quantityToAdd;
    }

    public bool CanStack(int quantityToAdd) {
        return this.stacks + quantityToAdd <= this.configuration.GetStackLimit();
    }

    public ItemStatus GetStatus() {
        return this.status;
    }

    private void CreateLootParticle() {
        GameObject lootObj = Instantiate(this.configuration.GetLootParticle(), this.transform);

        this.lootParticle = lootObj.GetComponent<ParticleSystem>();

        // Change color with rarity
        ParticleSystem.MainModule lootParticleMain = this.lootParticle.main;
        lootParticleMain.startColor = this.configuration.GetRarityLevelColor();

        // Adapt radius ratio based with scale of (0.5,0.5)
        ParticleSystem.ShapeModule lootParticleShape = this.lootParticle.shape;
        lootParticleShape.radius = (this.configuration.GetPickableScale().x * .28f) / .5f;

        // Adapt sprite colors
        foreach(SpriteRenderer renderer in lootObj.GetComponentsInChildren<SpriteRenderer>()) {
            renderer.color = new Color(this.configuration.GetRarityLevelColor().r,
                this.configuration.GetRarityLevelColor().g,
                this.configuration.GetRarityLevelColor().b,
                renderer.color.a);
        }
    }
}

public enum ItemStatus {
    ACTIVE,
    INACTIVE,
    PICKABLE
}
