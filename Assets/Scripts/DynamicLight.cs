﻿using System.Collections;
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
        if (!render.isVisible)
            return;
        int newPosX = (int)transform.position.x;
        // var newPosY = (int)(transform.position.y);
        int newPosY = Mathf.RoundToInt(transform.position.y);

        if (oldPosX != newPosX || oldPosY != newPosY) {
            WorldManager.dynamicLight[oldPosX, oldPosY] = 0;
            LightService.RecursivDeleteLight(oldPosX, oldPosY, true);
            LightService.RecursivAddNewLight(newPosX, newPosY, 0);
            WorldManager.dynamicLight[newPosX, newPosY] = 1;
            RefreshLight();
            oldPosX = newPosX;
            oldPosY = newPosY;
        } else {
            WorldManager.dynamicLight[newPosX, newPosY] = 1;
        }
    }

    private void OnEnable() {
        oldPosX = (int)transform.position.x;
        oldPosY = (int)transform.position.x;
    }

    private void OnDisable() {
        if (WorldManager.instance != null && WorldManager.instance.MapIsInit()) {
            WorldManager.dynamicLight[oldPosX, oldPosY] = 0;
            LightService.RecursivDeleteLight((int)transform.position.x, Mathf.RoundToInt(transform.position.y), true);
        }
    }
}
