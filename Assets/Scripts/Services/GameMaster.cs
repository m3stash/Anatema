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
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        ItemManager.instance.Init();
    }

    public MapSerialisable GetMapDatabaseByMapType(MapType mapype) {
        MapSerialisable mapConf;
        if(mapDatabase.TryGetValue(mapype, out mapConf)) {
            return mapConf;
        }
        return null;
    }

    public void CreateNewWorlds() {
        MapConfig[] mapConfigs = Resources.LoadAll<MapConfig>("Scriptables/MapSettings/");
        mapDatabase = new Dictionary<MapType, MapSerialisable>();
        for (var i = 0; i < mapConfigs.Length; i++) {
            MapSerialisable newMap = new MapSerialisable();
            GenerateMapService.instance.CreateMaps(mapConfigs[i].GetMapWidth(), mapConfigs[i].GetMapHeight(), mapConfigs[i].GetChunkSize(), newMap);
            mapDatabase.Add(mapConfigs[i].GetMapType(), newMap);
            LevelGenerator.instance.GenerateMap(newMap, mapConfigs[i].GetMapSettingsTop(), mapConfigs[i].GetMapSettingsMiddle(), mapConfigs[i].GetMapSettingsBottom());
        }
    }

}
