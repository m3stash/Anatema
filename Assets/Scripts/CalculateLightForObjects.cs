﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateLightForObjects : MonoBehaviour {

    private Color color;
    private Renderer render;
    private int oldShadow;
    private int oldLight;

    private void Start() {
        render = gameObject.GetComponent<Renderer>();
    }

    void Update() {
        if (!render.isVisible)
            return;
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        int newShadow = WorldManager.tilesShadowMap[x, y] + CycleDay.GetIntensity();
        int newLight = WorldManager.tilesLightMap[x, y];
        if (oldShadow != newShadow || oldLight != newLight) {
            float l;
            float oldL;
            if (newLight <= newShadow) {
                l = 1 - newLight * 0.01f;
                oldL = 1 - oldLight * 0.01f;
            } else {
                l = 1 - newShadow * 0.01f;
                oldL = 1 - oldShadow * 0.01f;
            }
            // Debug.Log("Light "+l);
            // Debug.Log("oldL " + oldL);
            render.material.color = Color.Lerp(new Color(oldL, oldL, oldL, 1), new Color(l, l, l, 1), Time.time);
            oldShadow = newShadow;
            oldLight = newLight;
        }
    }
}
