using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] public GameObject buildingLoader;

    [Header("Debug options")]
    [SerializeField] private bool saveWorldToJson;

    private void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public MapSerialisable GetMapDatabaseByMapType(MapType mapype) {
        if (mapDatabase.TryGetValue(mapype, out MapSerialisable mapConf)) {
            return mapConf;
        }
        return null;
    }

    public void NewGame() {
        MapConfig[] mapConfigs = Resources.LoadAll<MapConfig>("Scriptables/MapSettings/");
        mapDatabase = new Dictionary<MapType, MapSerialisable>();
        MapSerialisable newMap = new MapSerialisable();
        ItemManager.instance.Init();
        for (var i = 0; i < mapConfigs.Length; i++) {
            GenerateMapService.instance.CreateMaps(mapConfigs[i], newMap);
            mapDatabase.Add(mapConfigs[i].GetMapType(), newMap);
            LevelGenerator.instance.GenerateMap(newMap, mapConfigs[i]);
            LevelGenerator.instance.GenerateWorldLight(newMap.tilesShadowMap, newMap.tilesWorldMap, newMap.wallTilesMap);
        }
        // Create Json for html canvas map render Debug tool
        if (saveWorldToJson) {
            FileManager.SaveToJson(new ConvertWorldMapToJson(newMap.tilesWorldMap, newMap.wallTilesMap, newMap.objectsMap), "worldMap");
        }
        // toDo change name of "Demo" to "World"
        SceneManager.LoadScene("Demo", LoadSceneMode.Single);
    }

}
