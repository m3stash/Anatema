using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMapService : MonoBehaviour {

    public static GenerateMapService instance;

    private void Awake() {
        instance = this;
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
}
