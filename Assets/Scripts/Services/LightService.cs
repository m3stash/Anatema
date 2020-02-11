using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
public class LightService : MonoBehaviour {

    private static int maxW;
    private static int maxH;

    public static void RecursivAddNewLight(int x, int y, int lastLight) {
        if (IsOutOfBound(x, y))
            return;
        int newLight = GetAmountLight(WorldManager.tilesWorldMap[x, y], WorldManager.wallTilesMap[x, y], lastLight);
        if (newLight == 100 || newLight >= WorldManager.tilesLightMap[x, y])
            return;
        WorldManager.tilesLightMap[x, y] = newLight;
        RecursivAddNewLight(x + 1, y, newLight);
        RecursivAddNewLight(x, y + 1, newLight);
        RecursivAddNewLight(x - 1, y, newLight);
        RecursivAddNewLight(x, y - 1, newLight);
    }
    public static void RecursivDeleteLight(int x, int y, bool toDelete) {
        if (IsOutOfBound(x, y))
            return;
        var minLight = GetNeightboorMinOrMaxOpacity(WorldManager.tilesLightMap, x, y, false);
        int newLight = GetAmountLight(WorldManager.tilesWorldMap[x, y], WorldManager.wallTilesMap[x, y], minLight);
        if (newLight <= WorldManager.tilesLightMap[x, y] && !toDelete)
            return;
        var isLight = WorldManager.objectsMap[x, y] == 16 || WorldManager.dynamicLight[x, y] == 1;
        if (!toDelete && isLight)
            return;
        WorldManager.tilesLightMap[x, y] = newLight;
        RecursivDeleteLight(x + 1, y, false);
        RecursivDeleteLight(x, y + 1, false);
        RecursivDeleteLight(x - 1, y, false);
        RecursivDeleteLight(x, y - 1, false);
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
    public void RecursivAddShadow(int x, int y) {
        var tileWorldMap = WorldManager.tilesWorldMap[x, y];
        var wallTileMap = WorldManager.wallTilesMap[x, y];
        var tileLightMap = WorldManager.tilesLightMap[x, y];
        var isLight = WorldManager.objectsMap[x, y] == 16;
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
        RecursivAddShadow(x + 1, y);
        RecursivAddShadow(x, y + 1);
        RecursivAddShadow(x - 1, y);
        RecursivAddShadow(x, y - 1);
    }
    private static int GetNeightboorMinOrMaxOpacity(int[,] map, int x, int y, bool isMax) {
        var t = IsOutOfBoundMap(x, y + 1) ? 100 : map[x, y + 1];
        var b = IsOutOfBoundMap(x, y - 1) ? 100 : map[x, y - 1];
        var l = IsOutOfBoundMap(x - 1, y) ? 100 : map[x - 1, y];
        var r = IsOutOfBoundMap(x + 1, y) ? 100 : map[x + 1, y];
        if (isMax) {
            return System.Math.Max(System.Math.Max(t, b), System.Math.Max(l, r));
        }
        return System.Math.Min(System.Math.Min(t, b), System.Math.Min(l, r));
    }
    private static bool IsOutOfBoundMap(int x, int y) {
        return (x < 0 || x > maxW) || (y < 0 || y > maxH);
    }
    private static bool IsOutOfBound(int x, int y) {
        return (x < 0 || x > maxW) || (y < 0 || y > maxH);
    }
    private static int GetAmountLight(int tile, int wallTile, int lastLight) {
        if (tile > 0) {
            return lastLight + 10 < 100 ? lastLight + 10 : 100;
        }
        return lastLight + 5 < 100 ? lastLight + 5 : 100;
    }

    public static void Init() {
        maxW = WorldManager.tilesLightMap.GetUpperBound(0);
        maxH = WorldManager.tilesLightMap.GetUpperBound(1);
    }
}
