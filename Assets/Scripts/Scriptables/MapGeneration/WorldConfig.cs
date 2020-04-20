using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(fileName = "WorldConfig", menuName = "World/New Configuration")]
public class WorldConfig : ScriptableObject {
    [SerializeField] private string worldName;
    [Header("Main Settings")]
    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;
    [SerializeField] private int chunkSize;
    [Header("Generation Settings")]
    [SerializeField] private MapSettings top;
    [SerializeField] private MapSettings middle;
    [SerializeField] private MapSettings bottom;
    [Header("Only for WorldMap !")]
    [SerializeField] private bool isWorldMap;

    public bool IsWorldMap() {
        return isWorldMap;
    }

    public string GetWorldName() {
        return worldName;
    }

    public int GetWorldWidth() {
        return mapWidth;
    }

    public int GetWorldHeight() {
        return mapHeight;
    }

    public int GetChunkSize() {
        return chunkSize;
    }

    public MapSettings GetWorldSettingsTop() {
        return top;
    }

    public MapSettings GetWorldSettingsBottom() {
        return bottom;
    }

    public MapSettings GetWorldSettingsMiddle() {
        return middle;
    }
}
