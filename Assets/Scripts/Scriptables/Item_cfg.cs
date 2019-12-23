using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Type
{
    Block,
    Tool,
    Weapon,
    Accessory,
    Crafting,
    Consummable,
    Armor,
    Furniture,
};

public enum FurnitureType {
    None,
    Light,
    Storage,
    Decoration,
}

[CreateAssetMenu(fileName = "Item configuration", menuName = "Item/config")]
public class Item_cfg : ScriptableObject {

    [Header("Configuration Item")]
    public int id;
    public new string name;
    public Sprite sprite;
    [Space]
    public bool placeable;
    public Stacks stacks;
    public bool despawn;
    public Type type;
    public int width;
    public int height;
    [Space]
    [Tooltip("A n'utiliser que si Type === Furniture sinon rester sur None!")]
    [Header("Furniture Settings")]
    public FurnitureType furnitureType;
}