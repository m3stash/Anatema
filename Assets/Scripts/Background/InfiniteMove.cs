using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMove : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.right * this.speed * Time.deltaTime);
    }

    public void SetSpeed(float value) {
        this.speed = value;
    }
}
