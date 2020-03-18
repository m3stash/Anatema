using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System;
using Cinemachine;

public class WorldManager : MonoBehaviour {

    public static WorldManager instance;

    public Dictionary<int, TileBase> tilebaseDictionary;
    private ChunkService chunkService;
    private LightService lightService;
    private GameObject player;
    public int[,] tilesLightMap;
    public int[,] tilesShadowMap;
    public int[,] tilesWorldMap;
    public int[,] wallTilesMap;
    public int[,] objectsMap;
    public int[,] dynamicLight;
    private int worldSizeX;
    private int worldSizeY;
    private int chunkSize;
    [Header("Main Settings")]
    [SerializeField] public TileBase_cfg tilebase_cfg;
    [SerializeField] private MapType mapType;
    private bool worldManagerIsInit = false;
    // event
    public delegate void LightEventHandler();
    public static event LightEventHandler RefreshLight;
    public delegate void PlayerLoaded(GameObject player);
    public static event PlayerLoaded GetPlayer;

    /* private void InitFolders() {
        FileManager.ManageFolder("chunk-data");
    } */

    private void Awake() {
        instance = this;
        GetComponents();
    }

    public bool MapIsInit() {
        return worldManagerIsInit;
    }

    public int GetChunkSize() {
        return chunkSize;
    }

    public void GetComponents() {
        chunkService = gameObject.GetComponent<ChunkService>();
        lightService = GetComponent<LightService>();
    }

    private void GetAndSetValueFromMapSerialisable() {
        MapSerialisable mapConf = GameMaster.instance.GetMapDatabaseByMapType(mapType);
        tilesLightMap = mapConf.tilesLightMap;
        tilesShadowMap = mapConf.tilesShadowMap;
        tilesWorldMap = mapConf.tilesWorldMap;
        wallTilesMap = mapConf.wallTilesMap;
        objectsMap = mapConf.objectsMap;
        dynamicLight = mapConf.dynamicLight;
        worldSizeX = mapConf.mapWidth;
        worldSizeY = mapConf.mapHeight;
        chunkSize = mapConf.chunkSize;
    }

    void Start() {
        tilebaseDictionary = tilebase_cfg.GetDico();
        GetAndSetValueFromMapSerialisable();
        CreatePlayer();
        lightService.Init();
        chunkService.Init(tilebaseDictionary, player);
        worldManagerIsInit = true;
        GetPlayer(player);
    }

    private void CreatePlayer() {
        player = Instantiate((GameObject)Resources.Load("Prefabs/Characters/Player/Player"), new Vector3(0, 0, 0), transform.rotation);
        FindObjectOfType<CinemachineVirtualCamera>().Follow = player.transform; // toDo bouger er utiliser le GetPlayer event!
    }

    public void AddItem(Vector2Int pos, InventoryItemData item) {
        // Fill objects map with item id for origin cell and -1 for adjacent cells
        foreach (CellCollider cell in item.GetConfig().GetColliderConfig().GetCellColliders()) {
            objectsMap[pos.x + cell.GetRelativePosition().x, pos.y + cell.GetRelativePosition().y] = cell.IsOrigin() ? item.GetConfig().GetId() : -1;
        }

        ItemManager.instance.CreateItem(item.GetConfig().GetId(), ItemStatus.ACTIVE, new Vector3(pos.x, pos.y, 0));
        // toDo voir a rajouter le bloc en dessous uniquement pour des lights statiques les autres auront le script dynamic light
        /*if (config.CanEmitLight()) {
            LightService.RecursivAddNewLight(posX, posY, 0);
            RefreshLight();
        }*/
    }

    public void DeleteItem(Item item) {
        int posX = (int)item.transform.position.x;
        int posY = (int)item.transform.position.y;

        // Delete lights if item can emit light
        if (item.GetConfig().CanEmitLight()) {
            lightService.RecursivDeleteLight(posX, posY, true);
            RefreshLight();
        }

        // Spawn pickable item
        ItemManager.instance.CreateItem(objectsMap[posX, posY], ItemStatus.PICKABLE, new Vector3(posX + 0.5f, posY + 0.5f));

        // Remove allocations from objectsMap array
        foreach (CellCollider cellToCheck in item.GetConfig().GetColliderConfig().GetCellColliders()) {
            objectsMap[posX + cellToCheck.GetRelativePosition().x, posY + cellToCheck.GetRelativePosition().y] = 0;
        }

        // Destroy item
        item.Destroy(); // Todo refactor when add dropable system
    }

    public bool IsOutOfBound(int x, int y) {
        return (x < 0 || x > (worldSizeX - 1)) || (y < 0 || y > (worldSizeY - 1));
    }

    public void DeleteTile(int x, int y) {
        var id = tilesWorldMap[x, y];
        Chunk currentChunk = ManageChunkTile(x, y, 0);
        currentChunk.SetTile(new Vector3Int(x % chunkSize, y % chunkSize, 0), null);
        lightService.RecursivDeleteShadow(x, y);
        RefreshLight();
        ItemManager.instance.CreateItem(id, ItemStatus.PICKABLE, new Vector3(x + 0.5f, y + 0.5f)); // Todo 0.5f is equals to an half block size (Refactor it)
        ItemManager.instance.CreateItem(16, ItemStatus.PICKABLE, new Vector3(x + 0.5f, y + 0.5f)); // Todo 0.5f is equals to an half block size (Refactor it)
        ItemManager.instance.CreateItem(14, ItemStatus.PICKABLE, new Vector3(x + 0.5f, y + 0.5f)); // Todo 0.5f is equals to an half block size (Refactor it)
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
        tilesWorldMap[x, y] = id;
        return chunkService.GetChunk((int)x / chunkSize, (int)y / chunkSize);
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
