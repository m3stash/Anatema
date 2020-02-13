using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapFunctions {

    public static void ClearMap(Tilemap tilemap) {
        if (tilemap)
            tilemap.ClearAllTiles();
    }

    public static int[][] AddTrees(int[][] worldMap, int[][] objectsMap) {
        var widthMap = worldMap.Length;
        var heightMap = worldMap[0].Length;
        // toDo voir a gérer les bordures du monde pour pas calculer dans le vide
        // left to right
        var newTreeGap = -1;
        var xEnd = widthMap - 5;
        var xStart = 5;
        var gapBetweenTrees = 5;
        for (int x = xStart; x < xEnd; x++) {
            if (x < newTreeGap) {
                continue;
            }
            // top to bottom
            for (int y = heightMap - 50; y > heightMap - 250; y--) {
                if (worldMap[x][y] == 1) {
                    // si current != 0 && gauche et droite != 0 continu sinon on ne va pas plus bas !
                    if (worldMap[x - 1][y] == 1 && worldMap[x + 1][y] == 1) {
                        var empty = true;
                        // tree collider => x = 3 && y = 5
                        for (int xx = x - 1; xx <= x + 1; xx++) {
                            if (!empty) {
                                break;
                            }
                            for (int yy = y + 1; yy <= y + 5; yy++) {
                                if (worldMap[xx][yy] > 0) {
                                    empty = false;
                                    break;
                                }
                            }
                        }
                        if (empty) {
                            objectsMap[x][y + 1] = 31;
                            var newXGap = x + gapBetweenTrees;
                            newTreeGap = newXGap < xEnd ? newXGap : -1; // min gap between 2 trees
                        }
                    }
                    break;
                }
            }
        }
        return objectsMap;
    }

    public static int[][] AddGrassOntop(int[][] map) {
        var heightMap = map[0].Length;
        var widthMap = map.Length;
        for (int x = 0; x < widthMap; x++) {
            // toDO voir a variabiliser ça 
            for (int y = heightMap - 300; y < heightMap - 1; y++) {
                var topNeightboorTile = map[x][y + 1];
                var currentTile = map[x][y];
                if (topNeightboorTile == 0 && currentTile == 1) {
                    map[x][y] = 2;
                }
                if (currentTile == 0 && topNeightboorTile == 1 && x - 4 > 0) {
                    var fiveLeftVoid = true;
                    for (int z = x; z > x - 4; z--) {
                        if (map[z][y] > 0) {
                            fiveLeftVoid = false;
                            break;
                        }
                    }
                    map[x][y + 1] = fiveLeftVoid == true ? 2 : topNeightboorTile;
                }
                if (currentTile == 0 && topNeightboorTile == 1 && x + 4 < widthMap) {
                    var fiveRightVoid = true;
                    for (int z = x; z < x + 4; z++) {
                        if (map[z][y] > 0) {
                            fiveRightVoid = false;
                            break;
                        }
                    }
                    map[x][y + 1] = fiveRightVoid == true ? 2 : topNeightboorTile;
                }

            }
        }
        return map;
    }

    public static int[][] GenerateArray(int width, int height) {
        int[][] map = new int[width][];
        /*for (int x = 0; x < map.GetUpperBound(0); x++) {
            for (int y = 0; y < map.GetUpperBound(1); y++) {
                map[x, y] = 1;
            }
        }*/
        return map;
    }

    public static void RenderMap(int[][] map, Tilemap tilemap, TileBase[] tile) {
        tilemap.ClearAllTiles();
        for (int x = 0; x < map.Length; x++) {
            for (int y = 0; y < map[0].Length; y++) {
                var tileId = map[x][y];
                if (tileId > 0) {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile[tileId]);
                } else {
                    tilemap.SetTile(new Vector3Int(x, y, 0), null);
                }
            }
        }
    }

    public static int[][] PerlinNoise(int[][] map, float seed) {
        int newPoint;
        //Used to reduced the position of the perlin point
        float reduction = 0.5f;
        int heightMap = map[0].Length;
        //Create the perlin
        for (int x = 0; x < map.Length; x++) {
            newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(x, seed) - reduction) * heightMap);

            //Make sure the noise starts near the halfway point of the height
            newPoint += (heightMap / 2);
            for (int y = newPoint; y >= 0; y--) {
                map[x][y] = 1;
            }
        }
        return map;
    }

    public static int[][] PerlinNoiseSmooth(int[][] map, float seed, int interval) {
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
            for (int x = 0; x < map.Length; x += interval) {
                newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(x, (seed * reduction))) * map[0].Length);
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
                        map[x][y] = 1;
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

    public static int[][] PerlinNoiseCave(int[][] map, float modifier) {
        var maxHeight = map[0].Length - 250;
        var rand = Random.Range(0.01f, 1);

        for (int x = 0; x < map.Length; x++) {
            for (int y = 0; y < maxHeight; y++) {
                modifier = 0.04f;
                // grottes
                var res = Mathf.PerlinNoise((x + rand) * modifier, (y + rand) * modifier);
                if (res > 0 && res < 0.3f) {
                    map[x][y] = 0;
                }
                modifier = 0.08f;
                res = Mathf.PerlinNoise((x + rand) * modifier, (y + rand) * modifier);
                // gruyère
                if (res > 0.8f) {
                    map[x][y] = 0;
                }
            }
        }
        return map;
    }

    public static int[][] GenerateIrons(int[][] map) {
        var copper_count = 0;
        var silver_count = 0;
        var iron_count = 0;
        var gold_count = 0;
        int mapHeight = map[0].Length;
        var heightCopper = mapHeight - 225;
        var heightSilver = mapHeight - 255;
        var heightGold = mapHeight - 400;
        var maxHeight = mapHeight;
        var modifier = 0.06f;
        for (int x = 0; x < map.Length; x++) {
            for (int y = 0; y < maxHeight; y++) {
                if (map[x][y] > 0) {
                    modifier = 0.9f;
                    var noise = Mathf.PerlinNoise(x * modifier, y * modifier);
                    // copper
                    if (noise > 0.6f && noise < 0.615f && y < heightCopper) {
                        map[x][y] = 4;
                        copper_count++;
                    }
                    // iron
                    if (noise > 0.3f && noise < 0.305f && y < heightCopper) {
                        map[x][y] = 5;
                        iron_count++;
                    }
                    // silver
                    if (noise > 0.5f && noise < 0.510f && y < heightSilver) {
                        map[x][y] = 6;
                        silver_count++;
                    }
                    // gold
                    if (noise > 0.7f && noise < 0.705f && y < heightGold) {
                        map[x][y] = 7;
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

    public static int[][] RandomWalkTop(int[][] map, int[][] wallMap, float seed) {
        System.Random rand = new System.Random(seed.GetHashCode());
        // var mapHeight = map.GetUpperBound(1);
        var mapHeight = map[0].Length;
        var min = rand.Next(mapHeight - 300, mapHeight - 200);
        var max = rand.Next(mapHeight - 200, mapHeight - 100);
        int lastHeight = Random.Range(min, max);
        var mapWidth = map.Length;
        for (int x = 0; x < mapWidth; x++) {
            // for (int x = 0; x < map.GetUpperBound(0); x++) {
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
                map[x][y] = 1;
                wallMap[x][y] = 37; // toDo voir a changer le fond selon les biomes !!
            }
        }

        return map;
    }

    public static int[][] RandomWalkTopSmoothed(int[][] map, float seed, int minSectionWidth) {
        //Seed our random
        System.Random rand = new System.Random(seed.GetHashCode());
        int mapWidth = map.Length;
        int mapHeiht = map[0].Length;
        //Determine the start position
        int lastHeight = Random.Range(0, mapHeiht);

        //Used to determine which direction to go
        int nextMove = 0;
        //Used to keep track of the current sections width
        int sectionWidth = 0;

        //Work through the array width
        for (int x = 0; x <= mapWidth; x++) {
            //Determine the next move
            nextMove = rand.Next(2);

            //Only change the height if we have used the current height more than the minimum required section width
            if (nextMove == 0 && lastHeight > 0 && sectionWidth > minSectionWidth) {
                lastHeight--;
                sectionWidth = 0;
            } else if (nextMove == 1 && lastHeight < mapHeiht && sectionWidth > minSectionWidth) {
                lastHeight++;
                sectionWidth = 0;
            }
            //Increment the section width
            sectionWidth++;

            //Work our way from the height down to 0
            for (int y = lastHeight; y >= 0; y--) {
                map[x][y] = 1;
            }
        }

        //Return the modified map
        return map;
    }

    public static int[][] RandomWalkCave(int[][] map, float seed, int requiredFloorPercent) {
        //Seed our random
        System.Random rand = new System.Random(seed.GetHashCode());
        int mapWidth = map.Length;
        int mapHeight = map[0].Length;
        //Define our start x position
        int floorX = rand.Next(1, mapWidth - 1);
        //Define our start y position
        int floorY = rand.Next(1, mapHeight - 1);
        //Determine our required floorAmount
        int reqFloorAmount = ((mapHeight * mapWidth) * requiredFloorPercent) / 100;
        //Used for our while loop, when this reaches our reqFloorAmount we will stop tunneling
        int floorCount = 0;

        //Set our start position to not be a tile (0 = no tile, 1 = tile)
        map[floorX][floorY] = 0;
        //Increase our floor count
        floorCount++;

        while (floorCount < reqFloorAmount) {
            //Determine our next direction
            int randDir = rand.Next(4);

            switch (randDir) {
                case 0: //Up
                    //Ensure that the edges are still tiles
                    if ((floorY + 1) < mapHeight - 1) {
                        //Move the y up one
                        floorY++;

                        //Check if that piece is currently still a tile
                        if (map[floorX][floorY] == 1) {
                            //Change it to not a tile
                            map[floorX][floorY] = 0;
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
                        if (map[floorX][floorY] == 1) {
                            //Change it to not a tile
                            map[floorX][floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
                case 2: //Right
                    //Ensure that the edges are still tiles
                    if ((floorX + 1) < map.Length - 1) {
                        //Move the x to the right
                        floorX++;
                        //Check if that piece is currently still a tile
                        if (map[floorX][floorY] == 1) {
                            //Change it to not a tile
                            map[floorX][floorY] = 0;
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
                        if (map[floorX][floorY] == 1) {
                            //Change it to not a tile
                            map[floorX][floorY] = 0;
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

    public static int[][] RandomWalkCaveCustom(int[][] map, float seed, int requiredFloorPercent) {
        //Seed our random
        System.Random rand = new System.Random(seed.GetHashCode());
        int mapWidth = map.Length;
        int mapHeight = map.Length;
        //Define our start x position
        int floorX = Random.Range(1, mapHeight - 1);
        //Define our start y position
        int floorY = Random.Range(1, mapHeight - 1);
        //Determine our required floorAmount
        int reqFloorAmount = ((mapHeight * mapWidth) * requiredFloorPercent) / 100;
        //Used for our while loop, when this reaches our reqFloorAmount we will stop tunneling
        int floorCount = 0;

        //Set our start position to not be a tile (0 = no tile, 1 = tile)
        map[floorX][floorY] = 0;
        //Increase our floor count
        floorCount++;

        while (floorCount < reqFloorAmount) {
            //Determine our next direction
            int randDir = rand.Next(8);

            switch (randDir) {
                case 0: //North-West
                    //Ensure we don't go off the map
                    if ((floorY + 1) < mapHeight && (floorX - 1) > 0) {
                        //Move the y up 
                        floorY++;
                        //Move the x left
                        floorX--;

                        //Check if the position is a tile
                        if (map[floorX][floorY] == 1) {
                            //Change it to not a tile
                            map[floorX][floorY] = 0;
                            //Increase floor count
                            floorCount++;
                        }
                    }
                    break;
                case 1: //North
                    //Ensure we don't go off the map
                    if ((floorY + 1) < mapHeight) {
                        //Move the y up
                        floorY++;

                        //Check if the position is a tile
                        if (map[floorX][floorY] == 1) {
                            //Change it to not a tile
                            map[floorX][floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
                case 2: //North-East
                    //Ensure we don't go off the map
                    if ((floorY + 1) < mapHeight && (floorX + 1) < mapWidth) {
                        //Move the y up
                        floorY++;
                        //Move the x right
                        floorX++;

                        //Check if the position is a tile
                        if (map[floorX][floorY] == 1) {
                            //Change it to not a tile
                            map[floorX][floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
                case 3: //East
                    //Ensure we don't go off the map
                    if ((floorX + 1) < map.Length) {
                        //Move the x right
                        floorX++;

                        //Check if the position is a tile
                        if (map[floorX][floorY] == 1) {
                            //Change it to not a tile
                            map[floorX][floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
                case 4: //South-East
                    //Ensure we don't go off the map
                    if ((floorY - 1) > 0 && (floorX + 1) < mapWidth) {
                        //Move the y down
                        floorY--;
                        //Move the x right
                        floorX++;

                        //Check if the position is a tile
                        if (map[floorX][floorY] == 1) {
                            //Change it to not a tile
                            map[floorX][floorY] = 0;
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
                        if (map[floorX][floorY] == 1) {
                            //Change it to not a tile
                            map[floorX][floorY] = 0;
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
                        if (map[floorX][floorY] == 1) {
                            //Change it to not a tile
                            map[floorX][floorY] = 0;
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
                        if (map[floorX][floorY] == 1) {
                            //Change it to not a tile
                            map[floorX][floorY] = 0;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
            }
        }

        return map;
    }

    public static int[][] DirectionalTunnel(int[][] map, int minPathWidth, int maxPathWidth, int maxPathChange, int roughness, int windyness, int startPosX) {
        //This value goes from its minus counterpart to its positive value, in this case with a width value of 1, the width of the tunnel is 3
        int tunnelWidth = 1;
        //Set up our seed for the random.
        System.Random rand = new System.Random(Time.time.GetHashCode());

        //Create the first part of the tunnel
        for (int i = -tunnelWidth; i <= tunnelWidth; i++) {
            map[startPosX + i][0] = 0;
        }

        //Cycle through the array
        for (int y = 1; y < map[0].Length; y++) {
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
                int mapWidth = map.Length;
                if (startPosX > (mapWidth - maxPathWidth)) {
                    startPosX = mapWidth - maxPathWidth;
                }
            }

            //Work through the width of the tunnel
            for (int i = -tunnelWidth; i <= tunnelWidth; i++) {
                map[startPosX + i][y] = 0;
            }
        }
        return map;
    }

    public static int[][] GenerateCellularAutomata(int width, int height, float seed, int fillPercent, bool edgesAreWalls) {
        //Seed our random number generator
        System.Random rand = new System.Random(seed.GetHashCode());

        //Set up the size of our array
        int[][] map = new int[width][];
        WorldManager.Create2dArray(map, height);
        //Start looping through setting the cells.
        int mapWidth = map.Length;
        int mapHeight = map[0].Length;
        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                if (edgesAreWalls && (x == 0 || x == mapWidth - 1 || y == 0 || y == mapHeight - 1)) {
                    //Set the cell to be active if edges are walls
                    map[x][y] = 1;
                } else {
                    //Set the cell to be active if the result of rand.Next() is less than the fill percentage
                    map[x][y] = (rand.Next(0, 100) < fillPercent) ? 1 : 0;
                }
            }
        }
        return map;
    }
}