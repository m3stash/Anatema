﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateLightForObjects : MonoBehaviour {

    [SerializeField] bool hasChildSpriteRender;
    private Color color;
    private Renderer render;
    private Renderer []renders;
    private int oldShadow;
    private int oldLight;

    private void Start() {
        if (hasChildSpriteRender) {
            renders = gameObject.GetComponentsInChildren<Renderer>();
        }
        render = gameObject.GetComponent<Renderer>();
    }

    void Update() {
        /*if (!render.isVisible)
            return;*/
        int x = (int)transform.position.x;
        // int y = (int)transform.position.y;
        int y = Mathf.RoundToInt(transform.position.y);
        int newShadow = WorldManager.instance.worldMapShadow[x, y] + CycleDay.GetIntensity();
        int newLight = WorldManager.instance.worldMapLight[x, y];
        float l;
        float oldL;
        if (newLight < newShadow) {
            l = 1 - newLight * 0.01f;
            oldL = 1 - oldLight * 0.01f;
        } else {
            l = 1 - newShadow * 0.01f;
            oldL = 1 - oldShadow * 0.01f;
        }
        
        if (l > 1 || oldL > 1)
            return;
        if (hasChildSpriteRender) {
            for(var i = 0; i < renders.Length; i++) {
                renders[i].material.color = new Color(l, l, l, 1);
            }
        } else {
            render.material.color = new Color(l, l, l, 1);
        }

        oldShadow = newShadow;
        oldLight = newLight;
    }
}
