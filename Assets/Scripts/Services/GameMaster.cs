using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapSerialisable {
    public int[,] tilesLightMap;
    public int[,] tilesShadowMap;
    public int[,] tilesWorldMap;
    public int[,] wallTilesMap;
    public int[,] objectsMap;
    public int[,] dynamicLight;
    public int mapWidth;
    public int mapHeight;
    public int chunkSize;
}

public class GameMaster : MonoBehaviour {

    public static GameMaster instance;
    private MapConfig[] mapConfigs;
    private MapSerialisable[] maps;
    public Dictionary<MapType, MapSerialisable> mapDatabase;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        
    }

    public void CreateNewWorlds() {
        MapConfig[] mapConfigs = Resources.LoadAll<MapConfig>("Scriptables/MapSettings/");
        mapDatabase = new Dictionary<MapType, MapSerialisable>();
        for (var i = 0; i < mapConfigs.Length; i++) {
            MapSerialisable newMap = new MapSerialisable();
            GenerateMapService.instance.CreateMaps(mapConfigs[i].GetMapWidth(), mapConfigs[i].GetMapHeight(), mapConfigs[i].GetChunkSize(), newMap);
            mapDatabase.Add(mapConfigs[i].GetMapType(), newMap);
            LevelGenerator.instance.GenerateMap(newMap.tilesWorldMap, newMap.wallTilesMap, mapConfigs[i].GetMapSettingsTop(), mapConfigs[i].GetMapSettingsMiddle(), mapConfigs[i].GetMapSettingsBottom());
        }
    }

}
