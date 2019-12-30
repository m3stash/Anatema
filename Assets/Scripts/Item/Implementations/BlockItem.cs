using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockItem : Item
{
    private float currentDurability; // in percent (ex: 100% of durability)

    public override void Setup(ItemConfig config, ItemStatus status, int stacks, ItemPool associatedPool = null) {
        // Do default setup
        base.Setup(config, status, stacks, associatedPool);

        // Do setup of my private properties
        this.currentDurability = 100;
    }

    /// <summary>
    /// Hit this block to decrease its durability
    /// </summary>
    /// <param name="damage">Damage to apply</param>
    public void Hit(float damage) {
        this.currentDurability -= damage;

        if(this.currentDurability <= 0) {
            this.TransformToPickableItem();
        }
    }
}
