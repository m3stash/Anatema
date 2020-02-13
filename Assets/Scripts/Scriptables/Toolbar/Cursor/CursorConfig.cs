using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cursor configuration", menuName = "Cursor")]
public class CursorConfig : ScriptableObject
{
    [SerializeField] private CursorState cursorState;
    [SerializeField] private Texture2D mouseTexture;
    [SerializeField] private Sprite controllerSprite;

    public Sprite GetControllerSprite() {
        return this.controllerSprite;
    }

    public Texture2D GetMouseTexture() {
        return this.mouseTexture;
    }

    public CursorState GetCursorState() {
        return this.cursorState;
    }
}

public enum CursorState
{
    INVENTORY_NAVIGATION,
    INVENTORY_DRAG,
    TOOL,
    BUILD,
    DISABLED
}