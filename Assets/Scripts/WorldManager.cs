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
    public static int[,] tilesLightMap;
    public static int[,] tilesShadowMap;
    public static int[,] tilesWorldMap;
    public static int[,] wallTilesMap;
    public static int[,] objectsMap;
    public static int[,] dynamicLight;
    public static Dictionary<int, TileBase> tilebaseDictionary;
    private Sprite[] block_sprites;
    public TileBase_cfg tilebase_cfg;
    public static int chunkSize;
    // event
    public delegate void LightEventHandler();
    public static event LightEventHandler RefreshLight;
    public delegate void PlayerLoaded(GameObject player);
    public static event PlayerLoaded GetPlayer;
    // SerializeField
    [SerializeField] private int worldSizeX;
    [SerializeField] private int worldSizeY;
    [SerializeField]private int chunkSizeField; 
    private void InitFolders() {
        FileManager.ManageFolder("chunk-data");
    }

    void Start() {
        chunkSize = chunkSizeField;
        InitFolders();
        InitResources();
        CreateWorldMap();
        CreateLightMap();
        CreatePlayer();
        LightService.Init();
        chunkService.Init(tilebaseDictionary, player);
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
        dynamicLight = new int[worldSizeX, worldSizeY];
        levelGenerator.GenerateTilesWorldMap(tilesWorldMap, wallTilesMap, objectsMap);
        chunkService.CreateChunksFromMaps(tilesWorldMap);
    }
    private void CreatePlayer() {
        player = Instantiate((GameObject)Resources.Load("Prefabs/Characters/Player/Player"), new Vector3(0, 0, 0), transform.rotation);
        tile_selector.GetComponent<TileSelector>().Init(player, this, wallTilesMap, tilesWorldMap);
    }
    public void AddItem(int posX, int posY, InventoryItem item) {
        WorldManager.objectsMap[posX, posY] = 7;
        FurnitureItem newItem = ItemManager.instance.CreateItem(7, ItemStatus.ACTIVE, new Vector3(posX + 0.5f, posY + 0.5f, 0)) as FurnitureItem;
        FurnitureConfig config = newItem.GetConfig() as FurnitureConfig;
        // toDo voir a rajouter le bloc en dessous uniquement pour des lights statiques les autres auront le script dynamic light
        /*if (config.CanEmitLight()) {
            LightService.RecursivAddNewLight(posX, posY, 0);
            RefreshLight();
        }*/
    }
    public void DeleteItem(int posX, int posY) {
        if (objectsMap[posX, posY] == 7) { // toDo changer cette merde
            LightService.RecursivDeleteLight(posX, posY, true);
            RefreshLight();
        }
        ItemManager.instance.CreateItem(7, ItemStatus.PICKABLE, new Vector3(posX, posY));
    }
    public void DeleteTile(int x, int y) {
        var id = tilesWorldMap[x, y];
        Chunk currentChunk = ManageChunkTile(x, y, 0);
        currentChunk.SetTile(new Vector3Int(x % chunkSize, y % chunkSize, 0), null);
        lightService.RecursivDeleteShadow(x, y);
        RefreshLight();
        ItemManager.instance.CreateItem(id, ItemStatus.PICKABLE, new Vector3(x + 0.5f, y + 0.5f)); // Todo 0.5f is equals to an half block size (Refactor it)
        ItemManager.instance.CreateItem(7, ItemStatus.PICKABLE, new Vector3(x + 0.5f, y + 0.5f)); // Todo 0.5f is equals to an half block size (Refactor it)
        RefreshChunkNeightboorTiles(x, y, currentChunk.tilemapTile);
    }
    public void AddTile(int x, int y, int id) {
        Chunk currentChunk = ManageChunkTile(x, y, id);
        currentChunk.SetTile(new Vector3Int(x % chunkSize, y % chunkSize, 0), tilebaseDictionary[id]);
        lightService.RecursivAddShadow(x, y);
        RefreshLight();
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
