using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
public class LightService : MonoBehaviour {

    public static void RecursivAddNewLight(int x, int y, int lastLight) {
        if (IsOutOfBound(x, y))
            return;
        int newLight = GetAmountLight(WorldManager.tilesWorldMap[x, y], WorldManager.wallTilesMap[x, y], lastLight);
        if ((newLight >= WorldManager.tilesLightMap[x, y]) || newLight >= 100)
            return;
        WorldManager.tilesLightMap[x, y] = newLight;
        RecursivAddNewLight(x + 1, y, newLight);
        RecursivAddNewLight(x, y + 1, newLight);
        RecursivAddNewLight(x - 1, y, newLight);
        RecursivAddNewLight(x, y - 1, newLight);
    }
    public void RecursivDeleteShadow(int x, int y) {
        if (IsOutOfBound(x, y))
            return;
        var wallTileMap = WorldManager.wallTilesMap[x, y];
        var tileWorldMap = WorldManager.tilesWorldMap[x, y];
        var tileLightMap = WorldManager.tilesLightMap[x, y];
        var tileShadowMap = WorldManager.tilesShadowMap[x, y];
        var shadowOpacity = GetNeightboorMinOrMaxOpacity(WorldManager.tilesShadowMap, x, y, false);
        int newShadow = GetAmountLight(tileWorldMap, wallTileMap, shadowOpacity);
        var lightOpacity = GetNeightboorMinOrMaxOpacity(WorldManager.tilesLightMap, x, y, false);
        int newLight = GetAmountLight(tileWorldMap, wallTileMap, lightOpacity);
        if (newShadow >= tileShadowMap && newLight >= tileLightMap)
            return;
        if (newShadow < tileShadowMap) {
            WorldManager.tilesShadowMap[x, y] = newShadow;
        }
        if (newLight < tileLightMap) {
            WorldManager.tilesLightMap[x, y] = newLight;
        }
        RecursivDeleteShadow(x + 1, y);
        RecursivDeleteShadow(x, y + 1);
        RecursivDeleteShadow(x - 1, y);
        RecursivDeleteShadow(x, y - 1);
    }
    public static void RecursivDeleteLight(int x, int y, bool toDelete) {
        if (IsOutOfBound(x, y))
            return;
        var minLight = GetNeightboorMinOrMaxOpacity(WorldManager.tilesLightMap, x, y, false);
        int newLight = GetAmountLight(WorldManager.tilesWorldMap[x, y], WorldManager.wallTilesMap[x, y], minLight);
        var isLight = WorldManager.tilesObjetMap[x, y] != null && ChunkService.tilesObjetMap[x, y].name == "item_11(Clone)";
        if (newLight <= WorldManager.tilesLightMap[x, y] && !toDelete || !toDelete && isLight || newLight > 100)
            return;
        WorldManager.tilesLightMap[x, y] = newLight;
        RecursivDeleteLight(x + 1, y, false);
        RecursivDeleteLight(x, y + 1, false);
        RecursivDeleteLight(x - 1, y, false);
        RecursivDeleteLight(x, y - 1, false);
    }
    public void RecursivAddShadow(int x, int y, GameObject[,] tilesObjetMap) {
        var tileWorldMap = WorldManager.tilesWorldMap[x, y];
        var wallTileMap = WorldManager.wallTilesMap[x, y];
        var tileLightMap = WorldManager.tilesLightMap[x, y];
        var isLight = tilesObjetMap[x, y] != null && tilesObjetMap[x, y].name == "item_11(Clone)";
        if (IsOutOfBound(x, y) || (tileWorldMap == 0 && wallTileMap == 0 || isLight)) // toDo voir à faire ça autrement ? => lightOpacity == 0.15f
            return;
        var tileShadowMap = WorldManager.tilesShadowMap[x, y];
        var shadowOpacity = GetNeightboorMinOrMaxOpacity(WorldManager.tilesShadowMap, x, y, false);
        int newShadow = GetAmountLight(tileWorldMap, wallTileMap, shadowOpacity);
        var lightOpacity = GetNeightboorMinOrMaxOpacity(WorldManager.tilesLightMap, x, y, false);
        int newLight = GetAmountLight(tileWorldMap, wallTileMap, lightOpacity);
        if (newLight <= tileLightMap && newShadow <= tileShadowMap)
            return;
        if (newShadow > tileShadowMap) {
            WorldManager.tilesShadowMap[x, y] = newShadow;
        }
        if (newLight > tileLightMap) {
            WorldManager.tilesLightMap[x, y] = newLight;
        }
        RecursivAddShadow(x + 1, y, tilesObjetMap);
        RecursivAddShadow(x, y + 1, tilesObjetMap);
        RecursivAddShadow(x - 1, y, tilesObjetMap);
        RecursivAddShadow(x, y - 1, tilesObjetMap);
    }
    private static int GetNeightboorMinOrMaxOpacity(int[,] map, int x, int y, bool isMax) {
        var t = IsOutOfBoundMap(x, y + 1, map) ? 1 : map[x, y + 1];
        var b = IsOutOfBoundMap(x, y - 1, map) ? 1 : map[x, y - 1];
        var l = IsOutOfBoundMap(x - 1, y, map) ? 1 : map[x - 1, y];
        var r = IsOutOfBoundMap(x + 1, y, map) ? 1 : map[x + 1, y];
        if (isMax) {
            return Mathf.Max(t, b, l, r);
        }
        return Mathf.Min(t, b, l, r);
    }
    private static bool IsOutOfBoundMap(int x, int y, int[,] map) {
        return (x < 0 || x > map.GetUpperBound(0)) || (y < 0 || y > map.GetUpperBound(1));
    }
    private static bool IsOutOfBound(int x, int y) {
        return (x < 0 || x > WorldManager.tilesWorldMap.GetUpperBound(0)) || (y < 0 || y > WorldManager.tilesWorldMap.GetUpperBound(1));
    }
    private static int GetAmountLight(int tile, int wallTile, int lastLight) {
        if (tile == 0 && wallTile == 0) {
            return lastLight + 4;
        }
        int newLight = 0;
        if (tile > 0) {
            newLight = lastLight + 10;
        } else {
            if (wallTile > 0) {
                newLight = lastLight + 5;
            }
        }
        return newLight > 100 ? 100 : newLight;
    }
}
