using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum MapType {
    WORLDMAP
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

    public MapType GetMapType() {
        return mapType;
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
        return middle;
    }

    public MapSettings GetMapSettingsMiddle() {
        return bottom;
    }
}
