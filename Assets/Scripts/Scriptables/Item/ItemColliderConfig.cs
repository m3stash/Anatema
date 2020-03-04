using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Collider configuration", menuName = "Item Collider Config")]
public class ItemColliderConfig : ScriptableObject {

    [SerializeField] private CellCollider[] cellColliders;
    [SerializeField] private BlockType[] unAllowedBlockTypes;

    public void Setup(CellCollider[] cellColliders, BlockType[] unAllowedBlockTypes) {
        this.cellColliders = cellColliders;
        this.unAllowedBlockTypes = unAllowedBlockTypes;
    }

    public CellCollider[] GetCellColliders() {
        return this.cellColliders;
    }

    public BlockType[] GetUnAllowedBlockTypes() {
        return this.unAllowedBlockTypes;
    }
}

[System.Serializable]
public class CellCollider {

    [SerializeField] private Vector2Int position;
    [SerializeField] private bool origin;
    [SerializeField] private ContactType leftContactType;
    [SerializeField] private ContactType rightContactType;
    [SerializeField] private ContactType topContactType;
    [SerializeField] private ContactType bottomContactType;

    public CellCollider(ItemColliderCell cell) {
        this.position = cell.GetPosition();
        this.origin = cell.IsOrigin();
        this.leftContactType = cell.GetLeftContactPoint().GetContactType();
        this.rightContactType = cell.GetRightContactPoint().GetContactType();
        this.topContactType = cell.GetTopContactPoint().GetContactType();
        this.bottomContactType = cell.GetBottomContactPoint().GetContactType();
    }

    public ContactType GetLeftContactType() {
        return this.leftContactType;
    }

    public ContactType GetRightContactType() {
        return this.rightContactType;
    }

    public ContactType GetTopContactType() {
        return this.topContactType;
    }

    public ContactType GetBottomContactType() {
        return this.bottomContactType;
    }

    /// <summary>
    /// Return relative position from origin cell
    /// </summary>
    /// <returns></returns>
    public Vector2Int GetRelativePosition() {
        return this.position;
    }

    public bool IsOrigin() {
        return this.origin;
    }
}
