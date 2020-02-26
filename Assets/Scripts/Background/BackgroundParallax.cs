using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BackgroundParallax : MonoBehaviour {

    public float speed;
    public bool moveAxeY;
    public bool invertAxeY;
    public float ySpeed;
    Vector3 newPos;

    void Update() {
        newPos = BackgroundParallaxManager.GetNewVector3(speed, moveAxeY, transform, invertAxeY, ySpeed);
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
    }
}

