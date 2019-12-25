using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block configuration", menuName = "Item/Block")]
public class BlockConfig : ItemConfig
{
    [Header("Block Configuration")]
    [SerializeField] private BlockType blockType;

    public BlockType GetBlockType() {
        return this.blockType;
    }
}

public enum BlockType
{
    DIRT,
    GRASS,
    SAND,
    ORE,
    COPPER
}
