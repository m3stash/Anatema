using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;
    public GameObject player;

    void Start() {
        WorldManager.SetPlayer += SetPlayer;
    }

    private void OnDestroy() {
        WorldManager.SetPlayer -= SetPlayer;
    }

    private void SetPlayer(GameObject player) {
        this.player = player;
    }

    void FixedUpdate() {
        if (!player)
            return;
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + 3, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, 0);
    }

}
