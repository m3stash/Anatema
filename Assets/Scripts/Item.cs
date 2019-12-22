using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    [SerializeField]
    private string nameDisplay;
    [SerializeField]
    private int id;
    public Item_cfg config;

    public int maxStacks;
    public int currentStack;
    public string type;
    public int width;
    public int height;
    public string furnitureType;

    // MINE
    [SerializeField] private ItemConfig configuration;
    [SerializeField] private ItemPool associatedPool;
    private int stacks;

    void Start() {
        currentStack = currentStack == 0 ? 1 : currentStack;
        gameObject.GetComponent<SpriteRenderer>().sprite = config.sprite;
        nameDisplay = config.name;
        id = config.id;
        width = config.width;
        height = config.height;
        type = config.type.ToString();
        furnitureType = config.furnitureType.ToString();
        maxStacks = (int)Enum.Parse(typeof(Stacks), config.stacks.ToString());
    }

    public virtual void Setup(ItemConfig config, ItemPool associatedPool) {
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

    public ItemConfig GetConfig() {
        return this.configuration;
    }
}