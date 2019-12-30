using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// It's just a temporary script used to test the item system
/// </summary>
public class ScreenMouseRay : MonoBehaviour
{
    private new Camera camera;

    // Start is called before the first frame update
    void Start() {
        this.camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(this.camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit) {
                BlockItem block = hit.transform.GetComponentInChildren<BlockItem>();

                // If clicked object is a block item and he is ACTIVE => hit it
                if (block && block.GetStatus().Equals(ItemStatus.ACTIVE)) {
                    block.Hit(30);
                    Debug.Log("Target: " + hit.collider.name);
                }
            }
        }
    }
}
