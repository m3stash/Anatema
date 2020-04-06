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
    public delegate void Action<in T>(T obj);
}

public class GameMaster : MonoBehaviour {

    public static GameMaster instance;
    [SerializeField] private GameObject loader;
    public Dictionary<MapType, MapSerialisable> mapDatabase;
    private float waitTime = 0.3f;

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
        SceneManager.LoadSceneAsync("Loader");
        StartCoroutine(StartGeneration());
    }


    private IEnumerator StartGeneration() {
        loader.SetActive(true);
        Loader.instance.SetCurrentAction("Création du monde", "Initialisation");
        yield return new WaitForSeconds(waitTime);
        ItemManager.instance.Init();
        mapDatabase = new Dictionary<MapType, MapSerialisable>();
        StartCoroutine(StartCreate());
    }

    private IEnumerator StartCreate() {
        int currentPercent = 5;
        int createMapValuePercent = 80; // représentation en % de ce que représente la création de la map par rapport aux autres tâches
        Loader.instance.SetLoaderValue(currentPercent);
        Loader.instance.SetCurrentAction("Création du monde", "Génération des maps");
        yield return new WaitForSeconds(waitTime);
        MapConfig[] mapConfigs = Resources.LoadAll<MapConfig>("Scriptables/MapSettings/");
        for (var i = 0; i < mapConfigs.Length; i++) {
            mapConfigs[i].SetMapSeed(UnityEngine.Random.Range(0, 9999));
            MapSerialisable newMap = new MapSerialisable();
            GenerateMapService.instance.CreateMaps(mapConfigs[i], newMap);
            // toDo voir pour afficher un message si le map type n'a pas été renseigné correctement ou en doublon!
            mapDatabase.Add(mapConfigs[i].GetMapType(), newMap);
            yield return StartCoroutine(GenerateMap(newMap, mapConfigs[i], createMapValuePercent / mapConfigs.Length + 1, currentPercent));
        }
        // CreateJsonForDebugTool(mapDatabase[0]);
        yield return StartCoroutine(LoadScene());
    }

    private void CreateJsonForDebugTool(MapSerialisable newMap) {
        // Create Json for html canvas map render Debug tool
        if (saveWorldToJson) {
            FileManager.SaveToJson(new ConvertWorldMapToJson(newMap.tilesWorldMap, newMap.wallTilesMap, newMap.objectsMap), "worldMap");
        }
    }

    private IEnumerator LoadScene() {
        Loader.instance.SetLoaderValue(100);
        SceneManager.LoadSceneAsync("WorldMap", LoadSceneMode.Single);
        yield return new WaitForSeconds(1);
        loader.SetActive(false);
    }

    private IEnumerator GenerateMap(MapSerialisable newMap, MapConfig mapConfig, int percentValue, int currentPercent) {
        // toDo voir à creer une liste d'Action afin de boucler dessus pour dynamiser numberOfTask par son length !
        int numberOfTask = percentValue / 8;
        yield return StartCoroutine(GenerateMapService.instance.GenerateTerrain(newMap, mapConfig, currentPercent += numberOfTask));
        yield return StartCoroutine(GenerateMapService.instance.GenerateCaves(newMap, mapConfig, currentPercent += numberOfTask));
        yield return StartCoroutine(GenerateMapService.instance.GenerateTunnels(newMap, mapConfig, currentPercent += numberOfTask));
        yield return StartCoroutine(GenerateMapService.instance.GenerateIrons(newMap, mapConfig, currentPercent += numberOfTask));
        yield return StartCoroutine(GenerateMapService.instance.GenerateTrees(newMap, mapConfig, currentPercent += numberOfTask));
        yield return StartCoroutine(GenerateMapService.instance.GenerateGrassTiles(newMap, mapConfig, currentPercent += numberOfTask));
        yield return StartCoroutine(GenerateMapService.instance.GenerateGrasses(newMap, mapConfig, currentPercent += numberOfTask));
        yield return StartCoroutine(GenerateMapService.instance.GenerateLightMap(newMap, mapConfig, currentPercent += numberOfTask));
        yield return new WaitForSeconds(waitTime);
    }

}
