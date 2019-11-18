using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;


public class CycleDay : MonoBehaviour {

    public int startHour = 8;
    public int durationDayOnMinute;
    [SerializeField]
    private int currentHour = 0;
    private readonly int secondInDay = 86400;
    private int timerDay = 0;
    private int convertedInRealSecond;
    private static int intensity = 0;
    private Tilemap tileLightMap;
    // event
    public delegate void CycleDayHandler(int intenisty);
    public static event CycleDayHandler RefreshIntensity;
    public int lastHour = -1;

    void Start() {
        tileLightMap = GetComponentInChildren<Tilemap>();
        timerDay = startHour * 60 * 60;
        // sky = GameObject.FindGameObjectWithTag("Sky").GetComponent<SpriteRenderer>();
        ConvertTime();
        StartCoroutine(DayNightCicle());
    }
    public static int GetIntensity() {
        return intensity;
    }
    public void ConvertTime() {
        int durationOnSecond = durationDayOnMinute * 60;
        convertedInRealSecond = secondInDay / durationOnSecond;
    }
    private IEnumerator DayNightCicle() {
        while (true) {
            currentHour = timerDay / 60 / 60;
            SetIntensity(currentHour);
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
                intensity = 80;
                break;
            case 1:
                intensity = 80;
                break;
            case 2:
                intensity = 80;
                break;
            case 3:
                intensity = 70;
                break;
            case 4:
                intensity = 60;
                break;
            case 5:
                intensity = 50;
                break;
            case 6:
                intensity = 40;
                break;
            case 7:
                intensity = 30;
                break;
            case 8:
                intensity = 20;
                break;
            case 9:
                intensity = 10;
                break;
            case 17:
                intensity = 10;
                break;
            case 18:
                intensity = 20;
                break;
            case 19:
                intensity = 30;
                break;
            case 20:
                intensity = 40;
                break;
            case 21:
                intensity = 50;
                break;
            case 22:
                intensity = 60;
                break;
            case 23:
                intensity = 70;
                break;
            case 24:
                intensity = 80;
                break;
            default:
                intensity = 0;
                break;
        }
    }

}

