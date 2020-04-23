using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DynamicLight : MonoBehaviour {

    private int oldPosX;
    private int oldPosY;
    private Renderer render;
    // event
    public delegate void LightEventHandler();
    public static event LightEventHandler RefreshLight;
    private void Start() {
        render = gameObject.GetComponent<Renderer>();
    }

    private void FixedUpdate() {
        if (render && !render.isVisible)
            return;
        int newPosX = (int)transform.position.x;
        // var newPosY = (int)(transform.position.y);
        int newPosY = Mathf.RoundToInt(transform.position.y);

        if (oldPosX != newPosX || oldPosY != newPosY) {
            WorldManager.instance.worldMapDynamicLight[oldPosX, oldPosY] = 0;
            LightService.instance.RecursivDeleteLight(oldPosX, oldPosY, true);
            LightService.instance.RecursivAddNewLight(newPosX, newPosY, 0);
            WorldManager.instance.worldMapDynamicLight[newPosX, newPosY] = 1;
            RefreshLight();
            oldPosX = newPosX;
            oldPosY = newPosY;
        } else {
            WorldManager.instance.worldMapDynamicLight[newPosX, newPosY] = 1;
        }
    }

    private void OnEnable() {
        oldPosX = (int)transform.position.x;
        oldPosY = Mathf.RoundToInt(transform.position.y);
    }

    private void OnDisable() {
        if (WorldManager.instance != null && WorldManager.instance.MapIsInit()) {
            WorldManager.instance.worldMapDynamicLight[oldPosX, oldPosY] = 0;
            LightService.instance.RecursivDeleteLight((int)transform.position.x, Mathf.RoundToInt(transform.position.y), true);
        }
    }
}
