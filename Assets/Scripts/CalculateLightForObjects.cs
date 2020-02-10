using System.Collections;
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
            if (newLight <= newShadow && newLight < 100) {
                l = 1 - newLight * 0.01f;
            } else {
                l = 1 - newShadow * 0.01f;
            }
            render.material.color = new Color(l, l, l, 1);
            oldShadow = newShadow;
            oldLight = newLight;
        }
    }
}
