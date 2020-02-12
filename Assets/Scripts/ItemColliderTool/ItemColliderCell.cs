using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColliderCell : MonoBehaviour {
    [Header("Fields to complete")]
    [SerializeField] private GameObject originPoint;
    [SerializeField] private CellContactPoint leftContactPoint;
    [SerializeField] private CellContactPoint rightContactPoint;
    [SerializeField] private CellContactPoint topContactPoint;
    [SerializeField] private CellContactPoint bottomContactPoint;

    [Header("Don't touch it")]
    [SerializeField] private Vector2Int position;
    [SerializeField] private bool selected;
    [SerializeField] private bool origin;
    [SerializeField] private new SpriteRenderer renderer;

    public delegate void ItemColliderCellChanged();
    public static event ItemColliderCellChanged OnItemColliderCellChanged;

    private void Awake() {
        this.renderer = GetComponent<SpriteRenderer>();
        this.RefreshUI();
    }

    private void OnEnable() {
        ItemColliderCell.OnItemColliderCellChanged += ManageContactPoints;
    }

    private void OnDisable() {
        ItemColliderCell.OnItemColliderCellChanged -= ManageContactPoints;
    }

    public CellContactPoint GetLeftContactPoint() {
        return this.leftContactPoint;
    }

    public CellContactPoint GetRightContactPoint() {
        return this.rightContactPoint;
    }

    public CellContactPoint GetTopContactPoint() {
        return this.topContactPoint;
    }

    public CellContactPoint GetBottomContactPoint() {
        return this.bottomContactPoint;
    }

    public void Select() {
        this.selected = !this.selected;
        this.RefreshUI();

        OnItemColliderCellChanged?.Invoke();
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

        OnItemColliderCellChanged?.Invoke();
    }

    public Vector2Int GetPosition() {
        return this.position;
    }

    private void RefreshUI() {
        this.renderer.color = this.selected || this.origin ? new Color(1, 0, 0, 0.7f) : new Color(1, 1, 1, 0.7f);
        this.originPoint.SetActive(this.origin);
    }

    public bool IsSelected() {
        return this.selected;
    }

    public bool IsOrigin() {
        return this.origin;
    }

    /// <summary>
    /// Used to enable/disable cell contact points
    /// </summary>
    private void ManageContactPoints() {
        if (!this.selected && !this.origin) {
            this.topContactPoint.gameObject.SetActive(false);
            this.bottomContactPoint.gameObject.SetActive(false);
            this.leftContactPoint.gameObject.SetActive(false);
            this.rightContactPoint.gameObject.SetActive(false);
            return;
        }

        RaycastHit2D topHit = Physics2D.Raycast(this.transform.position + new Vector3(0, 1, 0), Vector2.zero);
        RaycastHit2D bottomHit = Physics2D.Raycast(this.transform.position + new Vector3(0, -1, 0), Vector2.zero);
        RaycastHit2D leftHit = Physics2D.Raycast(this.transform.position + new Vector3(-1, 0, 0), Vector2.zero);
        RaycastHit2D rightHit = Physics2D.Raycast(this.transform.position + new Vector3(1, 0, 0), Vector2.zero);

        this.CheckActiveState(rightHit.collider, this.rightContactPoint);
        this.CheckActiveState(leftHit.collider, this.leftContactPoint);
        this.CheckActiveState(topHit.collider, this.topContactPoint);
        this.CheckActiveState(bottomHit.collider, this.bottomContactPoint);
    }

    private void CheckActiveState(Collider2D collider, CellContactPoint associatedPoint) {
        if(collider) {
            ItemColliderCell cell = collider.GetComponent<ItemColliderCell>();
            associatedPoint.gameObject.SetActive(cell && !cell.IsSelected() && !cell.IsOrigin());
        } else {
            associatedPoint.gameObject.SetActive(true);
        }
    }
}
