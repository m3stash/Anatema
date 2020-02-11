using UnityEngine;
public class LevelGenerator : MonoBehaviour {

    public void GenerateTilesWorldMap(int[,] worldMap, int[,] wallTilesMap, int[,] objectsMap) {
        MapSettings middleMapSettings = Instantiate((MapSettings)Resources.Load("Scriptables/MapSettings/MiddleLayer"));
        MapSettings bottomMapSettings = Instantiate((MapSettings)Resources.Load("Scriptables/MapSettings/BottomLayer"));
        MapSettings topMapSettings = Instantiate((MapSettings)Resources.Load("Scriptables/MapSettings/TopLayer"));

        float seed = Time.time.GetHashCode();
        // float seed = Random.value;
        worldMap = MapFunctions.RandomWalkTop(worldMap, wallTilesMap, seed);
        worldMap = MapFunctions.PerlinNoiseCave(worldMap, bottomMapSettings.modifier);

        // Création des 3 tunnels 
        var startPosX = worldMap.GetUpperBound(0) / 6;
        worldMap = MapFunctions.DirectionalTunnel(worldMap, middleMapSettings.minPathWidth, middleMapSettings.maxPathWidth,
            middleMapSettings.maxPathChange, middleMapSettings.roughness, middleMapSettings.windyness, startPosX);
        var startPosX2 = worldMap.GetUpperBound(0) / 2;
        worldMap = MapFunctions.DirectionalTunnel(worldMap, middleMapSettings.minPathWidth, middleMapSettings.maxPathWidth,
            middleMapSettings.maxPathChange, middleMapSettings.roughness, middleMapSettings.windyness, startPosX2);
        var startPosX3 = worldMap.GetUpperBound(0) * 0.80f;
        worldMap = MapFunctions.DirectionalTunnel(worldMap, middleMapSettings.minPathWidth, middleMapSettings.maxPathWidth,
            middleMapSettings.maxPathChange, middleMapSettings.roughness, middleMapSettings.windyness, (int)startPosX3);
        // generate irons
        MapFunctions.GenerateIrons(worldMap);
        // toDO attention à générer les colines avant les arbres!
        // add trees
        objectsMap = MapFunctions.AddTrees(worldMap, objectsMap);
        // add grass
        worldMap = MapFunctions.AddGrassOntop(worldMap);
    }
    public void GenerateObjectsWorldMap(int[,] map) { }
    private bool IsOnBound(int x, int y, int BoundX, int boundY) {
        if (x < 0 || x > BoundX || y < 0 || y > boundY) {
            return false;
        }
        return true;
    }
    /*public void GenerateWorldLight(float[,] tilesLightMap, int[,] tilesWorldMap, int[,] wallTilesMap) {
        var boundX = tilesWorldMap.GetUpperBound(0);
        var boundY = tilesWorldMap.GetUpperBound(1);
        // toDo a implémenter la tilemap background
        for (var x = 0; x < tilesWorldMap.GetUpperBound(0); x++) {
            // top to bottom
            for (var y = boundY; y > 0; y--) {
                if (tilesWorldMap[x, y] == 0 && !IsOutOfBound(x, y - 1, tilesWorldMap) && tilesWorldMap[x, y - 1] > 0 || tilesWorldMap[x, y - 1] <= 1) {
                    tilesLightMap[x, y - 1] = GetAmountLight(tilesWorldMap[x, y - 1], wallTilesMap[x, y - 1], tilesLightMap[x, y]);
                }
            }
            // bottom to top
            for (var y = 0; y < boundY; y++) {
                if (tilesWorldMap[x, y] == 0 && !IsOutOfBound(x, y + 1, tilesWorldMap) && tilesWorldMap[x, y + 1] > 0 || tilesWorldMap[x, y + 1] <= 1) {
                    var newLight = GetAmountLight(tilesWorldMap[x, y + 1], wallTilesMap[x, y + 1], tilesLightMap[x, y]);
                    var topLight = tilesLightMap[x, y + 1];
                    tilesLightMap[x, y + 1] = newLight <= topLight ? newLight : topLight;
                }
            }
        }
        for (var y = boundY; y > 0; y--) {
            // left to right
            for (var x = 0; x < boundX; x++) {
                if (tilesWorldMap[x, y] == 0 && !IsOutOfBound(x + 1, y, tilesWorldMap) && tilesWorldMap[x + 1, y] > 0 || tilesWorldMap[x + 1, y] <= 1) {
                    var newLight = GetAmountLight(tilesWorldMap[x + 1, y], wallTilesMap[x + 1, y], tilesLightMap[x, y]);
                    var rightLight = tilesLightMap[x + 1, y];
                    tilesLightMap[x + 1, y] = newLight <= rightLight ? newLight : rightLight;
                }
            }
            // right to left
            for (var x = boundX; x > 0; x--) {
                if (tilesWorldMap[x, y] == 0 && !IsOutOfBound(x - 1, y, tilesWorldMap) && tilesWorldMap[x - 1, y] > 0 || tilesWorldMap[x - 1, y] <= 1) {
                    var newLight = GetAmountLight(tilesWorldMap[x - 1, y], wallTilesMap[x - 1, y], tilesLightMap[x, y]);
                    var rightLight = tilesLightMap[x - 1, y];
                    tilesLightMap[x - 1, y] = newLight <= rightLight ? newLight : rightLight;
                }
            }
        }
    }*/

    public void GenerateWorldLight(int[,] tilesLightMap, int[,] tilesShadowMap, int[,] tilesWorldMap, int[,] wallTilesMap) {
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
}