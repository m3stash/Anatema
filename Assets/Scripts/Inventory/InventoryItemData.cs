using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItemData
{
    [SerializeField] private ItemConfig itemConfig;
    [SerializeField] private int stacks;
    [SerializeField] private float durability; // It's an example

    public InventoryItemData() { }

    public InventoryItemData(ItemConfig itemConfig, int stacks, float durability)
    {
        this.itemConfig = itemConfig;
        this.stacks = stacks;
        this.durability = durability;
    }

    public ItemConfig GetConfig()
    {
        return this.itemConfig;
    }

    public int GetStacks()
    {
        return this.stacks;
    }

    public void SetStacks(int value)
    {
        this.stacks = value;
    }

    public void AddStacks(int quantityToAdd)
    {
        this.stacks += quantityToAdd;
    }

    public float GetDurability()
    {
        return this.durability;
    }

    public bool CanStack(int quantityToAdd)
    {
        return this.itemConfig.IsStackable() && this.stacks + quantityToAdd <= this.itemConfig.GetStackLimit();
    }
}
