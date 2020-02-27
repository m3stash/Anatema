using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapFunctions {

    public static void ClearMap(Tilemap tilemap) {
        if (tilemap)
            tilemap.ClearAllTiles();
    }

    private static bool CheckNoColliders(ItemConfig itemConf, int x, int y, int[,] worldMap, int[,] objectsMap, int[,] wallMap) {
        bool empty = true;
        foreach (CellCollider cell in itemConf.GetColliderConfig().GetCellColliders()) {
            if (!empty) {
                break;
            }
            bool noColliders = WorldManager.objectsMap[x + cell.GetRelativePosition().x, y + cell.GetRelativePosition().y] == 0 && worldMap[x, y] == 0 && wallMap[x, y] == 0;
            if (!noColliders) {
                empty = false;
            }
        }
        return empty;
    }

    private static void SetColliders(ItemConfig itemConf, int x, int y, int[,] objectsMap) {
        foreach (CellCollider cell in itemConf.GetColliderConfig().GetCellColliders()) {
            WorldManager.objectsMap[x + cell.GetRelativePosition().x, y + cell.GetRelativePosition().y] = cell.IsOrigin() ? itemConf.GetId() : -1;
        }
    }

    public static int[,] AddTrees(int[,] worldMap, int[,] objectsMap, int[,] wallMap) {
        ItemConfig itemConf = ItemManager.instance.GetItemWithId(31);
        var heightMap = worldMap.GetUpperBound(1);
        var widthMap = worldMap.GetUpperBound(0);
        // toDo voir a gérer les bordures du monde pour pas calculer dans le vide
        // left to right
        int newTreeGap = -1;
        int xEnd = widthMap - 5;
        int xStart = 5;
        for (int x = xStart; x < xEnd; x++) {
            if (x < newTreeGap) {
                continue;
            }
            // top to bottom
            for (int y = heightMap - 50; y > heightMap - 300; y--) {
                /*if (worldMap[x, y] == 1) {
                    // si current != 0 && gauche et droite != 0 continu sinon on ne va pas plus bas !
                    if (worldMap[x - 1, y] == 1 && worldMap[x + 1, y] == 1) {
                        bool empty = true;
                        
                        // tree collider => x = 3 && y = 5
                        for (int xx = x - 1; xx <= x + 1; xx++) {
                            if (!empty) {
                                break;
                            }
                            for (int yy = y + 1; yy <= y + 5; yy++) {
                                if (worldMap[xx, yy] > 0 || wallMap[xx, yy] > 0) {
                                    empty = false;
                                    break;
                                }
                            }
                        }
                        if (empty) {
                            objectsMap[x, y + 1] = 31;
                            var newXGap = x + Random.Range(5, 8);
                            newTreeGap = newXGap < xEnd ? newXGap : -1;
                        }
                    }
                    break;
                }*/
                if (worldMap[x, y - 1] == 1) {
                    if (CheckNoColliders(itemConf, x, y, worldMap, objectsMap, wallMap)) {
                        SetColliders(itemConf, x, y, objectsMap);
                        objectsMap[x, y] = 31; // toDo refacto plus tard pour les différents ID d'arbre
                        var newXGap = x + Random.Range(5, 8);
                        newTreeGap = newXGap < xEnd ? newXGap : -1;
                    }
                    break;
                }
            }
        }
        return objectsMap;
    }

    public static int[,] AddGrasses(int[,] worldMap, int[,] objectsMap, int[,] wallMap) {
        var heightMap = worldMap.GetUpperBound(1);
        var widthMap = worldMap.GetUpperBound(0);
        int idGrassWorldObject = 25;
        int newGap = -1;
        for (int x = 0; x < widthMap; x++) {
            if (x < newGap)
                continue;
            int deepCount = 0;
            for (int y = heightMap - 1; y > heightMap - 300; y--) {
                if (deepCount == 15)
                    break;
                if (worldMap[x, y] == 2) {
                    if (objectsMap[x, y + 1] == 0 && worldMap[x, y + 1] == 0 && deepCount < 15) {
                        objectsMap[x, y + 1] = idGrassWorldObject;
                        var newXGap = x + Random.Range(1, 4);
                        newGap = newXGap < widthMap ? newXGap : -1;
                    }
                }
                if (wallMap[x, y] > 0) {
                    deepCount++;
                }
            }
        }
        return objectsMap;
    }

    public static int[,] AddGrassOntop(int[,] map, int[,] wallMap) {
        var heightMap = map.GetUpperBound(1);
        var widthMap = map.GetUpperBound(0);
        int idGrass = 2;
        int idDirt = 1;
        int maxDeep = 15;
        for (int x = 0; x < widthMap; x++) {
            int deepCount = 0;
            for (int y = heightMap - 1; y > heightMap - 300; y--) {
                if (deepCount > maxDeep)
                    break;
                var topNeightboorTile = map[x, y + 1];
                var currentTile = map[x, y];
                if (topNeightboorTile == 0 && currentTile == idDirt) {
                    map[x, y] = idGrass;
                }
                if (currentTile == 0 && topNeightboorTile == idDirt && x - 4 > 0) {
                    var fiveLeftVoid = true;
                    for (int z = x; z > x - 4; z--) {
                        if (map[z, y] > 0) {
                            fiveLeftVoid = false;
                            break;
                        }
                    }
                    map[x, y + 1] = fiveLeftVoid == true ? idGrass : topNeightboorTile;
                }
                if (currentTile == 0 && topNeightboorTile == idDirt && x + 4 < widthMap) {
                    var fiveRightVoid = true;
                    for (int z = x; z < x + 4; z++) {
                        if (map[z, y] > 0) {
                            fiveRightVoid = false;
                            break;
                        }
                    }
                    map[x, y + 1] = fiveRightVoid == true ? idGrass : topNeightboorTile;
                }
                if (wallMap[x, y] > 0) {
                    deepCount++;
                }
            }
        }
        return map;
    }

    public static int[,] GenerateArray(int width, int height) {
        int[,] map = new int[width, height];
        /*for (int x = 0; x < map.GetUpperBound(0); x++) {
            for (int y = 0; y < map.GetUpperBound(1); y++) {
                map[x, y] = 1;
            }
        }*/
        return map;
    }

    public static void RenderMap(int[,] map, Tilemap tilemap, TileBase[] tile) {
        tilemap.ClearAllTiles();
        for (int x = 0; x < map.GetUpperBound(0); x++) {
            for (int y = 0; y < map.GetUpperBound(1); y++) {
                var tileId = map[x, y];
                if (tileId > 0) {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile[tileId]);
                } else {
                    tilemap.SetTile(new Vector3Int(x, y, 0), null);
                }
            }
        }
    }

    public static int[,] PerlinNoise(int[,] map, float seed) {
        int newPoint;
        //Used to reduced the position of the perlin point
        float reduction = 0.5f;
        //Create the perlin
        for (int x = 0; x < map.GetUpperBound(0); x++) {
            newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(x, seed) - reduction) * map.GetUpperBound(1));

            //Make sure the noise starts near the halfway point of the height
            newPoint += (map.GetUpperBound(1) / 2);
            for (int y = newPoint; y >= 0; y--) {
                map[x, y] = 1;
            }
        }
        return map;
    }

    public static int[,] PerlinNoiseSmooth(int[,] map, float seed, int interval) {
        //Smooth the noise and store it in the int array
        if (interval > 1) {
            int newPoint, points;
            //Used to reduced the position of the perlin point
            float reduction = 0.5f;

            //Used in the smoothing process
            Vector2Int currentPos, lastPos;
            //The corresponding points of the smoothing. One list for x and one for y
            List<int> noiseX = new List<int>();
            List<int> noiseY = new List<int>();

            //Generate the noise
            for (int x = 0; x < map.GetUpperBound(0); x += interval) {
                newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(x, (seed * reduction))) * map.GetUpperBound(1));
                noiseY.Add(newPoint);
                noiseX.Add(x);
            }

            points = noiseY.Count;

            //Start at 1 so we have a previous position already
            for (int i = 1; i < points; i++) {
                //Get the current position
                currentPos = new Vector2Int(noiseX[i], noiseY[i]);
                //Also get the last position
                lastPos = new Vector2Int(noiseX[i - 1], noiseY[i - 1]);

                //Find the difference between the two
                Vector2 diff = currentPos - lastPos;

                //Set up what the height change value will be 
                float heightChange = diff.y / interval;
                //Determine the current height
                float currHeight = lastPos.y;

                //Work our way through from the last x to the current x
                for (int x = lastPos.x; x < currentPos.x; x++) {
                    for (int y = Mathf.FloorToInt(currHeight); y > 0; y--) {
                        map[x, y] = 1;
                    }
                    currHeight += heightChange;
                }
            }
        } else {
            //Defaults to a normal perlin gen
            map = PerlinNoise(map, seed);
        }

        return map;
    }

    public static int[,] PerlinNoiseCave(int[,] map, float modifier) {
        int maxHeight = map.GetUpperBound(1);
        float rand = Random.Range(0, 1);

        for (int x = 0; x < map.GetUpperBound(0); x++) {
            for (int y = 0; y < maxHeight; y++) {
                modifier = 0.04f;
                // grottes
                var res = Mathf.PerlinNoise((x + rand) * modifier, (y + rand) * modifier);
                if (res > 0 && res < 0.3f) {
                    map[x, y] = 0;
                }
                modifier = 0.08f;
                res = Mathf.PerlinNoise((x + rand) * modifier, (y + rand) * modifier);
                // gruyère
                if (res > 0.8f) {
                    map[x, y] = 0;
                }
            }
        }


        /*for (int x = 0; x < map.GetUpperBound(0); x++) {
            for (int y = 0; y < maxHeight; y++) {
                modifier = 0.06f;
                var res = Mathf.PerlinNoise((x + rand) * modifier, (y + rand) * modifier);

                if (res > 0 && res < 0.2f) {
                    map[x, y] = 1;
                }
                if (res > 0.3f && res < 0.4f) {
                    map[x, y] = 1;
                }
                if (res > 0.4f && res < 0.6f) {
                    map[x, y] = 1;
                }
                if (res > 0.7f) {
                    map[x, y] = 0;
                }
            }
        }*/

        // original perlin noise..
        /*int newPoint;
        modifier = 0.06f;
        for (int x = 0; x < map.GetUpperBound(0); x++) {
            // for (int y = 0; y < map.GetUpperBound(1); y++) ///////////////////////////////////////
            for (int y = 0; y < 50; y++) {
                //Generate a new point using perlin noise, then round it to a value of either 0 or 1
                newPoint = Mathf.RoundToInt(Mathf.PerlinNoise(x * modifier, y * modifier));
                map[x, y] = newPoint;
            }
        }*/

        return map;
    }

    public static int[,] GenerateIrons(int[,] map) {
        int height = map.GetUpperBound(1);
        int copper_count = 0;
        int silver_count = 0;
        int iron_count = 0;
        int gold_count = 0;
        float heightCopper = height / 2f;
        float heightIron = height / 3;
        float heightSilver = height / 4;
        float heightGold = height - height / 3f;
        float modifier = 0.9f;
        for (int x = 0; x < map.GetUpperBound(0); x++) {
            for (int y = 0; y < height; y++) {
                float noise = Mathf.PerlinNoise(x * modifier, y * modifier);
                if (map[x, y] > 0) {
                    // copper
                    if (noise > 0.6f && noise < 0.630f && y > heightCopper) {
                        map[x, y] = 4;
                        copper_count++;
                    }
                    // iron
                    if (noise > 0.3f && noise < 0.315f && y > heightIron && y < height - heightIron) {
                        map[x, y] = 5;
                        iron_count++;
                    }
                    // silver
                    if (noise > 0.5f && noise < 0.510f && y > heightSilver) {
                        map[x, y] = 6;
                        silver_count++;
                    }
                    // gold
                    if (noise > 0.7f && noise < 0.705f && y < heightGold) {
                        map[x, y] = 7;
                        gold_count++;
                    }
                }
            }
        }
        Debug.Log("Cooper => " + copper_count);
        Debug.Log("Iron => " + iron_count);
        Debug.Log("Silver => " + silver_count);
        Debug.Log("Gold => " + gold_count);
        return map;
    }

    public static int[,] GenerateMountain(int[,] map, int[,] wallMap, float seed, Wave[] waves) {
        return GenerateNoiseMap(3, waves, map);
    }

    public static int[,] GenerateNoiseMap(float scale, Wave[] waves, int[,] map) {
        // create an empty noise map with the mapDepth and mapWidth coordinates

        for (int y = 0; y < map.GetUpperBound(1); y++) {
            for (int x = 0; x < map.GetUpperBound(0); x++) {
                // calculate sample indices based on the coordinates, the scale and the offset
                float sampleX = x / scale;
                float sampleY = y / scale;

                float noise = 0f;
                float normalization = 0f;
                foreach (Wave wave in waves) {
                    // generate noise value using PerlinNoise for a given Wave
                    noise += wave.amplitude * Mathf.PerlinNoise(sampleX * wave.frequency + wave.seed, sampleY * wave.frequency + wave.seed);
                    normalization += wave.amplitude;
                }
                // normalize the noise value so that it is within 0 and 1
                noise /= normalization;
                if (noise < 0.5f && map[x, y] == 0) {
                    map[x, y] = 1;
                }
            }
        }
        return map;
    }

    public static int[,] RandomWalkTop(int[,] map, int[,] wallMap, float seed) {
        System.Random rand = new System.Random(seed.GetHashCode());
        var mapHeight = map.GetUpperBound(1);
        var min = rand.Next(mapHeight - 300, mapHeight - 200);
        var max = rand.Next(mapHeight - 200, mapHeight - 100);
        int lastHeight = Random.Range(min, max);

        for (int x = 0; x < map.GetUpperBound(0); x++) {
            int nextMove = rand.Next(4);
            //If heads, and we aren't near the bottom, minus some height
            if (nextMove == 0 && lastHeight > 3) {
                lastHeight--;
            }

            //If tails, and we aren't near the top, add some height
            else if (nextMove == 2 && lastHeight < mapHeight - 200) {
                lastHeight++;
            }

            //Circle through from the lastheight to the bottom
            for (int y = lastHeight; y >= 0; y--) {
                map[x, y] = 1;
                wallMap[x, y] = 37; // toDo voir a changer le fond selon les biomes !!
            }
        }

        return map;
    }


    public static int[,] RandomWalkTopSmoothed(int[,] map, float seed, int minSectionWidth) {
        //Seed our random
        System.Random rand = new System.Random(seed.GetHashCode());

        //Determine the start position
        int lastHeight = Random.Range(0, map.GetUpperBound(1));

        //Used to determine which direction to go
        int nextMove = 0;
        //Used to keep track of the current sections width
        int sectionWidth = 0;

        //Work through the array width
        for (int x = 0; x <= map.GetUpperBound(0); x++) {
            //Determine the next move
            nextMove = rand.Next(2);

            //Only change the height if we have used the current height more than the minimum required section width
            if (nextMove == 0 && lastHeight > 0 && sectionWidth > minSectionWidth) {
                lastHeight--;
                sectionWidth = 0;
            } else if (nextMove == 1 && lastHeight < map.GetUpperBound(1) && sectionWidth > minSectionWidth) {
                lastHeight++;
                sectionWidth = 0;
            }
            //Increment the section width
            sectionWidth++;

            //Work our way from the height down to 0
            for (int y = lastHeight; y >= 0; y--) {
                map[x, y] = 1;
            }
        }

        //Return the modified map
        return map;
    }

    public static int[,] RandomWalkCave(int[,] map, float seed, int requiredFloorPercent) {
        //Seed our random
        System.Random rand = new System.Random(seed.GetHashCode());

        //Define our start x position
        int floorX = rand.Next(1, map.GetUpperBound(0) - 1);
        //Define our start y position
        int floorY = rand.Next(1, map.GetUpperBound(1) - 1);
        //Determine our required floorAmount
        int reqFloorAmount = ((map.GetUpperBound(1) * map.GetUpperBound(0)) * requiredFloorPercent) / 100;
        //Used for our while loop, when this reaches our reqFloorAmount we will stop tunneling
        int floorCount = 0;

        //Set our start position to not be a tile (0 = no tile, 1 = tile)
        map[floorX, floorY] = 0;
        //Increase our floor count
        floorCount++;

        while (floorCount < reqFloorAmount) {
            //Determine our next direction
            int randDir = rand.Next(4);

            switch (randDir) {
                case 0: //Up
                    //Ensure that the edges are still tiles
                    if ((floorY + 1) < map.GetUpperBound(1) - 1) {
                        //Move the y up one
                        floorY++;

                        //Check if that piece is currently still a tile
                        if (map[floorX, floorY] == 1) {
                            //Change it to not a tile
                            map[floorX, floorY] = 0;
                            //Increase floor count
                            floorCount++;
                        }
                    }
                    break;
                case 1: //Down
                    //Ensure that the edges are still tiles
                    if ((floorY - 1) > 1) {
                        //Move the y down one
                        floorY--;
                        //Check if that piece is currently still a tile
                        if (map[floorX, floorY] == 1) {
                            //Change it to not a tile
                            map[floorX, floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
                case 2: //Right
                    //Ensure that the edges are still tiles
                    if ((floorX + 1) < map.GetUpperBound(0) - 1) {
                        //Move the x to the right
                        floorX++;
                        //Check if that piece is currently still a tile
                        if (map[floorX, floorY] == 1) {
                            //Change it to not a tile
                            map[floorX, floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
                case 3: //Left
                    //Ensure that the edges are still tiles
                    if ((floorX - 1) > 1) {
                        //Move the x to the left
                        floorX--;
                        //Check if that piece is currently still a tile
                        if (map[floorX, floorY] == 1) {
                            //Change it to not a tile
                            map[floorX, floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
            }
        }
        //Return the updated map
        return map;
    }

    public static int[,] RandomWalkCaveCustom(int[,] map, float seed, int requiredFloorPercent) {
        //Seed our random
        System.Random rand = new System.Random(seed.GetHashCode());

        //Define our start x position
        int floorX = Random.Range(1, map.GetUpperBound(0) - 1);
        //Define our start y position
        int floorY = Random.Range(1, map.GetUpperBound(1) - 1);
        //Determine our required floorAmount
        int reqFloorAmount = ((map.GetUpperBound(1) * map.GetUpperBound(0)) * requiredFloorPercent) / 100;
        //Used for our while loop, when this reaches our reqFloorAmount we will stop tunneling
        int floorCount = 0;

        //Set our start position to not be a tile (0 = no tile, 1 = tile)
        map[floorX, floorY] = 0;
        //Increase our floor count
        floorCount++;

        while (floorCount < reqFloorAmount) {
            //Determine our next direction
            int randDir = rand.Next(8);

            switch (randDir) {
                case 0: //North-West
                    //Ensure we don't go off the map
                    if ((floorY + 1) < map.GetUpperBound(1) && (floorX - 1) > 0) {
                        //Move the y up 
                        floorY++;
                        //Move the x left
                        floorX--;

                        //Check if the position is a tile
                        if (map[floorX, floorY] == 1) {
                            //Change it to not a tile
                            map[floorX, floorY] = 0;
                            //Increase floor count
                            floorCount++;
                        }
                    }
                    break;
                case 1: //North
                    //Ensure we don't go off the map
                    if ((floorY + 1) < map.GetUpperBound(1)) {
                        //Move the y up
                        floorY++;

                        //Check if the position is a tile
                        if (map[floorX, floorY] == 1) {
                            //Change it to not a tile
                            map[floorX, floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
                case 2: //North-East
                    //Ensure we don't go off the map
                    if ((floorY + 1) < map.GetUpperBound(1) && (floorX + 1) < map.GetUpperBound(0)) {
                        //Move the y up
                        floorY++;
                        //Move the x right
                        floorX++;

                        //Check if the position is a tile
                        if (map[floorX, floorY] == 1) {
                            //Change it to not a tile
                            map[floorX, floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
                case 3: //East
                    //Ensure we don't go off the map
                    if ((floorX + 1) < map.GetUpperBound(0)) {
                        //Move the x right
                        floorX++;

                        //Check if the position is a tile
                        if (map[floorX, floorY] == 1) {
                            //Change it to not a tile
                            map[floorX, floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
                case 4: //South-East
                    //Ensure we don't go off the map
                    if ((floorY - 1) > 0 && (floorX + 1) < map.GetUpperBound(0)) {
                        //Move the y down
                        floorY--;
                        //Move the x right
                        floorX++;

                        //Check if the position is a tile
                        if (map[floorX, floorY] == 1) {
                            //Change it to not a tile
                            map[floorX, floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
                case 5: //South
                    //Ensure we don't go off the map
                    if ((floorY - 1) > 0) {
                        //Move the y down
                        floorY--;

                        //Check if the position is a tile
                        if (map[floorX, floorY] == 1) {
                            //Change it to not a tile
                            map[floorX, floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
                case 6: //South-West
                    //Ensure we don't go off the map
                    if ((floorY - 1) > 0 && (floorX - 1) > 0) {
                        //Move the y down
                        floorY--;
                        //move the x left
                        floorX--;

                        //Check if the position is a tile
                        if (map[floorX, floorY] == 1) {
                            //Change it to not a tile
                            map[floorX, floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
                case 7: //West
                    //Ensure we don't go off the map
                    if ((floorX - 1) > 0) {
                        //Move the x left
                        floorX--;

                        //Check if the position is a tile
                        if (map[floorX, floorY] == 1) {
                            //Change it to not a tile
                            map[floorX, floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
            }
        }

        return map;
    }

    public static int[,] DirectionalTunnel(int[,] map, int minPathWidth, int maxPathWidth, int maxPathChange, int roughness, int windyness, int startPosX) {
        //This value goes from its minus counterpart to its positive value, in this case with a width value of 1, the width of the tunnel is 3
        int tunnelWidth = 1;
        //Set up our seed for the random.
        System.Random rand = new System.Random(Time.time.GetHashCode());

        //Create the first part of the tunnel
        for (int i = -tunnelWidth; i <= tunnelWidth; i++) {
            map[startPosX + i, 0] = 0;
        }

        //Cycle through the array
        for (int y = 1; y < map.GetUpperBound(1); y++) {
            //Check if we can change the roughness
            if (rand.Next(0, 100) > roughness) {

                //Get the amount we will change for the width
                int widthChange = Random.Range(-maxPathWidth, maxPathWidth);
                tunnelWidth += widthChange;

                //Check to see we arent making the path too small
                if (tunnelWidth < minPathWidth) {
                    tunnelWidth = minPathWidth;
                }

                //Check that the path width isnt over our maximum
                if (tunnelWidth > maxPathWidth) {
                    tunnelWidth = maxPathWidth;
                }
            }

            //Check if we can change the windyness
            if (rand.Next(0, 100) > windyness) {
                //Get the amount we will change for the x position
                int xChange = Random.Range(-maxPathChange, maxPathChange);
                startPosX += xChange;

                //Check we arent too close to the left side of the map
                if (startPosX < maxPathWidth) {
                    startPosX = maxPathWidth;
                }
                //Check we arent too close to the right side of the map
                if (startPosX > (map.GetUpperBound(0) - maxPathWidth)) {
                    startPosX = map.GetUpperBound(0) - maxPathWidth;
                }
            }

            //Work through the width of the tunnel
            for (int i = -tunnelWidth; i <= tunnelWidth; i++) {
                map[startPosX + i, y] = 0;
            }
        }
        return map;
    }

    public static int[,] GenerateCellularAutomata(int width, int height, float seed, int fillPercent, bool edgesAreWalls) {
        //Seed our random number generator
        System.Random rand = new System.Random(seed.GetHashCode());

        //Set up the size of our array
        int[,] map = new int[width, height];

        //Start looping through setting the cells.
        for (int x = 0; x < map.GetUpperBound(0); x++) {
            for (int y = 0; y < map.GetUpperBound(1); y++) {
                if (edgesAreWalls && (x == 0 || x == map.GetUpperBound(0) - 1 || y == 0 || y == map.GetUpperBound(1) - 1)) {
                    //Set the cell to be active if edges are walls
                    map[x, y] = 1;
                } else {
                    //Set the cell to be active if the result of rand.Next() is less than the fill percentage
                    map[x, y] = (rand.Next(0, 100) < fillPercent) ? 1 : 0;
                }
            }
        }
        return map;
    }
}