using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    [SerializeField] private GameObject eyeRaycast;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Vector2 rightOffset;
    [SerializeField] private Vector2 boundOffset;
    [SerializeField] private Vector2 handOffset;
    private float radiusOffset = 0.05f;
    public bool onRightWall;
    public bool onGround;
    public bool canGrab;
    public bool onHandWall;
    private Vector2 direction;

    void Update() {

        direction = transform.localScale.x < 0 ? -Vector2.right : Vector2.right;
        Vector2 position = transform.position;

        RaycastHit2D eyeHit = Physics2D.Raycast(eyeRaycast.transform.position, direction, 0.8f, wallLayer);
        Debug.DrawRay(eyeRaycast.transform.position, direction, Color.red);
        if (eyeHit.collider == null) {
            canGrab = true;
        } else {
            canGrab = false;
        }

        Vector2 newHandOffset;
        Vector2 newRightOffset;
        if (transform.localScale.x < 0) {
            newHandOffset = new Vector2(-handOffset.x, handOffset.y);
            newRightOffset = new Vector2(-rightOffset.x, rightOffset.y);
        } else {
            newHandOffset = handOffset;
            newRightOffset = rightOffset;
        }
        
        onHandWall = Physics2D.OverlapCircle(position + newHandOffset, radiusOffset, wallLayer);
        onRightWall = Physics2D.OverlapCircle(position + newRightOffset, radiusOffset, wallLayer);
        onGround = Physics2D.OverlapCircle(position + boundOffset, radiusOffset, wallLayer);

    }

    public bool OnGround() {
        return onGround;
    }

    public bool CanGrab() {
        return canGrab;
    }
    public bool OnHandWall() {
        return onHandWall;
    }

    public bool OnRightWall() {
        return onRightWall;
    }


    private void OnDrawGizmos() {
        Vector2 newHandOffset;
        Vector2 newRightOffset;
        if (transform.localScale.x < 0) {
            newHandOffset = new Vector2(-handOffset.x, handOffset.y);
            newRightOffset = new Vector2(-rightOffset.x, rightOffset.y);
        } else {
            newHandOffset = handOffset;
            newRightOffset = rightOffset;
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + newHandOffset, radiusOffset);
        Gizmos.DrawWireSphere((Vector2)transform.position + newRightOffset, radiusOffset);
        Gizmos.DrawWireSphere((Vector2)transform.position + boundOffset, radiusOffset);
    }

}
