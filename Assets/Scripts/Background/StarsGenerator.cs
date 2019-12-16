using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsGenerator : MonoBehaviour {
    [SerializeField] private ParticleSystem starParticle;
    [SerializeField] private ParticleSystem starParticle2;
    [SerializeField] private int quantity;
    [SerializeField] private float range;

    private void OnEnable() {
        CycleDay.RefreshIntensity += ChangeZindex;
        this.GenerateStars(starParticle);
        this.GenerateStars(starParticle2);
    }

    private void OnDisable() {
        CycleDay.RefreshIntensity -= ChangeZindex;
    }

    private void ChangeZindex(int intensity) {
        switch (intensity) {
            case 0:
                transform.position = new Vector3(transform.position.x, transform.position.y, -18);
                break;
            case 10:
                transform.position = new Vector3(transform.position.x, transform.position.y, -18);
                break;
            case 20:
                transform.position = new Vector3(transform.position.x, transform.position.y, -8);
                break;
            case 30:
                transform.position = new Vector3(transform.position.x, transform.position.y, -14);
                break;
            case 40:
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                break;
            case 50:
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                break;
            case 60:
                transform.position = new Vector3(transform.position.x, transform.position.y, 2);
                break;
            case 70:
                transform.position = new Vector3(transform.position.x, transform.position.y, 2);
                break;
        }
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
