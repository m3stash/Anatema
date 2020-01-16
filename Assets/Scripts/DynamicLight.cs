using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLight : MonoBehaviour {

    private int oldPosX;
    private int oldPosY;
    private int newPosX;
    private int newPosY;
    private void FixedUpdate() {
        // dynamic light
        var newPosX = (int)transform.position.x;
        var newPosY = (int)transform.position.y;
        if (oldPosX != newPosX || oldPosY != newPosY) {
            Debug.Log("oldPosX " + oldPosX);
            Debug.Log("oldPosX " + oldPosX);
            Debug.Log("oldPosX " + newPosX);
            Debug.Log("oldPosX " + newPosY);
            // LightService.RecursivDeleteLight(oldPosX, oldPosY, true);
            // LightService.RecursivAddNewLight(newPosX, newPosY, 0);
        }
        oldPosX = newPosX;
        oldPosY = newPosY;
    }

    private void OnEnable() {
        oldPosX = (int)transform.position.x;
        oldPosY = (int)transform.position.x;
    }
}
