using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallaxManager : MonoBehaviour {

    //public float speed;
    private GameObject player;
    private GameObject camera;
    private float oldPosY;
    private float oldPosX;
    private static float x;
    private static float y;
    private Rigidbody2D rg2d;
    public static BackgroundParallaxManager instance;

    private void OnEnable() {
        WorldManager.GetPlayer += SetPlayer;
    }

    private void OnDestroy() {
        WorldManager.GetPlayer -= SetPlayer;
    }
    private void SetPlayer(GameObject player) {
        // var cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camera = Camera.main.gameObject;
        this.player = player;
        rg2d = player.GetComponent<Rigidbody2D>();
        oldPosY = player.transform.position.y;
        oldPosX = player.transform.position.x;
    }

    public static Vector3 GetNewVector3(float speed, bool moveAxeY, Transform t, bool invertAxeY, float ySpeed) {
        var newY = moveAxeY ? t.position.y - y : t.position.y;
        if (invertAxeY) {
            newY = moveAxeY ? t.position.y - (y * ySpeed) : t.position.y;
        }
        return new Vector3(t.position.x + x * speed, newY, t.position.z);
    }

    public void Update() {
        if (!player)
            return;
        x = 0f;
        y = 0f;
        if (rg2d.velocity.y != 0) {
            float newDist = camera.transform.position.y - oldPosY;
            y = newDist;
            oldPosY = camera.transform.position.y;
        }
        bool ifEqualX = Mathf.Approximately(camera.transform.position.x, oldPosX);
        if (!ifEqualX) {
            float newDist = camera.transform.position.x - oldPosX;
            x = -newDist;
            oldPosX = camera.transform.position.x;
        }
    }

}
