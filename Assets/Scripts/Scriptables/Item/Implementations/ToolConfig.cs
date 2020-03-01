using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool configuration", menuName = "Item/Tool")]
public class ToolConfig : ItemConfig {

    [Header("Tool configuration")]
    [SerializeField] private ToolType toolType;

    public ToolType GetToolType() {
        return this.toolType;
    }
}

[System.Serializable]
public enum ToolType
{
    AXE,
    PICKAXE,
    HAMMER
}