using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
public class LightService : MonoBehaviour {

    private int[,] tilesWorldMap;
    private int[,] tilesShadowMap;
    private int[,] wallTilesMap;
    private int[,] tilesLightMap;

    public void Init(int[,] tilesWorldMap, int[,] tilesLightMap, int[,] wallTilesMap, int[,] tilesShadowMap) {
        this.tilesWorldMap = tilesWorldMap;
        this.wallTilesMap = wallTilesMap;
        this.tilesShadowMap = tilesShadowMap;
        this.tilesLightMap = tilesLightMap;
    }
    public void RecursivAddNewLight(int x, int y, int lastLight) {
        if (IsOutOfBound(x, y))
            return;
        int newLight = GetAmountLight(tilesWorldMap[x, y], wallTilesMap[x, y], lastLight);
        if ((newLight >= tilesLightMap[x, y]) || newLight >= 100)
            return;
        tilesLightMap[x, y] = newLight;
        RecursivAddNewLight(x + 1, y, newLight);
        RecursivAddNewLight(x, y + 1, newLight);
        RecursivAddNewLight(x - 1, y, newLight);
        RecursivAddNewLight(x, y - 1, newLight);
    }
    public void RecursivDeleteShadow(int x, int y) {
        if (IsOutOfBound(x, y))
            return;
        var wallTileMap = wallTilesMap[x, y];
        var tileWorldMap = tilesWorldMap[x, y];
        var tileLightMap = tilesLightMap[x, y];
        var tileShadowMap = tilesShadowMap[x, y];
        var shadowOpacity = GetNeightboorMinOrMaxOpacity(tilesShadowMap, x, y, false);
        int newShadow = GetAmountLight(tileWorldMap, wallTileMap, shadowOpacity);
        var lightOpacity = GetNeightboorMinOrMaxOpacity(tilesLightMap, x, y, false);
        int newLight = GetAmountLight(tileWorldMap, wallTileMap, lightOpacity);
        if (newShadow >= tileShadowMap && newLight >= tileLightMap)
            return;
        if (newShadow < tileShadowMap) {
            tilesShadowMap[x, y] = newShadow;
        }
        if (newLight < tileLightMap) {
            tilesLightMap[x, y] = newLight;
        }
        RecursivDeleteShadow(x + 1, y);
        RecursivDeleteShadow(x, y + 1);
        RecursivDeleteShadow(x - 1, y);
        RecursivDeleteShadow(x, y - 1);
    }
    public void RecursivDeleteLight(int x, int y, bool toDelete, GameObject[,] tilesObjetMap) {
        if (IsOutOfBound(x, y))
            return;
        var minLight = GetNeightboorMinOrMaxOpacity(tilesLightMap, x, y, false);
        int newLight = GetAmountLight(tilesWorldMap[x, y], wallTilesMap[x, y], minLight);
        var isLight = tilesObjetMap[x, y] != null && tilesObjetMap[x, y].name == "item_11(Clone)";
        if (newLight <= tilesLightMap[x, y] && !toDelete || !toDelete && isLight || newLight > 100)
            return;
        tilesLightMap[x, y] = newLight;
        RecursivDeleteLight(x + 1, y, false, tilesObjetMap);
        RecursivDeleteLight(x, y + 1, false, tilesObjetMap);
        RecursivDeleteLight(x - 1, y, false, tilesObjetMap);
        RecursivDeleteLight(x, y - 1, false, tilesObjetMap);
    }
    public void RecursivAddShadow(int x, int y, GameObject[,] tilesObjetMap) {
        var tileWorldMap = tilesWorldMap[x, y];
        var wallTileMap = wallTilesMap[x, y];
        var tileLightMap = tilesLightMap[x, y];
        var isLight = tilesObjetMap[x, y] != null && tilesObjetMap[x, y].name == "item_11(Clone)";
        if (IsOutOfBound(x, y) || (tileWorldMap == 0 && wallTileMap == 0 || isLight)) // toDo voir à faire ça autrement ? => lightOpacity == 0.15f
            return;
        var tileShadowMap = tilesShadowMap[x, y];
        var shadowOpacity = GetNeightboorMinOrMaxOpacity(tilesShadowMap, x, y, false);
        int newShadow = GetAmountLight(tileWorldMap, wallTileMap, shadowOpacity);
        var lightOpacity = GetNeightboorMinOrMaxOpacity(tilesLightMap, x, y, false);
        int newLight = GetAmountLight(tileWorldMap, wallTileMap, lightOpacity);
        if (newLight <= tileLightMap && newShadow <= tileShadowMap)
            return;
        if (newShadow > tileShadowMap) {
            tilesShadowMap[x, y] = newShadow;
        }
        if (newLight > tileLightMap) {
            tilesLightMap[x, y] = newLight;
        }
        RecursivAddShadow(x + 1, y, tilesObjetMap);
        RecursivAddShadow(x, y + 1, tilesObjetMap);
        RecursivAddShadow(x - 1, y, tilesObjetMap);
        RecursivAddShadow(x, y - 1, tilesObjetMap);
    }
    private int GetNeightboorMinOrMaxOpacity(int[,] map, int x, int y, bool isMax) {
        var t = IsOutOfBoundMap(x, y + 1, map) ? 1 : map[x, y + 1];
        var b = IsOutOfBoundMap(x, y - 1, map) ? 1 : map[x, y - 1];
        var l = IsOutOfBoundMap(x - 1, y, map) ? 1 : map[x - 1, y];
        var r = IsOutOfBoundMap(x + 1, y, map) ? 1 : map[x + 1, y];
        if (isMax) {
            return Mathf.Max(t, b, l, r);
        }
        return Mathf.Min(t, b, l, r);
    }
    private bool IsOutOfBoundMap(int x, int y, int[,] map) {
        return (x < 0 || x > map.GetUpperBound(0)) || (y < 0 || y > map.GetUpperBound(1));
    }
    private bool IsOutOfBound(int x, int y) {
        return (x < 0 || x > tilesWorldMap.GetUpperBound(0)) || (y < 0 || y > tilesWorldMap.GetUpperBound(1));
    }
    private int GetAmountLight(int tile, int wallTile, int lastLight) {
        if (tile == 0 && wallTile == 0) {
            return lastLight + 4;
        }
        int newLight = 0;
        if (tile > 0) {
            newLight = lastLight + 15;
        } else {
            if (wallTile > 0) {
                newLight = lastLight + 5;
            }
        }
        return newLight > 100 ? 100 : newLight;
    }
}
