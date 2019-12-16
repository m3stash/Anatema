using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;


public class CycleDay : MonoBehaviour {
    [SerializeField] private Light AmbiantLight;
    [SerializeField] private int startHour = 8;
    [SerializeField] private int durationDayOnMinute;
    [SerializeField] private int currentHour = 0;
    [SerializeField] private GameObject sphere;
    [SerializeField] private GameObject[] moon;
    [SerializeField] private GameObject sun;
    private readonly int secondInDay = 86400;
    private static int timerDay = 0;
    private int convertedInRealSecond;
    private static int intensity = 0;
    private Tilemap tileLightMap;
    private Color32 newColor;
    private float AmbiantIntensity;
    private int lastHour = -1;
    private Light[] moonLight;
    private float moonIntensity;
    private Material[] moonMat;
    // event
    public delegate void CycleDayHandler(int intenisty);
    public static event CycleDayHandler RefreshIntensity;

    void Start() {
        moonMat = new Material[2];
        moonLight = new Light[2];
        moonLight[0] = moon[0].GetComponentInChildren<Light>();
        moonMat[0] = moon[0].GetComponent<Renderer>().material;
        moonLight[1] = moon[1].GetComponentInChildren<Light>();
        moonMat[1] = moon[1].GetComponent<Renderer>().material;
        tileLightMap = GetComponentInChildren<Tilemap>();
        timerDay = startHour * 60 * 60;
        ConvertTime();
        StartCoroutine(DayNightCycle());
    }
    public static int GetIntensity() {
        return intensity;
    }
    public void ConvertTime() {
        int durationOnSecond = durationDayOnMinute * 60;
        convertedInRealSecond = secondInDay / durationOnSecond;
    }
    public static int GetNbrSecondOfDay() {
        return timerDay;
    }
    void Update() {
        AmbiantLight.color = Color.Lerp(AmbiantLight.color, newColor, Time.deltaTime);
        AmbiantLight.intensity = Mathf.Lerp(AmbiantLight.intensity, AmbiantIntensity, Time.deltaTime);
        moonMat[0].color = Color.Lerp(moonMat[0].color, new Color(moonMat[0].color.a, moonMat[0].color.b, moonMat[1].color.g, moonIntensity), Time.deltaTime);
        moonLight[0].intensity = Mathf.Lerp(moonLight[0].intensity, moonIntensity, Time.deltaTime);
        moonMat[1].color = Color.Lerp(moonMat[1].color, new Color(moonMat[1].color.a, moonMat[1].color.b, moonMat[1].color.g, moonIntensity), Time.deltaTime);
        moonLight[1].intensity = Mathf.Lerp(moonLight[1].intensity, moonIntensity, Time.deltaTime);
    }
    private IEnumerator DayNightCycle() {
        while (true) {
            var hour = timerDay / 60 / 60;
            if (hour == 24) {
                currentHour = 0;
                timerDay = 0;
            } else {
                currentHour = hour;
            }
            // currentHour = 12;
            if (lastHour != currentHour) {
                SetIntensity(currentHour);
                SetGlobalColor(currentHour);
                lastHour = currentHour;
                if (RefreshIntensity != null) {
                    RefreshIntensity(intensity);
                }
            }
            timerDay += convertedInRealSecond;
            yield return new WaitForSeconds(1);
        }
    }
    private void SetIntensity(int currentHour) {
        // On divise 1 / 24 afin d'avoir l'intensité par heure = 0.01
        switch (currentHour) {
            case 0:
                intensity = 70;
                AmbiantIntensity = 0.10f;
                moonIntensity = 1.5f;
                break;
            case 1:
                intensity = 70;
                AmbiantIntensity = 0.10f;
                moonIntensity = 1.5f;
                break;
            case 2:
                intensity = 70;
                AmbiantIntensity = 0.10f;
                moonIntensity = 1.5f;
                break;
            case 3:
                intensity = 70;
                AmbiantIntensity = 0.10f;
                moonIntensity = 1.2f;
                break;
            case 4:
                intensity = 60;
                AmbiantIntensity = 0.13f;
                moonIntensity = 0.9f;
                break;
            case 5:
                intensity = 50;
                AmbiantIntensity = 0.26f;
                moonIntensity = 0.7f;
                break;
            case 6:
                intensity = 40;
                AmbiantIntensity = 0.33f;
                moonIntensity = 0.4f;
                break;
            case 7:
                intensity = 30;
                AmbiantIntensity = 0.46f;
                moonIntensity = 0.3f;
                break;
            case 8:
                intensity = 20;
                AmbiantIntensity = 0.59f;
                moonIntensity = 0;
                break;
            case 9:
                intensity = 10;
                AmbiantIntensity = 0.72f;
                moonIntensity = 0;
                break;
            case 17:
                intensity = 10;
                AmbiantIntensity = 0.85f;
                moonIntensity = 0;
                break;
            case 18:
                intensity = 20;
                AmbiantIntensity = 0.72f;
                moonIntensity = 0;
                break;
            case 19:
                intensity = 30;
                AmbiantIntensity = 0.59f;
                moonIntensity = 0.3f;
                break;
            case 20:
                intensity = 40;
                AmbiantIntensity = 0.46f;
                moonIntensity = 0.6f;
                break;
            case 21:
                intensity = 50;
                AmbiantIntensity = 0.33f;
                break;
            case 22:
                intensity = 60;
                AmbiantIntensity = 0.26f;
                moonIntensity = 0.9f;
                break;
            case 23:
                intensity = 70;
                AmbiantIntensity = 0.13f;
                moonIntensity = 1.2f;
                break;
            case 24:
                intensity = 70;
                AmbiantIntensity = 0.10f;
                moonIntensity = 1.5f;
                break;
            default:
                intensity = 0;
                AmbiantIntensity = 1;
                moonIntensity = 0f;
                break;
        }
    }

    private void SetGlobalColor(int currentHour) {
        if (currentHour > 22 && currentHour <= 24 || currentHour >= 0 && currentHour < 4) {
            // newColor = new Color32(0, 0, 0, 255);
            newColor = new Color32(255, 255, 255, 255);
            // Debug.Log("nuit");
        } else if (currentHour >= 4 && currentHour < 7) {
            newColor = new Color32(135, 135, 135, 255);
            // Debug.Log("aube");
        } else if (currentHour >= 7 && currentHour < 10) {
            newColor = new Color32(212, 212, 212, 255);
        } else if (currentHour >= 10 && currentHour < 16) {
            newColor = new Color32(255, 255, 255, 255);
            // Debug.Log("journée");
        } else if (currentHour >= 16 && currentHour < 19) {
            newColor = new Color32(255, 100, 100, 255);
            // Debug.Log("fin journée");
        } else {
            // >= 19 && < 22
            newColor = new Color32(120, 120, 120, 255);
            // Debug.Log("crepuscule");
        }
    }


}

