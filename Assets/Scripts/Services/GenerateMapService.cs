using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public enum WORLDMAPTYPES {
    WORLDMAPLIGHT,
    WORLDMAPSHADOW,
    WORLDMAPTILE,
    WORLDMAPWALL,
    WORLDMAPOBJECT,
    WORLDMAPDYNAMICLIGHT
}

public class GenerateMapService : MonoBehaviour {

    public static GenerateMapService instance;
    private float waitTime = 0.3f;
    private Dictionary<WORLDMAPTYPES, string> worldMapTypesDico;

    private void Awake() {
        instance = this;
        worldMapTypesDico = new Dictionary<WORLDMAPTYPES, string>();
        worldMapTypesDico.Add(WORLDMAPTYPES.WORLDMAPLIGHT, "worldMapLight");
        worldMapTypesDico.Add(WORLDMAPTYPES.WORLDMAPSHADOW, "worldMapShadow");
        worldMapTypesDico.Add(WORLDMAPTYPES.WORLDMAPTILE, "worldMapTile");
        worldMapTypesDico.Add(WORLDMAPTYPES.WORLDMAPWALL, "worldMapWall");
        worldMapTypesDico.Add(WORLDMAPTYPES.WORLDMAPOBJECT, "worldMapObject");
        worldMapTypesDico.Add(WORLDMAPTYPES.WORLDMAPDYNAMICLIGHT, "worldMapDynamicLight");
    }

    public void Generate3Tunnels(MapSerialisable map, WorldConfig worldConfig) {
        // 3 tunnels
        MapSettings middleMapSettings = worldConfig.GetWorldSettingsMiddle();
        var startPosX = map.worldMapTile.GetUpperBound(0) / 6;
        map.worldMapTile = MapFunctions.DirectionalTunnel(map.worldMapTile, middleMapSettings.minPathWidth, middleMapSettings.maxPathWidth,
            middleMapSettings.maxPathChange, middleMapSettings.roughness, middleMapSettings.windyness, startPosX);
        var startPosX2 = map.worldMapTile.GetUpperBound(0) / 2;
        map.worldMapTile = MapFunctions.DirectionalTunnel(map.worldMapTile, middleMapSettings.minPathWidth, middleMapSettings.maxPathWidth,
            middleMapSettings.maxPathChange, middleMapSettings.roughness, middleMapSettings.windyness, startPosX2);
        var startPosX3 = map.worldMapTile.GetUpperBound(0) * 0.80f;
        map.worldMapTile = MapFunctions.DirectionalTunnel(map.worldMapTile, middleMapSettings.minPathWidth, middleMapSettings.maxPathWidth,
            middleMapSettings.maxPathChange, middleMapSettings.roughness, middleMapSettings.windyness, (int)startPosX3);
    }

    private bool IsOutOfBound(int x, int y, int[,] worldMapTile) {
        return (x < 0 || x > worldMapTile.GetUpperBound(0)) || (y < 0 || y > worldMapTile.GetUpperBound(1));
    }

    private int GetAmountLight(int tile, int wallTile, int lastLight) { // TODO REFACTO pour utiliser le light service !!!!!!!!!!!!
        if (tile == 0 && wallTile == 0) {
            return 0;
        }
        int newLight = 0;
        if (tile > 0) {
            newLight = lastLight + 10;
        } else {
            if (wallTile > 0) {
                newLight = lastLight + 5;
            } else {
                return 0;
            }
        }
        return newLight > 100 ? 100 : newLight;
    }

    public void CreateMaps(WorldConfig worldConfig, MapSerialisable map, int seed) {
        int mapWidth = worldConfig.GetWorldWidth();
        int mapHeight = worldConfig.GetWorldHeight();
        int chunkSize = worldConfig.GetChunkSize();
        map.mapConf = new MapConf(mapWidth, mapHeight, chunkSize, seed);
        map.worldMapTile = new int[mapWidth, mapHeight];
        map.worldMapWall = new int[mapWidth, mapHeight];
        map.worldMapObject = new int[mapWidth, mapHeight];
        map.worldMapDynamicLight = new int[mapWidth, mapHeight];
        map.worldMapLight = new int[mapWidth, mapHeight];
        map.worldMapShadow = new int[mapWidth, mapHeight];
        for (var x = 0; x < mapWidth; x++) {
            for (var y = 0; y < mapHeight; y++) {
                map.worldMapLight[x, y] = 100;
            }
        }
    }
    public IEnumerator GenerateMap(MapSerialisable newMap, WorldConfig worldConfigs, int percentValue, int currentPercent) {
        // toDo voir à creer une liste d'Action afin de boucler dessus pour dynamiser numberOfTask par son length !
        int numberOfTask = percentValue / 8;
        yield return StartCoroutine(GenerateMapService.instance.GenerateTerrain(newMap, worldConfigs, currentPercent += numberOfTask));
        yield return StartCoroutine(GenerateMapService.instance.GenerateCaves(newMap, worldConfigs, currentPercent += numberOfTask));
        yield return StartCoroutine(GenerateMapService.instance.GenerateTunnels(newMap, worldConfigs, currentPercent += numberOfTask));
        yield return StartCoroutine(GenerateMapService.instance.GenerateIrons(newMap, worldConfigs, currentPercent += numberOfTask));
        yield return StartCoroutine(GenerateMapService.instance.GenerateTrees(newMap, worldConfigs, currentPercent += numberOfTask));
        yield return StartCoroutine(GenerateMapService.instance.GenerateGrassTiles(newMap, worldConfigs, currentPercent += numberOfTask));
        yield return StartCoroutine(GenerateMapService.instance.GenerateGrasses(newMap, worldConfigs, currentPercent += numberOfTask));
        yield return StartCoroutine(GenerateMapService.instance.GenerateLightMap(newMap, worldConfigs, currentPercent += numberOfTask));
    }

    public IEnumerator LoadMapFromFiles(MapSerialisable worldData, int saveSlot, string worldName, int percentValue, int currentPercent) {
        int numberOfTask = percentValue / worldMapTypesDico.Count;
        yield return StartCoroutine(GetMapFile(saveSlot, worldName, WORLDMAPTYPES.WORLDMAPLIGHT, worldData, currentPercent += numberOfTask));
        yield return StartCoroutine(GetMapFile(saveSlot, worldName, WORLDMAPTYPES.WORLDMAPSHADOW, worldData, currentPercent += numberOfTask));
        yield return StartCoroutine(GetMapFile(saveSlot, worldName, WORLDMAPTYPES.WORLDMAPTILE, worldData, currentPercent += numberOfTask));
        yield return StartCoroutine(GetMapFile(saveSlot, worldName, WORLDMAPTYPES.WORLDMAPWALL, worldData, currentPercent += numberOfTask));
        yield return StartCoroutine(GetMapFile(saveSlot, worldName, WORLDMAPTYPES.WORLDMAPOBJECT, worldData, currentPercent += numberOfTask));
        yield return StartCoroutine(GetMapFile(saveSlot, worldName, WORLDMAPTYPES.WORLDMAPDYNAMICLIGHT, worldData, currentPercent += numberOfTask));
    }

    public IEnumerator SaveMapFiles(int saveSlot, string worldName, MapSerialisable worldData, int percentValue, int currentPercent) {
        int numberOfTask = percentValue / worldMapTypesDico.Count;
        yield return StartCoroutine(SaveMapFile(saveSlot, worldName, WORLDMAPTYPES.WORLDMAPLIGHT, worldData.worldMapLight, currentPercent += numberOfTask));
        yield return StartCoroutine(SaveMapFile(saveSlot, worldName, WORLDMAPTYPES.WORLDMAPSHADOW, worldData.worldMapShadow, currentPercent += numberOfTask));
        yield return StartCoroutine(SaveMapFile(saveSlot, worldName, WORLDMAPTYPES.WORLDMAPTILE, worldData.worldMapTile, currentPercent += numberOfTask));
        yield return StartCoroutine(SaveMapFile(saveSlot, worldName, WORLDMAPTYPES.WORLDMAPWALL, worldData.worldMapWall, currentPercent += numberOfTask));
        yield return StartCoroutine(SaveMapFile(saveSlot, worldName, WORLDMAPTYPES.WORLDMAPOBJECT, worldData.worldMapObject, currentPercent += numberOfTask));
        yield return StartCoroutine(SaveMapFile(saveSlot, worldName, WORLDMAPTYPES.WORLDMAPDYNAMICLIGHT, worldData.worldMapDynamicLight, currentPercent += numberOfTask));
    }

    private static short[,] ConvertToShort2dArray(int[,] intArr) {
        int boundX = intArr.GetLength(0);
        int bountY = intArr.GetLength(1);
        short[,] result = new short[boundX, bountY];
        for (int x = 0; x < boundX; x++) {
            for (int y = 0; y < bountY; y++) {
                result[x, y] = (short)intArr[x, y];
            }
        }
        return result;
    }

    private static int[,] ConvertToInt2dArray(short[,] intArr) {
        int boundX = intArr.GetLength(0);
        int bountY = intArr.GetLength(1);
        int[,] result = new int[boundX, bountY];
        for (int x = 0; x < boundX; x++) {
            for (int y = 0; y < bountY; y++) {
                result[x, y] = (int)intArr[x, y];
            }
        }
        return result;
    }

    private IEnumerator SaveMapFile(int saveSlot, string worldName, WORLDMAPTYPES worldMapType, int[,] map, int loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Création des fichiers de savegarde pour la map " + worldMapTypesDico[worldMapType]);
        short[,] convertedMap = ConvertToShort2dArray(map);
        FileManager.Save(convertedMap, GameMaster.instance.GetSavePath(saveSlot) + "_" + worldName + "." + worldMapTypesDico[worldMapType] + ".map.data");
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public bool VerifyAllFileExists(int saveSlot, string worldName) {
        bool allExist = true;
        foreach (KeyValuePair<WORLDMAPTYPES, string> entry in worldMapTypesDico) {
            if(!FileManager.CheckFileExist(GameMaster.instance.GetSavePath(saveSlot) + "_" + worldName + "." + entry.Value + ".map.data")) {
                allExist = false;
            }
        }
        return allExist;
    }

    public IEnumerator GetMapFile(int saveSlot, string worldName, WORLDMAPTYPES worlMapType, MapSerialisable worldData, int loaderValue) {
        Loader.instance.SetCurrentAction("Chargement du monde", "Initialisation de la map " + worldMapTypesDico[worlMapType]);
        int[,] convertedValues = ConvertToInt2dArray(FileManager.GetFile<short[,]>(GameMaster.instance.GetSavePath(saveSlot) + "_" + worldName + "." + worldMapTypesDico[worlMapType] + ".map.data"));
        switch (worlMapType) {
            case WORLDMAPTYPES.WORLDMAPLIGHT:
                worldData.worldMapLight = convertedValues;
                break;
            case WORLDMAPTYPES.WORLDMAPSHADOW:
                worldData.worldMapShadow = convertedValues;
                break;
            case WORLDMAPTYPES.WORLDMAPTILE:
                worldData.worldMapTile = convertedValues;
                break;
            case WORLDMAPTYPES.WORLDMAPWALL:
                worldData.worldMapWall = convertedValues;
                break;
            case WORLDMAPTYPES.WORLDMAPOBJECT:
                worldData.worldMapObject = convertedValues;
                break;
            case WORLDMAPTYPES.WORLDMAPDYNAMICLIGHT:
                worldData.worldMapDynamicLight = convertedValues;
                break;
        }
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateLightMap(MapSerialisable newMap, WorldConfig worldConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération de la lumière");
        GenerateWorldLight(newMap.worldMapShadow, newMap.worldMapTile, newMap.worldMapWall);
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateTerrain(MapSerialisable newMap, WorldConfig worldConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération du terrain");
        MapFunctions.RandomWalkTop(newMap.worldMapTile, newMap.worldMapWall, newMap.mapConf.seed);
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateCaves(MapSerialisable newMap, WorldConfig worldConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération des caves");
        MapFunctions.PerlinNoiseCave(newMap.worldMapTile, worldConfig.GetWorldSettingsBottom());
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateTunnels(MapSerialisable newMap, WorldConfig worldConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération des tunnels");
        Generate3Tunnels(newMap, worldConfig);
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateIrons(MapSerialisable newMap, WorldConfig worldConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération des irons");
        MapFunctions.GenerateIrons(newMap.worldMapTile);
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateTrees(MapSerialisable newMap, WorldConfig worldConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération des arbres");
        MapFunctions.AddTreesItems(newMap.worldMapTile, newMap.worldMapObject, newMap.worldMapWall);
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateGrassTiles(MapSerialisable newMap, WorldConfig worldConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération des tiles d'herbe");
        MapFunctions.AddGrassOntop(newMap.worldMapTile, newMap.worldMapWall);
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateGrasses(MapSerialisable newMap, WorldConfig worldConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération de l'herbe");
        MapFunctions.AddGrassesItems(newMap.worldMapTile, newMap.worldMapObject, newMap.worldMapWall);
        yield return new WaitForSeconds(waitTime);
    }

    public void GenerateWorldLight(int[,] worldMapShadow, int[,] worldMapTile, int[,] worldMapWall) {
        var boundX = worldMapTile.GetUpperBound(0);
        var boundY = worldMapTile.GetUpperBound(1);
        // toDo a implémenter la tilemap background
        for (var x = 0; x < worldMapTile.GetUpperBound(0); x++) {
            // top to bottom
            for (var y = boundY; y > 0; y--) {
                if (worldMapTile[x, y] == 0 && !IsOutOfBound(x, y - 1, worldMapTile) && worldMapTile[x, y - 1] > 0 || worldMapTile[x, y - 1] <= 255) {
                    worldMapShadow[x, y - 1] = GetAmountLight(worldMapTile[x, y - 1], worldMapWall[x, y - 1], worldMapShadow[x, y]);
                }
            }
            // bottom to top
            for (var y = 0; y < boundY; y++) {
                if (worldMapTile[x, y] == 0 && !IsOutOfBound(x, y + 1, worldMapTile) && worldMapTile[x, y + 1] > 0 || worldMapTile[x, y + 1] <= 255) {
                    var newLight = GetAmountLight(worldMapTile[x, y + 1], worldMapWall[x, y + 1], worldMapShadow[x, y]);
                    var topLight = worldMapShadow[x, y + 1];
                    worldMapShadow[x, y + 1] = newLight <= topLight ? newLight : topLight;
                }
            }
        }
        for (var y = boundY; y > 0; y--) {
            // left to right
            for (var x = 0; x < boundX; x++) {
                if (worldMapTile[x, y] == 0 && !IsOutOfBound(x + 1, y, worldMapTile) && worldMapTile[x + 1, y] > 0 || worldMapTile[x + 1, y] <= 255) {
                    var newLight = GetAmountLight(worldMapTile[x + 1, y], worldMapWall[x + 1, y], worldMapShadow[x, y]);
                    var rightLight = worldMapShadow[x + 1, y];
                    worldMapShadow[x + 1, y] = newLight <= rightLight ? newLight : rightLight;
                }
            }
            // right to left
            for (var x = boundX; x > 0; x--) {
                if (worldMapTile[x, y] == 0 && !IsOutOfBound(x - 1, y, worldMapTile) && worldMapTile[x - 1, y] > 0 || worldMapTile[x - 1, y] <= 255) {
                    var newLight = GetAmountLight(worldMapTile[x - 1, y], worldMapWall[x - 1, y], worldMapShadow[x, y]);
                    var rightLight = worldMapShadow[x - 1, y];
                    worldMapShadow[x - 1, y] = newLight <= rightLight ? newLight : rightLight;
                }
            }
        }
    }

}
