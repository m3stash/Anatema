using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DynamicLight : MonoBehaviour {

    private int oldPosX;
    private int oldPosY;
    // event
    public delegate void LightEventHandler();
    public static event LightEventHandler RefreshLight;

    private void FixedUpdate() {
        var newPosX = (int)transform.position.x;
        var newPosY = (int)transform.position.y;
        if(oldPosX != newPosX || oldPosY != newPosY) {
            LightService.RecursivDeleteLight(oldPosX, oldPosY, true);
            LightService.RecursivAddNewLight(newPosX, newPosY, 0);
            RefreshLight(); // toDo trouver le bug quand on creuse avec la remise a zéro de la light si changement d'heure en meme temps
        }
        oldPosX = newPosX;
        oldPosY = newPosY;
    }

    private void OnEnable() {
        oldPosX = (int)transform.position.x;
        oldPosY = (int)transform.position.x;
    }

    private void OnDisable() {
        LightService.RecursivDeleteLight(oldPosX, oldPosY, true);
    }
}
