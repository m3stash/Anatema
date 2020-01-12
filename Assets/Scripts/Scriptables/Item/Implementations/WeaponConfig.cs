using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon configuration", menuName = "Item/Weapon")]
public class WeaponConfig : ItemConfig {

    [Header("Weapon Configuration")]
    [SerializeField] private float damage;

    [SerializeField] private float durability;

    public float GetDamage() {
        return this.damage;
    }

    public float GetDurability() {
        return this.durability;
    }
}
