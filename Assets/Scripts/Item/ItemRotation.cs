using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemRotation : MonoBehaviour {
    [Header("Fields to complete")]
    [SerializeField] private Sprite leftCollisionSprite;
    [SerializeField] private Sprite rightCollisionSprite;
    [SerializeField] private Sprite topCollisionSprite;
    [SerializeField] private Sprite bottomCollisionSprite;

    [Header("Don't touch it")]
    [SerializeField] private Direction collisionSide = Direction.BOTTOM;
    private new SpriteRenderer renderer;

    private void Awake() {
        this.renderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Used to apply correct sprite renderer in function of collision
    /// </summary>
    public void RefreshUI() {
        if(rightCollisionSprite && WorldManager.instance.tilesWorldMap[(int)this.transform.position.x + 1, (int)this.transform.position.y] > 0) {
            this.renderer.sprite = this.rightCollisionSprite;
            this.collisionSide = Direction.RIGHT;
        } else if(leftCollisionSprite && WorldManager.instance.tilesWorldMap[(int)this.transform.position.x - 1, (int)this.transform.position.y] > 0) {
            this.renderer.sprite = this.leftCollisionSprite;
            this.collisionSide = Direction.LEFT;
        } else if(topCollisionSprite && WorldManager.instance.tilesWorldMap[(int)this.transform.position.x, (int)this.transform.position.y + 1] > 0) {
            this.renderer.sprite = this.topCollisionSprite;
            this.collisionSide = Direction.TOP;
        } else if(bottomCollisionSprite && WorldManager.instance.tilesWorldMap[(int)this.transform.position.x, (int)this.transform.position.y - 1] > 0) {
            this.renderer.sprite = this.bottomCollisionSprite;
            this.collisionSide = Direction.BOTTOM;
        }
    }

    public Direction GetCollisionSide() {
        return this.collisionSide;
    }
}

public enum Direction {
    BOTTOM,
    TOP,
    LEFT,
    RIGHT
}

public enum ReverseDirection {
    BOTTOM = Direction.TOP,
    TOP = Direction.BOTTOM,
    LEFT = Direction.RIGHT,
    RIGHT = Direction.LEFT
}
