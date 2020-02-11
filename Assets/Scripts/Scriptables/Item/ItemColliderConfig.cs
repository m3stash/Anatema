using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Collider configuration", menuName = "Item Collider Config")]
public class ItemColliderConfig : ScriptableObject {

    [SerializeField] private CellCollider[] cellColliders;

    public void SetCellColliders(CellCollider[] cellColliders) {
        this.cellColliders = cellColliders;
    }

    public CellCollider[] GetCellColliders() {
        return this.cellColliders;
    }
}

[System.Serializable]
public class CellCollider {

    [SerializeField] private Vector2Int position;
    [SerializeField] private bool origin;

    public CellCollider(ItemColliderCell cell) {
        position = cell.GetPosition();
        origin = cell.IsOrigin();
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
