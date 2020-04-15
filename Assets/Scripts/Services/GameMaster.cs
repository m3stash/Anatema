using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public class MapSerialisable {
    public int[,] worldMapLight;
    public int[,] worldMapShadow;
    public int[,] worldMapTile;
    public int[,] worldMapWall;
    public int[,] worldMapObject;
    public int[,] worldMapDynamicLight;
    public MapConf mapConf;
}
public class MapSerialisableTest {
    public string worldMapLight;
    public string worldMapShadow;
    public string worldMapTile;
    public string worldMapWall;
    public string worldMapObject;
    public string worldMapDynamicLight;
    public MapConf mapConf;
}

[System.Serializable]
public class MapConf {
    public int mapWidth;
    public int mapHeight;
    public int chunkSize;
    public int seed;
    public MapConf(int mapWidth, int mapHeight, int chunkSize, int seed) {
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        this.chunkSize = chunkSize;
        this.seed = seed;
    }
}

[System.Serializable]
public class SaveData {

    public int saveSlot;
    public DateTime dateLastSave;
    public float gameTime;
    public string currentWorld;

    public SaveData(int saveSlot, DateTime dateLastSave, float gameTime, /*Dictionary<string, MapSerialisable> mapDatabase,*/ string currentWorld) {
        this.saveSlot = saveSlot;
        this.dateLastSave = dateLastSave;
        this.gameTime = gameTime;
        this.currentWorld = currentWorld;
    }
}

public class GameMaster : MonoBehaviour {

    public static GameMaster instance;
    [SerializeField] private GameObject loader;
    public Dictionary<string, MapSerialisable> mapDatabase;
    private float waitTime = 0.3f;
    private bool sceneIsLoad = false;
    private float gameTime;
    private int saveSlot;
    private string savePath = "saves/save_";
    private string currentWorld;
    private MapSerialisable worldData;
    private int mapWidth;
    private int mapHeight;
    private int chunkSize;
    private MapConf mapConf;
    private SaveData[] saves;
    private bool saveInProgress = false;
    private DialogModal dialogModal;
    // private List<string> WorldList;

    [Header("Debug options")]
    [SerializeField] private bool saveWorldToJson;

    private void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        SetCurrentSaves();

    }

    private void Update() {
        if (sceneIsLoad) {
            gameTime += Time.time;
        }
    }
    public void NewGame(int currentSlot) {
        saveSlot = currentSlot;
        SceneManager.LoadSceneAsync("Loader");
        StartCoroutine(StartGeneration(currentSlot));
    }

    public void LoadSave(int currentSlot) {
        saveSlot = currentSlot;
        SceneManager.LoadSceneAsync("Loader");
        StartCoroutine(LoadWorld(currentSlot));
    }
    public void DeleteSave(int slot) {
        FileManager.DeleteFile("/saves/save_" + saveSlot);
    }

    private IEnumerator LoadScene() {
        Loader.instance.SetLoaderValue(100);
        SceneManager.LoadSceneAsync("WorldMap", LoadSceneMode.Single);
        yield return new WaitForSeconds(1);
        loader.SetActive(false);
        sceneIsLoad = true;
    }

    public MapSerialisable GetWorldData() {
        return worldData;
    }

    public string GetSavePath(int saveSlot) {
        return "/" + savePath + saveSlot + "/save_" + saveSlot;
    }

    private void SetCurrentSaves() {
        saves = new SaveData[3];
        saves[0] = FileManager.GetFile<SaveData>(GetSavePath(0) + ".data");
        saves[1] = FileManager.GetFile<SaveData>(GetSavePath(1) + ".data");
        saves[2] = FileManager.GetFile<SaveData>(GetSavePath(2) + ".data");
    }

    public SaveData[] GetSaves() {
        return saves;
    }

    private IEnumerator LoadWorld(int currentSlot) {
        loader.SetActive(true);
        Loader.instance.SetLoaderValue(5);
        Loader.instance.SetCurrentAction("Chargement du monde", "Initialisation");
        yield return new WaitForSeconds(waitTime);
        ItemManager.instance.Init();
        SaveData saveData = FileManager.GetFile<SaveData>(GetSavePath(currentSlot) + ".data");
        currentWorld = saveData.currentWorld;
        gameTime = saveData.gameTime;
        mapConf = FileManager.GetFile<MapConf>(GetSavePath(currentSlot) + "_" + currentWorld + ".infos.data");
        StartCoroutine(LoadMapFiles(saveSlot, currentWorld));
    }

    private IEnumerator StartGeneration(int slotNumber) {
        saveInProgress = true;
        gameTime = 0;
        loader.SetActive(true);
        Loader.instance.SetCurrentAction("Création du monde", "Initialisation");
        yield return new WaitForSeconds(waitTime);
        ItemManager.instance.Init();
        mapDatabase = new Dictionary<string, MapSerialisable>();
        StartCoroutine(StartCreate(slotNumber));
    }
    private void Save(int saveSlot) {
        FileManager.ManageFolder("saves");
        FileManager.ManageFolder(savePath + saveSlot);
        SaveData saveData = new SaveData(saveSlot, DateTime.Now, gameTime, /*mapDatabase,*/ currentWorld);
        FileManager.Save(saveData, GetSavePath(saveSlot) + ".data");
    }

    private IEnumerator StartCreate(int slotNumber) {
        int currentPercent = 5;
        Loader.instance.SetLoaderValue(currentPercent);
        Loader.instance.SetCurrentAction("Création du monde", "Génération des maps");
        yield return new WaitForSeconds(waitTime);
        WorldConfig[] WorldConfigs = Resources.LoadAll<WorldConfig>("Scriptables/WorldsSettings/");
        if (WorldConfigs.Length == 0) {
            Debug.Log("Warning no worldsSettings found");
        }
        for (var i = 0; i < WorldConfigs.Length; i++) {
            int seed = UnityEngine.Random.Range(0, 9999);
            MapSerialisable worldData = new MapSerialisable();
            GenerateMapService.instance.CreateMaps(WorldConfigs[i], worldData, seed);
            // toDo voir pour afficher un message si le map type n'a pas été renseigné correctement ou en doublon!
            string worldName = WorldConfigs[i].GetWorldName();
            if (WorldConfigs[i].IsWorldMap()) {
                currentWorld = worldName;
                this.worldData = worldData;
            } else {
                Debug.Log("Warning no boolean default worldmap set in World Setting !!!");
            }
            FileManager.ManageFolder(savePath + slotNumber);
            // (XXX / WorldConfigs.Length + 1) = représentation en % de ce que représente la création de la map par rapport aux autres tâches
            // int currentPercentCalc = 
            yield return StartCoroutine(GenerateMapService.instance.GenerateMap(worldData, WorldConfigs[i], 20 / WorldConfigs.Length, currentPercent));
            yield return StartCoroutine(GenerateMapService.instance.SaveMapFiles(slotNumber, worldName, worldData, 80 / WorldConfigs.Length, 20));
            MapConf MapConf = new MapConf(WorldConfigs[i].GetWorldWidth(), WorldConfigs[i].GetWorldHeight(), WorldConfigs[i].GetChunkSize(), seed);
            FileManager.Save(MapConf, GetSavePath(slotNumber) + "_" + worldName + ".infos.data");
        }
        // CreateJsonForDebugTool(mapDatabase[0]);
        Save(slotNumber);
        saveInProgress = false;
        yield return StartCoroutine(LoadScene());
    }

    private IEnumerator LoadMapFiles(int saveSlot, string worldName) {
        Loader.instance.SetCurrentAction("Chargement du monde", "Initialisation");
        worldData = new MapSerialisable();
        worldData.mapConf = new MapConf(mapConf.mapWidth, mapConf.mapHeight, mapConf.chunkSize, mapConf.seed);
        Loader.instance.SetLoaderValue(10);
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(GenerateMapService.instance.LoadMapFromFiles(worldData, saveSlot, worldName, 90, 10));
        yield return StartCoroutine(LoadScene());
    }

    private void OnDestroy() {
        // if stop during file creation prossessing then => delete folder save!
        if (saveInProgress) {
            DeleteSave(saveSlot);
        }
    }


    private void CreateJsonForDebugTool(MapSerialisable newMap) {
        // Create Json for html canvas map render Debug tool
        if (saveWorldToJson) {
            FileManager.SaveToJson(new ConvertWorldMapToJson(newMap.worldMapTile, newMap.worldMapWall, newMap.worldMapObject), "worldMap");
        }
    }
}
