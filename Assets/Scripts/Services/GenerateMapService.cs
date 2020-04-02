using System.Collections;
using UnityEngine;

public class GenerateMapService : MonoBehaviour {

    public static GenerateMapService instance;
    private float waitTime = 0.3f;

    private void Awake() {
        instance = this;
    }
    public void Generate3Tunnels(MapSerialisable map, MapConfig mapConfig) {
        // 3 tunnels
        MapSettings middleMapSettings = mapConfig.GetMapSettingsMiddle();
        var startPosX = map.tilesWorldMap.GetUpperBound(0) / 6;
        map.tilesWorldMap = MapFunctions.DirectionalTunnel(map.tilesWorldMap, middleMapSettings.minPathWidth, middleMapSettings.maxPathWidth,
            middleMapSettings.maxPathChange, middleMapSettings.roughness, middleMapSettings.windyness, startPosX);
        var startPosX2 = map.tilesWorldMap.GetUpperBound(0) / 2;
        map.tilesWorldMap = MapFunctions.DirectionalTunnel(map.tilesWorldMap, middleMapSettings.minPathWidth, middleMapSettings.maxPathWidth,
            middleMapSettings.maxPathChange, middleMapSettings.roughness, middleMapSettings.windyness, startPosX2);
        var startPosX3 = map.tilesWorldMap.GetUpperBound(0) * 0.80f;
        map.tilesWorldMap = MapFunctions.DirectionalTunnel(map.tilesWorldMap, middleMapSettings.minPathWidth, middleMapSettings.maxPathWidth,
            middleMapSettings.maxPathChange, middleMapSettings.roughness, middleMapSettings.windyness, (int)startPosX3);
    }

    public void GenerateWorldLight(int[,] tilesShadowMap, int[,] tilesWorldMap, int[,] wallTilesMap) {
        var boundX = tilesWorldMap.GetUpperBound(0);
        var boundY = tilesWorldMap.GetUpperBound(1);
        // toDo a implémenter la tilemap background
        for (var x = 0; x < tilesWorldMap.GetUpperBound(0); x++) {
            // top to bottom
            for (var y = boundY; y > 0; y--) {
                if (tilesWorldMap[x, y] == 0 && !IsOutOfBound(x, y - 1, tilesWorldMap) && tilesWorldMap[x, y - 1] > 0 || tilesWorldMap[x, y - 1] <= 255) {
                    tilesShadowMap[x, y - 1] = GetAmountLight(tilesWorldMap[x, y - 1], wallTilesMap[x, y - 1], tilesShadowMap[x, y]);
                }
            }
            // bottom to top
            for (var y = 0; y < boundY; y++) {
                if (tilesWorldMap[x, y] == 0 && !IsOutOfBound(x, y + 1, tilesWorldMap) && tilesWorldMap[x, y + 1] > 0 || tilesWorldMap[x, y + 1] <= 255) {
                    var newLight = GetAmountLight(tilesWorldMap[x, y + 1], wallTilesMap[x, y + 1], tilesShadowMap[x, y]);
                    var topLight = tilesShadowMap[x, y + 1];
                    tilesShadowMap[x, y + 1] = newLight <= topLight ? newLight : topLight;
                }
            }
        }
        for (var y = boundY; y > 0; y--) {
            // left to right
            for (var x = 0; x < boundX; x++) {
                if (tilesWorldMap[x, y] == 0 && !IsOutOfBound(x + 1, y, tilesWorldMap) && tilesWorldMap[x + 1, y] > 0 || tilesWorldMap[x + 1, y] <= 255) {
                    var newLight = GetAmountLight(tilesWorldMap[x + 1, y], wallTilesMap[x + 1, y], tilesShadowMap[x, y]);
                    var rightLight = tilesShadowMap[x + 1, y];
                    tilesShadowMap[x + 1, y] = newLight <= rightLight ? newLight : rightLight;
                }
            }
            // right to left
            for (var x = boundX; x > 0; x--) {
                if (tilesWorldMap[x, y] == 0 && !IsOutOfBound(x - 1, y, tilesWorldMap) && tilesWorldMap[x - 1, y] > 0 || tilesWorldMap[x - 1, y] <= 255) {
                    var newLight = GetAmountLight(tilesWorldMap[x - 1, y], wallTilesMap[x - 1, y], tilesShadowMap[x, y]);
                    var rightLight = tilesShadowMap[x - 1, y];
                    tilesShadowMap[x - 1, y] = newLight <= rightLight ? newLight : rightLight;
                }
            }
        }
    }

    private bool IsOutOfBound(int x, int y, int[,] tilesWorldMap) {
        return (x < 0 || x > tilesWorldMap.GetUpperBound(0)) || (y < 0 || y > tilesWorldMap.GetUpperBound(1));
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

    public void CreateMaps(MapConfig mapConfig, MapSerialisable map) {
        int mapWidth = mapConfig.GetMapWidth();
        int mapHeight = mapConfig.GetMapHeight();
        int chunkSize = mapConfig.GetChunkSize();
        map.mapWidth = mapWidth;
        map.mapHeight = mapHeight;
        map.chunkSize = chunkSize;
        map.tilesWorldMap = new int[mapWidth, mapHeight];
        map.wallTilesMap = new int[mapWidth, mapHeight];
        map.objectsMap = new int[mapWidth, mapHeight];
        map.dynamicLight = new int[mapWidth, mapHeight];
        map.tilesLightMap = new int[mapWidth, mapHeight];
        map.tilesShadowMap = new int[mapWidth, mapHeight];
        for (var x = 0; x < mapWidth; x++) {
            for (var y = 0; y < mapHeight; y++) {
                map.tilesLightMap[x, y] = 100;
            }
        }
    }
    public IEnumerator GenerateLightMap(MapSerialisable newMap, MapConfig mapConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération de la lumière");
        GenerateWorldLight(newMap.tilesShadowMap, newMap.tilesWorldMap, newMap.wallTilesMap);
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateTerrain(MapSerialisable newMap, MapConfig mapConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération du terrain");
        MapFunctions.RandomWalkTop(newMap.tilesWorldMap, newMap.wallTilesMap, mapConfig.GetMapSeed());
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateCaves(MapSerialisable newMap, MapConfig mapConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération des caves");
        MapFunctions.PerlinNoiseCave(newMap.tilesWorldMap, mapConfig.GetMapSettingsBottom());
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateTunnels(MapSerialisable newMap, MapConfig mapConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération des tunnels");
        Generate3Tunnels(newMap, mapConfig);
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateIrons(MapSerialisable newMap, MapConfig mapConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération des irons");
        MapFunctions.GenerateIrons(newMap.tilesWorldMap);
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateTrees(MapSerialisable newMap, MapConfig mapConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération des arbres");
        MapFunctions.AddTreesItems(newMap.tilesWorldMap, newMap.objectsMap, newMap.wallTilesMap);
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateGrassTiles(MapSerialisable newMap, MapConfig mapConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération des tiles d'herbe");
        MapFunctions.AddGrassOntop(newMap.tilesWorldMap, newMap.wallTilesMap);
        Loader.instance.SetLoaderValue(loaderValue);
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator GenerateGrasses(MapSerialisable newMap, MapConfig mapConfig, float loaderValue) {
        Loader.instance.SetCurrentAction("Création du monde", "Génération de l'herbe");
        MapFunctions.AddGrassesItems(newMap.tilesWorldMap, newMap.objectsMap, newMap.wallTilesMap);
        yield return new WaitForSeconds(waitTime);
    }
}
