using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemRotation : MonoBehaviour
{
    [Header("Fields to complete")]
    [SerializeField] private Sprite leftCollisionSprite;
    [SerializeField] private Sprite rightCollisionSprite;
    [SerializeField] private Sprite topCollisionSprite;
    [SerializeField] private Sprite bottomCollisionSprite;

    private new SpriteRenderer renderer;

    private void Awake() {
        this.renderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Used to apply correct sprite renderer in function of collision
    /// </summary>
    public void RefreshUI() {
        if (rightCollisionSprite && WorldManager.tilesWorldMap[(int)this.transform.position.x + 1, (int)this.transform.position.y] > 0) {
            this.renderer.sprite = this.rightCollisionSprite;
        } else if (leftCollisionSprite && WorldManager.tilesWorldMap[(int)this.transform.position.x - 1, (int)this.transform.position.y] > 0) {
            this.renderer.sprite = this.leftCollisionSprite;
        } else if (topCollisionSprite && WorldManager.tilesWorldMap[(int)this.transform.position.x, (int)this.transform.position.y + 1] > 0) {
            this.renderer.sprite = this.topCollisionSprite;
        } else if (bottomCollisionSprite && WorldManager.tilesWorldMap[(int)this.transform.position.x, (int)this.transform.position.y - 1] > 0) {
            this.renderer.sprite = this.bottomCollisionSprite;
        }
    }
}
