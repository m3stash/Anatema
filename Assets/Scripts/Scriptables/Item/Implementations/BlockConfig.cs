using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block configuration", menuName = "Item/Block")]
public class BlockConfig : ItemConfig {

    [Header("Block Configuration")]
    [SerializeField] private BlockType blockType;
    [SerializeField] private int durability;

    public BlockType GetBlockType() {
        return this.blockType;
    }

    public int GetDurability() {
        return this.durability;
    }
}

public enum BlockType {
    DIRT,
    GRASS,
    SAND,
    ORE,
    WATER,
    LAVA,
    STONE,
    WOOD
}
