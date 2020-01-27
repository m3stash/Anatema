using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Toolbar configuration", menuName = "Toolbar")]
public class ToolbarConfig: ScriptableObject
{
    [SerializeField] private ToolbarType type;
    [SerializeField] private ItemType[] allowedItemTypes;
    [SerializeField] private int size;

    public ToolbarType GetToolbarType() {
        return this.type;
    }

    public int GetSize() {
        return this.size;
    }

    public ItemType[] GetAllowedItemTypes() {
        return this.allowedItemTypes;
    }
}

public enum ToolbarType {
    BUILD,
    CONSUMABLE,
    TOOL
}