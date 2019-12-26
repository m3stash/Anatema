using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable configuration", menuName = "Item/Consumable")]
public class ConsumableConfig : ItemConfig
{
    [Header("Consumable Configuration")]
    [SerializeField] private ConsumableType type;

    public ConsumableType GetConsumableType()
    {
        return this.type;
    }
}

public enum ConsumableType
{
    LIFE,
    SPEED
}