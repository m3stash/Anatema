using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour {

    public LightService lightService;
    private TilemapCollider2D tc2d;
    public TileMapScript tileMapTileMapScript;
    public TileMapScript wallTileMapScript;
    public TileMapScript shadowTileMapScript;
    public Tilemap tilemapTile;
    public Tilemap tilemapWall;
    public Tilemap tilemapShadow;
    public int[,] wallTilesMap;
    public int[,] tilesMap;
    public int[,] tilesLightMap;
    public int[,] tilesShadowMap;
    public int[,] objectsMap;
    public GameObject[,] tilesObjetMap;
    public GameObject player;
    public Dictionary<int, TileBase> tilebaseDictionary;
    public Vector2Int chunkPosition;
    public Vector2Int worldPosition;
    public int chunkSize;
    private bool firstInitialisation = true;
    private bool isChunkVisible = false;
    private bool alreadyVisible = false;

    [SerializeField] private List<Item> items;

    private void OnEnable() {
        CycleDay.RefreshIntensity += RefreshShadowMap;
        WorldManager.RefreshLight += RefreshShadowMap;
        ChunkService.RefreshLight += RefreshShadowMap;
        if (!firstInitialisation) {
            tileMapTileMapScript.hasAlreadyInit = true;
            wallTileMapScript.hasAlreadyInit = true;
            RefreshTiles();
        }
    }

    private void OnItemCreated(Item item) {
        int itemPosX = (int)item.transform.position.x / this.chunkSize;
        int itemPosY = (int)item.transform.position.y / this.chunkSize;

     
        if(itemPosX == chunkPosition.x && itemPosY == chunkPosition.y) {
            this.items.Add(item);
            Debug.Log("Item added to chunk name : " + this.name);
        }
    }

    private void generateObjectsMap() {
        for (var x = 0; x < chunkSize; x++) {
            for (var y = 0; y < chunkSize; y++) {
                // toDo refacto is just a poc
                if(objectsMap[worldPosition.x + x, worldPosition.y + y] == 22) {
                    Item item = ItemManager.instance.CreateItem(6, ItemStatus.ACTIVE, new Vector3(worldPosition.x + x, worldPosition.y + y));
                    this.items.Add(item);
                }
            }
        }
    }

    private void RefreshShadowMap(int intensity) {
        for (var x = 0; x < chunkSize; x++) {
            for (var y = 0; y < chunkSize; y++) {
                var shadow = tilesShadowMap[worldPosition.x + x, worldPosition.y + y] + intensity;
                var light = tilesLightMap[worldPosition.x + x, worldPosition.y + y];
                if ((wallTilesMap[worldPosition.x + x, worldPosition.y + y] == 0 && tilesMap[x, y] == 0) || (shadow == 0 && light == 0)) {
                    tilemapShadow.SetColor(new Vector3Int(x, y, 0), new Color(0, 0, 0, 0));
                } else {
                    if (light <= shadow && light < 100) {
                        tilemapShadow.SetColor(new Vector3Int(x, y, 0), new Color(0, 0, 0, (float)light * 0.01f));
                    } else {
                        tilemapShadow.SetColor(new Vector3Int(x, y, 0), new Color(0, 0, 0, (float)shadow * 0.01f));
                    }
                }
            }
        }
    }
    private void OnDisable() {
        this.ClearItems();
        tilemapWall.ClearAllTiles();
        tilemapTile.ClearAllTiles(); // for tile not refresh (display with bad sprite number!!)
        alreadyVisible = false;
        CycleDay.RefreshIntensity -= RefreshShadowMap;
        WorldManager.RefreshLight -= RefreshShadowMap;
        ChunkService.RefreshLight -= RefreshShadowMap;
    }

    private void ClearItems() {
        foreach(Item item in this.items) {
            item.Destroy();
        }
    }

    private void RefreshTiles() {
        Vector3Int[] positions = new Vector3Int[chunkSize * chunkSize];
        TileBase[] tileArray = new TileBase[positions.Length];
        TileBase[] tileArrayShadow = new TileBase[positions.Length];
        TileBase[] tileArrayWall = new TileBase[positions.Length];
        for (int index = 0; index < positions.Length; index++) {
            var x = index % chunkSize;
            var y = index / chunkSize;
            positions[index] = new Vector3Int(x, y, 0);
            var tileBaseIndex = tilesMap[x, y];
            if (tileBaseIndex > 0) {
                tileArray[index] = tilebaseDictionary[tileBaseIndex];
            } else {
                tileArray[index] = null;
            }
            if (tilesShadowMap[x, y] > 0) {
                tileArrayShadow[index] = tilebaseDictionary[-1];
            } else {
                tileArrayShadow[index] = null;
            }
            if (wallTilesMap[x + worldPosition.x, y + worldPosition.y] > 0) {
                tileArrayWall[index] = tilebaseDictionary[7];
            } else {
                tileArrayWall[index] = null;
            }
        }
        tilemapTile.SetTiles(positions, tileArray);
        tilemapShadow.SetTiles(positions, tileArrayShadow);
        tilemapWall.SetTiles(positions, tileArrayWall);
    }
    void Start() {
        // !! call just one time afer instanciate !!
        InitColliders();
        firstInitialisation = false;
        RefreshTiles();
        tileMapTileMapScript.hasAlreadyInit = true;
        wallTileMapScript.hasAlreadyInit = true;
        this.items = new List<Item>();
    }
    private void InitColliders() {
        tc2d = GetComponentInChildren<TilemapCollider2D>();
        tc2d.enabled = false;
        var bc2d = GetComponentInChildren<BoxCollider2D>();
        bc2d.offset = new Vector2(chunkSize / 2, chunkSize / 2);
        bc2d.size = new Vector2(chunkSize, chunkSize);
    }
    public void SetTile(Vector3Int vector3, TileBase tilebase) {
        tilemapTile.SetTile(vector3, tilebase);
    }
    public void RefreshTile(Vector3Int vector3) {
        tilemapTile.RefreshTile(vector3);
    }
    public void ChunckVisible(bool isVisible) {
        // to to rajouter un test pour ne plus passer par ici si le chunk est visible et a déjà été activé!!!!!!!!!!!!!!!!!!
        isChunkVisible = isVisible;
        if (isVisible && !alreadyVisible) {
            alreadyVisible = true;
            tc2d.enabled = true;
            generateObjectsMap();
            RefreshShadowMap(CycleDay.GetIntensity());
        }
    }
}