using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMapService : MonoBehaviour {

    [SerializeField] private bool saveWorldToJson;

    public static GenerateMapService instance;
    private void Awake() {
        instance = this;
    }

    public void CreateMaps(int mapWidth, int mapHeight, int chunkSize, MapSerialisable map) {
        
        CreateWorldMap(map, mapWidth, mapHeight, chunkSize);
        CreateLightMap(map, mapWidth, mapHeight, chunkSize);
    }

    private void CreateLightMap(MapSerialisable map, int mapWidth, int mapHeight, int chunkSize) {
        map.tilesLightMap = new int[mapWidth, mapHeight];
        for (var x = 0; x < mapWidth; x++) {
            for (var y = 0; y < mapHeight; y++) {
                map.tilesLightMap[x, y] = 100;
            }
        }
        map.tilesShadowMap = new int[mapWidth, mapHeight];
        LevelGenerator.instance.GenerateWorldLight(map.tilesLightMap, map.tilesShadowMap, map.tilesWorldMap, map.wallTilesMap);
    }

    private void CreateWorldMap(MapSerialisable map, int mapWidth, int mapHeight, int chunkSize) {
        map.tilesWorldMap = new int[mapWidth, mapHeight];
        map.wallTilesMap = new int[mapWidth, mapHeight];
        map.objectsMap = new int[mapWidth, mapHeight];
        map.dynamicLight = new int[mapWidth, mapHeight];
        /*LevelGenerator.instance.GenerateWorldMap(tilesWorldMap, wallTilesMap);
        if (saveWorldToJson) {
            // for test with html canvas map render
            FileManager.SaveToJson(new ConvertWorldMapToJson(tilesWorldMap, wallTilesMap, objectsMap), "worldMap");
        }*/
    }
}
