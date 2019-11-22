using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;


public class CycleDay : MonoBehaviour {
    [SerializeField] Light sun;
    public int startHour = 8;
    public int durationDayOnMinute;
    [SerializeField]private int currentHour = 0;
    private readonly int secondInDay = 86400;
    private static int timerDay = 0;
    private int convertedInRealSecond;
    private static int intensity = 0;
    private Tilemap tileLightMap;
    private Color32 newColor;
    private float sunIntensity;
    // event
    public delegate void CycleDayHandler(int intenisty);
    public static event CycleDayHandler RefreshIntensity;
    public int lastHour = -1;

    void Start() {
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
        sun.color = Color.Lerp(sun.color, newColor, Time.deltaTime);
        sun.intensity = Mathf.Lerp(sun.intensity, sunIntensity, Time.deltaTime);
    }
    private IEnumerator DayNightCycle() {
        while (true) {
            /*if(currentHour > 17 && currentHour <= 24 || currentHour >= 0 && currentHour < 6) {
            } else {
            }*/
            currentHour = timerDay / 60 / 60;
            SetIntensity(currentHour);
            SetGlobalColor(currentHour);
            if (lastHour != currentHour) {
                lastHour = currentHour;
                RefreshIntensity(intensity);
            }
            timerDay = currentHour == 24 ? 0 : timerDay += convertedInRealSecond;
            yield return new WaitForSeconds(1);
        }
    }
    private void SetIntensity(int currentHour) {
        // On divise 1 / 24 afin d'avoir l'intensité par heure = 0.01
        switch (currentHour) {
            case 0:
                intensity = 70;
                sunIntensity = 0f;
                // intensity = 80;
                break;
            case 1:
                intensity = 70;
                sunIntensity = 0f;
                // intensity = 80;
                break;
            case 2:
                intensity = 70;
                sunIntensity = 0f;
                // intensity = 80;
                break;
            case 3:
                intensity = 70;
                sunIntensity = 0f;
                break;
            case 4:
                intensity = 60;
                sunIntensity = 0.13f;
                break;
            case 5:
                intensity = 50;
                sunIntensity = 0.26f;
                break;
            case 6:
                intensity = 40;
                sunIntensity = 0.33f;
                break;
            case 7:
                intensity = 30;
                sunIntensity = 0.46f;
                break;
            case 8:
                intensity = 20;
                sunIntensity = 0.59f;
                break;
            case 9:
                intensity = 10;
                sunIntensity = 0.72f;
                break;
            case 17:
                intensity = 10;
                sunIntensity = 0.85f;
                break;
            case 18:
                intensity = 20;
                sunIntensity = 0.72f;
                break;
            case 19:
                intensity = 30;
                sunIntensity = 0.59f;
                break;
            case 20:
                intensity = 40;
                sunIntensity = 0.46f;
                break;
            case 21:
                intensity = 50;
                sunIntensity = 0.33f;
                break;
            case 22:
                intensity = 60;
                sunIntensity = 0.26f;
                break;
            case 23:
                intensity = 70;
                sunIntensity = 0.13f;
                break;
            case 24:
                intensity = 70;
                sunIntensity = 0f;
                // intensity = 80;
                break;
            default:
                intensity = 0;
                sunIntensity = 1;
                break;
        }
    }

    private void SetGlobalColor(int currentHour) {
        if (currentHour >= 22 && currentHour <= 24 || currentHour >= 0 && currentHour < 4) {
            newColor = new Color32(0, 0, 0, 255);
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
            newColor = new Color32(60, 50, 50, 255);
            // Debug.Log("crepuscule");
        }
    }


}

