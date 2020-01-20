using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItemData {
    [SerializeField] private ItemConfig itemConfig;
    [SerializeField] private int stacks;
    [SerializeField] private float durability; // It's an example

    public InventoryItemData() { }

    public InventoryItemData(ItemConfig itemConfig, int stacks, float durability) {
        this.itemConfig = itemConfig;
        this.stacks = stacks;
        this.durability = durability;
    }

    public ItemConfig GetConfig() {
        return this.itemConfig;
    }

    public int GetStacks() {
        return this.stacks;
    }

    public int GetOverflowStacks() {
        return this.stacks - this.itemConfig.GetStackLimit();
    }

    public void SetStacks(int value) {
        this.stacks = value;
    }

    public void AddStacks(int quantityToAdd) {
        this.stacks += quantityToAdd;
    }

    public void RemoveStacks(int quantityToRemove) {
        this.stacks -= quantityToRemove;
    }

    public float GetDurability() {
        return this.durability;
    }

    public bool CanStack() {
        return this.itemConfig.IsStackable() && this.stacks < this.itemConfig.GetStackLimit();
    }

    public bool IsSameThan(InventoryItemData itemData) {
        return this.itemConfig.GetId().Equals(itemData.GetConfig().GetId());
    }
}
