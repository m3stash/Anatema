using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsGenerator : MonoBehaviour {
    [SerializeField] private ParticleSystem starParticle;
    [SerializeField] private ParticleSystem starParticle2;
    [SerializeField] private int quantity;
    [SerializeField] private float range;

    // Start is called before the first frame update
    void Start() {
        this.GenerateStars(starParticle);
        this.GenerateStars(starParticle2);
    }

    private void Update() {
        transform.Rotate(Vector3.up * 1.5f * Time.deltaTime);

    }

    private void GenerateStars(ParticleSystem system) {
        ParticleSystem.Particle[] points = new ParticleSystem.Particle[this.quantity];
        for (int i = 0; i < points.Length; i++) {
            points[i].position = Random.insideUnitSphere * this.range;
            points[i].startSize = Random.Range(0.05f, 0.1f);
            points[i].startColor = new Color(1, 1, 1, 1);
        }
        system.SetParticles(points, points.Length);
    }
}
