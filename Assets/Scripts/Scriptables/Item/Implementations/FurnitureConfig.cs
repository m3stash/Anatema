using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Furniture configuration", menuName = "Item/Furniture")]
public class FurnitureConfig : ItemConfig {
    [Header("Furniture Configuration")]
    [SerializeField] private FurnitureType furnitureType;

    public FurnitureType GetFurnitureType() {
        return this.furnitureType;
    }
}

public enum FurnitureType {
    Storage,
    Decoration,
}