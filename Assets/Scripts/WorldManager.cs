using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System;

public class WorldManager : MonoBehaviour {

    private ChunkService chunkService;
    private LightService lightService;
    private LevelGenerator levelGenerator;
    private GameObject tile_selector;
    private GameObject player;
    private int[,] tilesLightMap;
    private int[,] tilesShadowMap;
    private int[,] tilesWorldMap;
    private int[,] wallTilesMap;
    private int[,] objectsMap;
    private GameObject[,] tilesObjetMap;
    private Dictionary<int, TileBase> tilebaseDictionary;
    private Dictionary<int, Item_cfg> ObjectbaseDictionary;
    private Sprite[] block_sprites;
    public int worldSizeX;
    public int worldSizeY;
    public TileBase_cfg tilebase_cfg;
    public int chunkSize;
    // event
    public delegate void LightEventHandler(int intensity);
    public static event LightEventHandler RefreshLight;
    public delegate void PlayerLoaded(GameObject player);
    public static event PlayerLoaded GetPlayer;

    private void InitFolders() {
        FileManager.ManageFolder("chunk-data");
    }

    void Start() {
        InitFolders();
        InitResources();
        CreateWorldMap();
        CreateLightMap();
        CreatePlayer();
        chunkService.Init(chunkSize, tilebaseDictionary, tilesWorldMap, tilesLightMap, player, lightService, tilesShadowMap, tilesObjetMap, objectsMap);
        lightService.Init(tilesWorldMap, tilesLightMap, wallTilesMap, tilesShadowMap);
        GetPlayer(player);
    }
    private void InitResources() {
        chunkService = gameObject.GetComponent<ChunkService>();
        levelGenerator = gameObject.GetComponent<LevelGenerator>();
        lightService = gameObject.GetComponent<LightService>();
        block_sprites = Resources.LoadAll<Sprite>("Sprites/blocks");
        tilebaseDictionary = tilebase_cfg.GetDico();
        tile_selector = Instantiate(Resources.Load("Prefabs/tile_selector")) as GameObject;
    }
    private void CreateLightMap() {
        tilesLightMap = new int[worldSizeX, worldSizeY];
        for(var x = 0; x < worldSizeX; x++) {
            for (var y = 0; y < worldSizeY; y++) {
                tilesLightMap[x, y] = 100;
            }
        }
        tilesShadowMap = new int[worldSizeX, worldSizeY];
        levelGenerator.GenerateWorldLight(tilesLightMap, tilesShadowMap, tilesWorldMap, wallTilesMap);
    }
    private void CreateWorldMap() {
        tilesWorldMap = new int[worldSizeX, worldSizeY];
        wallTilesMap = new int[worldSizeX, worldSizeY];
        objectsMap = new int[worldSizeX, worldSizeY];
        tilesObjetMap = new GameObject[worldSizeX, worldSizeY]; // toDo voir a virer cette merde et plutot utiliser : objectsMap!!
        levelGenerator.GenerateTilesWorldMap(tilesWorldMap, wallTilesMap, objectsMap);
        chunkService.SetWallMap(wallTilesMap);
        chunkService.CreateChunksFromMaps(tilesWorldMap, chunkSize);
    }
    private void CreatePlayer() {
        player = Instantiate((GameObject)Resources.Load("Prefabs/Characters/Player/Player"), new Vector3(0, 0, 0), transform.rotation);
        tile_selector.GetComponent<TileSelector>().Init(player, this, wallTilesMap, tilesWorldMap, tilesObjetMap);
    }
    public void AddItem(int posX, int posY, InventoryItem item) {
        var id = item.config.GetId();
        // toDo faire un pool d'objet déjà instancié!
        var go = Instantiate((GameObject)Resources.Load("Prefabs/Items/item_" + id), new Vector3(posX + 0.5f, posY + 0.5f, 0), transform.rotation);
        tilesObjetMap[posX, posY] = go;
        if (id == 11) {
            lightService.RecursivAddNewLight(posX, posY, 0);
            RefreshLight(CycleDay.GetIntensity());
        }
    }
    public void DeleteItem(int posX, int posY) {
        if (tilesObjetMap[posX, posY].name == "item_11(Clone)") { // toDo changer cette merde
            lightService.RecursivDeleteLight(posX, posY, true, tilesObjetMap);
            RefreshLight(CycleDay.GetIntensity());
        }
        tilesObjetMap[posX, posY] = null;
        Destroy(tilesObjetMap[posX, posY]);
        ManageItems.CreateItemOnMap(posX, posY, 11);
    }
    public void DeleteTile(int x, int y) {
        var id = tilesWorldMap[x, y];
        Chunk currentChunk = ManageChunkTile(x, y, 0);
        currentChunk.SetTile(new Vector3Int(x % chunkSize, y % chunkSize, 0), null);
        lightService.RecursivDeleteShadow(x, y);
        RefreshLight(CycleDay.GetIntensity());
        ManageItems.CreateItemOnMap(x, y, id);
        RefreshChunkNeightboorTiles(x, y, currentChunk.tilemapTile);
    }
    public void AddTile(int x, int y, int id) {
        Chunk currentChunk = ManageChunkTile(x, y, id);
        currentChunk.SetTile(new Vector3Int(x % chunkSize, y % chunkSize, 0), tilebaseDictionary[id]);
        lightService.RecursivAddShadow(x, y, tilesObjetMap);
        RefreshLight(CycleDay.GetIntensity());
        RefreshChunkNeightboorTiles(x, y, currentChunk.tilemapTile);
    }
    private Chunk ManageChunkTile(int x, int y, int id) {
        int chunkX = (int)x / chunkSize;
        int chunkY = (int)y / chunkSize;
        var tilemap = chunkService.GetTilesMapChunks(chunkX, chunkY);
        tilemap[x % chunkSize, y % chunkSize] = id;
        tilesWorldMap[x, y] = id;
        return chunkService.GetChunk(chunkX, chunkY);
    }
    public void RefreshChunkNeightboorTiles(int x, int y, Tilemap tilemap) {
        var topBoundMap = chunkSize - 1;
        var rightBoundMap = chunkSize - 1;
        var posYInMap = y % chunkSize;
        var posXInMap = x % chunkSize;
        // tile position on tilemap
        bool yTopInMap = posYInMap == chunkSize - 1;
        bool yBottomInMap = posYInMap == 0;
        bool xRightInMap = posXInMap == chunkSize - 1;
        bool xLeftInMap = posXInMap == 0;
        int chunkPosX = (int)x / chunkSize;
        int chunkPosY = (int)y / chunkSize;
        // top //
        if (yTopInMap) {
            Chunk topChunk = chunkService.GetChunk(chunkPosX, chunkPosY + 1);
            // if diagonal left top
            if (xLeftInMap) {
                Chunk leftChunk = chunkService.GetChunk(chunkPosX - 1, chunkPosY);
                Chunk diagLeftTopChunk = chunkService.GetChunk(chunkPosX - 1, chunkPosY + 1);
                if (leftChunk) {
                    leftChunk.tilemapTile.RefreshTile(new Vector3Int(rightBoundMap, topBoundMap, 0));
                    leftChunk.tilemapTile.RefreshTile(new Vector3Int(rightBoundMap, topBoundMap - 1, 0));
                }
                if (diagLeftTopChunk) {
                    diagLeftTopChunk.tilemapTile.RefreshTile(new Vector3Int(rightBoundMap, 0, 0));
                }
                if (topChunk) {
                    topChunk.tilemapTile.RefreshTile(new Vector3Int(0, 0, 0));
                    topChunk.tilemapTile.RefreshTile(new Vector3Int(1, 0, 0));
                }
            } else if (xRightInMap) {
                // if diagonal right top
                Chunk rightChunk = chunkService.GetChunk(chunkPosX + 1, chunkPosY);
                Chunk diagRightTopChunk = chunkService.GetChunk(chunkPosX + 1, chunkPosY + 1);
                if (rightChunk) {
                    rightChunk.tilemapTile.RefreshTile(new Vector3Int(0, topBoundMap, 0));
                    rightChunk.tilemapTile.RefreshTile(new Vector3Int(0, topBoundMap - 1, 0));
                }
                if (diagRightTopChunk) {
                    diagRightTopChunk.tilemapTile.RefreshTile(new Vector3Int(0, 0, 0));
                }
                if (topChunk) {
                    topChunk.tilemapTile.RefreshTile(new Vector3Int(rightBoundMap, 0, 0));
                    topChunk.tilemapTile.RefreshTile(new Vector3Int(rightBoundMap - 1, 0, 0));
                }
            } else {
                // if just top
                if (topChunk) {
                    topChunk.tilemapTile.RefreshTile(new Vector3Int(posXInMap, 0, 0));
                    topChunk.tilemapTile.RefreshTile(new Vector3Int(posXInMap - 1, 0, 0));
                    topChunk.tilemapTile.RefreshTile(new Vector3Int(posXInMap + 1, 0, 0));
                }
            }
        } else {
            // 3 top tiles in current chunk
            tilemap.RefreshTile(new Vector3Int(posXInMap, posYInMap + 1, 0));
            tilemap.RefreshTile(new Vector3Int(posXInMap + 1, posYInMap + 1, 0));
            tilemap.RefreshTile(new Vector3Int(posXInMap - 1, posYInMap + 1, 0));
        }
        if (yBottomInMap) {
            // bottom //
            Chunk bottomChunk = chunkService.GetChunk(chunkPosX, chunkPosY - 1);
            if (xLeftInMap) {
                // if diagonal left bottom
                Chunk leftBottomChunk = chunkService.GetChunk(chunkPosX - 1, chunkPosY);
                Chunk diagLeftBottomChunk = chunkService.GetChunk(chunkPosX - 1, chunkPosY - 1);
                if (leftBottomChunk) {
                    leftBottomChunk.tilemapTile.RefreshTile(new Vector3Int(rightBoundMap, 0, 0));
                    leftBottomChunk.tilemapTile.RefreshTile(new Vector3Int(rightBoundMap, 1, 0));
                }
                if (diagLeftBottomChunk) {
                    diagLeftBottomChunk.tilemapTile.RefreshTile(new Vector3Int(rightBoundMap, topBoundMap, 0));
                }
                if (bottomChunk) {
                    bottomChunk.tilemapTile.RefreshTile(new Vector3Int(0, topBoundMap, 0));
                    bottomChunk.tilemapTile.RefreshTile(new Vector3Int(1, topBoundMap, 0));
                }
            } else if (xRightInMap) {
                // if diagonal right bottom
                Chunk rightBottomChunk = chunkService.GetChunk(chunkPosX + 1, chunkPosY);
                Chunk diagRightBottomChunk = chunkService.GetChunk(chunkPosX + 1, chunkPosY - 1);
                if (rightBottomChunk) {
                    rightBottomChunk.RefreshTile(new Vector3Int(0, 0, 0));
                    rightBottomChunk.RefreshTile(new Vector3Int(0, 1, 0));
                }
                if (diagRightBottomChunk) {
                    diagRightBottomChunk.RefreshTile(new Vector3Int(0, topBoundMap, 0));
                }
                if (bottomChunk) {
                    bottomChunk.RefreshTile(new Vector3Int(rightBoundMap, topBoundMap, 0));
                    bottomChunk.RefreshTile(new Vector3Int(rightBoundMap - 1, topBoundMap, 0));
                }
            } else {
                // just bottom //
                if (bottomChunk) {
                    bottomChunk.tilemapTile.RefreshTile(new Vector3Int(posXInMap, topBoundMap, 0));
                    bottomChunk.tilemapTile.RefreshTile(new Vector3Int(posXInMap - 1, topBoundMap, 0));
                    bottomChunk.tilemapTile.RefreshTile(new Vector3Int(posXInMap + 1, topBoundMap, 0));
                }
            }
        } else {
            // 3 bottom tiles in current chunk
            tilemap.RefreshTile(new Vector3Int(posXInMap, posYInMap - 1, 0));
            tilemap.RefreshTile(new Vector3Int(posXInMap - 1, posYInMap - 1, 0));
            tilemap.RefreshTile(new Vector3Int(posXInMap + 1, posYInMap - 1, 0));
        }
        if (xLeftInMap) {
            // just left //
            Chunk leftChunk = chunkService.GetChunk(chunkPosX - 1, chunkPosY);
            if (leftChunk) {
                leftChunk.tilemapTile.RefreshTile(new Vector3Int(rightBoundMap, posYInMap, 0));
                leftChunk.tilemapTile.RefreshTile(new Vector3Int(rightBoundMap, posYInMap - 1, 0));
                leftChunk.tilemapTile.RefreshTile(new Vector3Int(rightBoundMap, posYInMap + 1, 0));
            }
        } else {
            tilemap.RefreshTile(new Vector3Int(posXInMap - 1, posYInMap, 0));
        }
        if (xRightInMap) {
            // just right
            Chunk rightChunk = chunkService.GetChunk(chunkPosX + 1, chunkPosY);
            if (rightChunk) {
                rightChunk.tilemapTile.RefreshTile(new Vector3Int(0, posYInMap, 0));
                rightChunk.tilemapTile.RefreshTile(new Vector3Int(0, posYInMap - 1, 0));
                rightChunk.tilemapTile.RefreshTile(new Vector3Int(0, posYInMap + 1, 0));
            }
        } else {
            tilemap.RefreshTile(new Vector3Int(posXInMap + 1, posYInMap, 0));
        }
    }
}
