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
    public bool topCollider;
    public bool botttomCollider;
    public bool middleCollider;
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
        Vector2 calculatedTopOffset;
        Vector2 calculatedBottomffset;
        Vector2 calculatedMiddleOffset;
        if (transform.localScale.x < 0) {
            calculatedTopOffset = new Vector2(position.x - 0.3f, position.y + 2.5f);
            calculatedMiddleOffset = new Vector2(position.x - 0.3f, position.y + 1.5f);
            calculatedBottomffset = new Vector2(position.x - 0.3f, position.y + 0.5f);
            newHandOffset = new Vector2(-handOffset.x, handOffset.y);
            newRightOffset = new Vector2(-rightOffset.x, rightOffset.y);
        } else {
            calculatedTopOffset = new Vector2(position.x + 0.3f, position.y + 2.5f);
            calculatedMiddleOffset = new Vector2(position.x + 0.3f, position.y + 1.5f);
            calculatedBottomffset = new Vector2(position.x + 0.3f, position.y + 0.5f);
            newHandOffset = handOffset;
            newRightOffset = rightOffset;
        }
        topCollider = Physics2D.OverlapCircle(calculatedTopOffset, radiusOffset, wallLayer);
        middleCollider = Physics2D.OverlapCircle(calculatedMiddleOffset, radiusOffset, wallLayer);
        botttomCollider = Physics2D.OverlapCircle(calculatedBottomffset, radiusOffset, wallLayer);

        onHandWall = Physics2D.OverlapCircle(position + newHandOffset, radiusOffset, wallLayer);
        onRightWall = Physics2D.OverlapCircle(position + newRightOffset, radiusOffset, wallLayer);
        onGround = Physics2D.OverlapCircle(position + boundOffset, radiusOffset, wallLayer);

    }

    public bool IsTopCollision() {
        return topCollider;
    }

    public bool IsMiddleCollision() {
        return middleCollider;
    }

    public bool IsBottomCollision() {
        return botttomCollider;
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
        Vector2 position = transform.position;
        Vector2 newHandOffset;
        Vector2 newRightOffset;
        Vector2 calculatedTopOffset;
        Vector2 calculatedBottomffset;
        Vector2 calculatedMiddleOffset;
        if (transform.localScale.x < 0) {
            calculatedTopOffset = new Vector2(position.x - 0.3f, position.y + 2.5f);
            calculatedMiddleOffset = new Vector2(position.x - 0.3f, position.y + 1.5f);
            calculatedBottomffset = new Vector2(position.x - 0.3f, position.y + 0.5f);
            newHandOffset = new Vector2(-handOffset.x, handOffset.y);
            newRightOffset = new Vector2(-rightOffset.x, rightOffset.y);
        } else {
            calculatedTopOffset = new Vector2(position.x + 0.3f, position.y + 2.5f);
            calculatedMiddleOffset = new Vector2(position.x + 0.3f, position.y + 1.5f);
            calculatedBottomffset = new Vector2(position.x + 0.3f, position.y + 0.5f);
            newHandOffset = handOffset;
            newRightOffset = rightOffset;
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + newHandOffset, radiusOffset);
        Gizmos.DrawWireSphere((Vector2)transform.position + newRightOffset, radiusOffset);
        Gizmos.DrawWireSphere((Vector2)transform.position + boundOffset, radiusOffset);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(calculatedTopOffset, radiusOffset);
        Gizmos.DrawWireSphere(calculatedMiddleOffset, radiusOffset);
        Gizmos.DrawWireSphere(calculatedBottomffset, radiusOffset);
    }

}
