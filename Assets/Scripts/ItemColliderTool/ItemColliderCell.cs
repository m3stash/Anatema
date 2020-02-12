using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColliderCell : MonoBehaviour
{
    [SerializeField] private Vector2Int position;
    [SerializeField] private bool selected;
    [SerializeField] private bool origin;

    private new SpriteRenderer renderer;

    private void Awake() {
        this.renderer = GetComponent<SpriteRenderer>();
        this.RefreshUI();
    }

    public void Select() {
        this.selected = !this.selected;
        this.RefreshUI();
    }

    public void SetPosition(Vector2Int pos) {
        this.position = pos;
    }

    public void SetRelativePositionFrom(Vector2Int pos) {
        this.position = new Vector2Int(this.position.x - pos.x, this.position.y - pos.y);
    }

    public void SetOrigin() {
        this.origin = true;
        this.RefreshUI();
    }

    public Vector2Int GetPosition() {
        return this.position;
    }

    private void RefreshUI() {
        this.renderer.color = this.selected || this.origin ? new Color(1,0,0,0.7f) : new Color(1, 1, 1, 0.7f);
    }

    public bool IsSelected() {
        return this.selected;
    }

    public bool IsOrigin() {
        return this.origin;
    }
}
