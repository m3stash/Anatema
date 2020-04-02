using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum MapType {
    WORLDMAP,
}

[CreateAssetMenu(fileName = "MapConfig", menuName = "Map/New Configuration")]
public class MapConfig : ScriptableObject {
    [SerializeField] private MapType mapType;
    [Header("Main Settings")]
    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;
    [SerializeField] private int chunkSize;
    [Header("Generation Settings")]
    [SerializeField] private MapSettings top;
    [SerializeField] private MapSettings middle;
    [SerializeField] private MapSettings bottom;
    private int mapSeed;

    public MapType GetMapType() {
        return mapType;
    }
    public void SetMapSeed(int mapSeed) {
        this.mapSeed = mapSeed;
    }
    public int GetMapSeed() {
        return mapSeed;
    }

    public int GetMapWidth() {
        return mapWidth;
    }

    public int GetMapHeight() {
        return mapHeight;
    }

    public int GetChunkSize() {
        return chunkSize;
    }

    public MapSettings GetMapSettingsTop() {
        return top;
    }

    public MapSettings GetMapSettingsBottom() {
        return bottom;
    }

    public MapSettings GetMapSettingsMiddle() {
        return middle;
    }
}
