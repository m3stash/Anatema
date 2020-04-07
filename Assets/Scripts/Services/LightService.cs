using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
public class LightService : MonoBehaviour {

    private static int maxW;
    private static int maxH;

    public static LightService instance;

    public void Init() {
        instance = this;
        maxW = WorldManager.instance.worldMapLight.GetUpperBound(0);
        maxH = WorldManager.instance.worldMapLight.GetUpperBound(1);
    }

    public void RecursivAddNewLight(int x, int y, int lastLight) {
        if (IsOutOfBound(x, y))
            return;
        int newLight = GetAmountLight(WorldManager.instance.worldMapTile[x, y], WorldManager.instance.worldMapWall[x, y], lastLight);
        if (newLight == 100 || newLight >= WorldManager.instance.worldMapLight[x, y])
            return;
        WorldManager.instance.worldMapLight[x, y] = newLight;
        RecursivAddNewLight(x + 1, y, newLight);
        RecursivAddNewLight(x, y + 1, newLight);
        RecursivAddNewLight(x - 1, y, newLight);
        RecursivAddNewLight(x, y - 1, newLight);
    }
    public void RecursivDeleteLight(int x, int y, bool toDelete) {
        if (IsOutOfBound(x, y))
            return;
        var minLight = GetNeightboorMinOpacity(WorldManager.instance.worldMapLight, x, y);
        int newLight = GetAmountLight(WorldManager.instance.worldMapLight[x, y], WorldManager.instance.worldMapLight[x, y], minLight);
        if (newLight <= WorldManager.instance.worldMapLight[x, y] && !toDelete)
            return;
        var isLight = IsLight(x, y);
        if (!toDelete && isLight)
            return;
        WorldManager.instance.worldMapLight[x, y] = newLight;
        RecursivDeleteLight(x + 1, y, false);
        RecursivDeleteLight(x, y + 1, false);
        RecursivDeleteLight(x - 1, y, false);
        RecursivDeleteLight(x, y - 1, false);
    }
    public void RecursivDeleteShadow(int x, int y) {
        if (IsOutOfBound(x, y))
            return;
        var wallTileMap = WorldManager.instance.worldMapWall[x, y];
        var tileWorldMap = WorldManager.instance.worldMapTile[x, y];
        var tileLightMap = WorldManager.instance.worldMapLight[x, y];
        var tileShadowMap = WorldManager.instance.worldMapShadow[x, y];
        var shadowOpacity = GetNeightboorMinOpacity(WorldManager.instance.worldMapShadow, x, y);
        int newShadow = GetAmountLight(tileWorldMap, wallTileMap, shadowOpacity);
        var lightOpacity = GetNeightboorMinOpacity(WorldManager.instance.worldMapLight, x, y);
        int newLight = GetAmountLight(tileWorldMap, wallTileMap, lightOpacity);
        bool badNewShadow = newShadow >= tileShadowMap;
        bool badNewLight = newLight >= tileLightMap;
        if (badNewShadow && badNewLight)
            return;
        if (!badNewShadow) {
            WorldManager.instance.worldMapShadow[x, y] = newShadow;
        }
        if (!badNewLight) {
            WorldManager.instance.worldMapLight[x, y] = newLight;
        }
        RecursivDeleteShadow(x + 1, y);
        RecursivDeleteShadow(x, y + 1);
        RecursivDeleteShadow(x - 1, y);
        RecursivDeleteShadow(x, y - 1);
    }

    public bool IsLight(int x, int y) {
        return WorldManager.instance.worldMapObject[x, y] == 16 || WorldManager.instance.worldMapDynamicLight[x, y] == 1;
    }

    public void RecursivAddShadow(int x, int y) {
        var tileWorldMap = WorldManager.instance.worldMapTile[x, y];
        var wallTileMap = WorldManager.instance.worldMapWall[x, y];
        var tileLightMap = WorldManager.instance.worldMapLight[x, y];
        var isLight = IsLight(x, y);
        if (isLight || IsOutOfBound(x, y) || (tileWorldMap == 0 && wallTileMap == 0))
            return;
        var tileShadowMap = WorldManager.instance.worldMapShadow[x, y];
        var shadowOpacity = GetNeightboorMinOpacity(WorldManager.instance.worldMapShadow, x, y);
        int newShadow = GetAmountLight(tileWorldMap, wallTileMap, shadowOpacity);
        var lightOpacity = GetNeightboorMinOpacity(WorldManager.instance.worldMapLight, x, y);
        int newLight = GetAmountLight(tileWorldMap, wallTileMap, lightOpacity);
        bool badNewShadow = newShadow <= tileShadowMap;
        bool badNewLight = newLight <= tileLightMap;
        if (newLight <= tileLightMap && newShadow <= tileShadowMap)
            return;
        if (!badNewShadow) {
            WorldManager.instance.worldMapShadow[x, y] = newShadow;
        }
        if (!badNewLight) {
            WorldManager.instance.worldMapLight[x, y] = newLight;
        }
        RecursivAddShadow(x + 1, y);
        RecursivAddShadow(x, y + 1);
        RecursivAddShadow(x - 1, y);
        RecursivAddShadow(x, y - 1);
    }

    private int GetNeightboorMaxOpacity(int[,] map, int x, int y) {
        if (x == 0 || x == maxW || y == 0 || y == maxH) {
            return 100;
        }
        return System.Math.Max(System.Math.Max(map[x, y + 1], map[x, y - 1]), System.Math.Max(map[x - 1, y], map[x + 1, y]));
    }

    private int GetNeightboorMinOpacity(int[,] map, int x, int y) {
        if (x > 0 && x < maxW && y > 0 && y < maxH) {
            return System.Math.Min(System.Math.Min(map[x, y + 1], map[x, y - 1]), System.Math.Min(map[x - 1, y], map[x + 1, y]));
        }
        var t = IsOutOfBoundMap(x, y + 1) ? 100 : map[x, y + 1];
        var b = IsOutOfBoundMap(x, y - 1) ? 100 : map[x, y - 1];
        var l = IsOutOfBoundMap(x - 1, y) ? 100 : map[x - 1, y];
        var r = IsOutOfBoundMap(x + 1, y) ? 100 : map[x + 1, y];
        return System.Math.Min(System.Math.Min(t, b), System.Math.Min(l, r));
    }
    private bool IsOutOfBoundMap(int x, int y) {
        return (x < 0 || x > maxW) || (y < 0 || y > maxH);
    }
    public bool IsOutOfBound(int x, int y) { // TODO replace with WorldManager.instance.IsOutOfBound()
        return (x < 0 || x > maxW) || (y < 0 || y > maxH);
    }
    private int GetAmountLight(int tile, int wallTile, int lastLight) {
        if (tile > 0) {
            return lastLight + 10 < 100 ? lastLight + 10 : 100;
        }
        if (wallTile > 0) {
            return lastLight + 5 < 100 ? lastLight + 5 : 100;
        }
        return lastLight + 4 < 100 ? lastLight + 4 : 100;
    }

}
