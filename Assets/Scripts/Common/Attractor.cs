using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private float attractorSpeed;
    [SerializeField] private Rigidbody2D rb;

    private void Awake() {
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if(this.target && this.rb) {
            Vector2 direction = new Vector2(target.position.x - this.transform.position.x, target.position.y - this.transform.position.y);
            this.rb.velocity = direction.normalized * this.attractorSpeed;
        }
    }

    public void Setup(Transform target, float attractorSpeed) {
        this.target = target;
        this.attractorSpeed = attractorSpeed;

        this.rb = this.GetComponent<Rigidbody2D>();
    }

    public void Reset() {
        this.target = null;
        this.attractorSpeed = 0;
    }
}
