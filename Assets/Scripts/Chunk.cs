using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour {

    private TilemapCollider2D tc2d;
    public TileMapScript tileMapTileMapScript;
    public TileMapScript wallTileMapScript;
    public TileMapScript shadowTileMapScript;
    public Tilemap tilemapTile;
    public Tilemap tilemapWall;
    public Tilemap tilemapShadow;
    public int[,] tilesMap;
    public GameObject player;
    public Vector2Int chunkPosition;
    public Vector2Int worldPosition;
    private bool firstInitialisation = true;
    private bool isChunkVisible = false;
    private bool alreadyVisible = false;

    [SerializeField] private List<Item> items;

    private void OnEnable() {
        CycleDay.RefreshIntensity += RefreshShadowMap;
        WorldManager.RefreshLight += RefreshShadowMap;
        DynamicLight.RefreshLight += RefreshShadowMap;
        Item.OnItemMoved += OnItemMoved;
        Item.OnItemDestroyed += OnItemDestroyed;
        if(!firstInitialisation) {
            tileMapTileMapScript.hasAlreadyInit = true;
            wallTileMapScript.hasAlreadyInit = true;
            RefreshTiles();
        }
    }

    private void OnItemMoved(Item item) {
        int itemPosX = (int)item.transform.position.x / WorldManager.chunkSize;
        int itemPosY = (int)item.transform.position.y / WorldManager.chunkSize;
        int itemIdxFound = this.items.FindIndex(elem => elem.GetInstanceID() == item.GetInstanceID());

        // Add item to this chunk if it's inside position interval and it's not already referenced
        if(itemPosX == chunkPosition.x && itemPosY == chunkPosition.y) {
            if(itemIdxFound == -1) {
                this.items.Add(item);
            }
        } else if(itemIdxFound != -1) { // Else delete it if it was referenced
            this.items.RemoveAt(itemIdxFound);

        }
    }

    /// <summary>
    /// Called when an item is destroyed
    /// If it was referenced in my items so remove it
    /// </summary>
    /// <param name="item"></param>
    private void OnItemDestroyed(Item item) {
        int itemIdxFound = this.items.FindIndex(elem => elem.GetInstanceID() == item.GetInstanceID());

        if(itemIdxFound != -1) {
            this.items.RemoveAt(itemIdxFound);
        }
    }

    private void generateObjectsMap() {
        for(var x = 0; x < WorldManager.chunkSize; x++) {
            for(var y = 0; y < WorldManager.chunkSize; y++) {
                // toDo refacto is just a poc
                if(WorldManager.objectsMap[worldPosition.x + x, worldPosition.y + y] == 22) {
                    Item item = ItemManager.instance.CreateItem(6, ItemStatus.ACTIVE, new Vector3(worldPosition.x + x, worldPosition.y + y));
                    this.items.Add(item);
                }
            }
        }
    }

    private void RefreshShadowMap() {
        /*if (!isChunkVisible) // toDO voie à améliorer ça !
            return;*/
        var intensity = CycleDay.GetIntensity();
        for (var x = 0; x < WorldManager.chunkSize; x++) {
            for(var y = 0; y < WorldManager.chunkSize; y++) {
                var shadow = WorldManager.tilesShadowMap[worldPosition.x + x, worldPosition.y + y] + intensity;
                var light = WorldManager.tilesLightMap[worldPosition.x + x, worldPosition.y + y];
                if((WorldManager.wallTilesMap[worldPosition.x + x, worldPosition.y + y] == 0 && tilesMap[x, y] == 0) || (shadow == 0 && light == 0)) {
                    tilemapShadow.SetColor(new Vector3Int(x, y, 0), new Color(0, 0, 0, 0));
                } else {
                    if(light <= shadow && light < 100) {
                        tilemapShadow.SetColor(new Vector3Int(x, y, 0), new Color(0, 0, 0, (float)light * 0.01f));
                    } else {
                        tilemapShadow.SetColor(new Vector3Int(x, y, 0), new Color(0, 0, 0, (float)shadow * 0.01f));
                    }
                }
            }
        }
    }

    private void OnDisable() {
        ItemManager.instance.DestroyItems(this.items.ToArray());
        tilemapWall.ClearAllTiles();
        tilemapTile.ClearAllTiles(); // for tile not refresh (display with bad sprite number!!)
        alreadyVisible = false;
        CycleDay.RefreshIntensity -= RefreshShadowMap;
        WorldManager.RefreshLight -= RefreshShadowMap;
        DynamicLight.RefreshLight -= RefreshShadowMap;
        Item.OnItemMoved -= OnItemMoved;
        Item.OnItemDestroyed -= OnItemDestroyed;
    }

    private void RefreshTiles() {
        Vector3Int[] positions = new Vector3Int[WorldManager.chunkSize * WorldManager.chunkSize];
        TileBase[] tileArray = new TileBase[positions.Length];
        TileBase[] tileArrayShadow = new TileBase[positions.Length];
        TileBase[] tileArrayWall = new TileBase[positions.Length];
        for(int index = 0; index < positions.Length; index++) {
            var x = index % WorldManager.chunkSize;
            var y = index / WorldManager.chunkSize;
            positions[index] = new Vector3Int(x, y, 0);
            var tileBaseIndex = tilesMap[x, y];
            if(tileBaseIndex > 0) {
                tileArray[index] = ChunkService.tilebaseDictionary[tileBaseIndex];
            } else {
                tileArray[index] = null;
            }
            if(WorldManager.tilesShadowMap[x, y] > 0) {
                tileArrayShadow[index] = ChunkService.tilebaseDictionary[-1];
            } else {
                tileArrayShadow[index] = null;
            }
            if(WorldManager.wallTilesMap[x + worldPosition.x, y + worldPosition.y] > 0) {
                tileArrayWall[index] = ChunkService.tilebaseDictionary[7];
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
        // var bc2d = GetComponentInChildren<BoxCollider2D>();
        // bc2d.offset = new Vector2(WorldManager.chunkSize / 2, WorldManager.chunkSize / 2);
        // bc2d.size = new Vector2(WorldManager.chunkSize, WorldManager.chunkSize);
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
        if(isVisible && !alreadyVisible) {
            alreadyVisible = true;
            tc2d.enabled = true;
            generateObjectsMap();
            RefreshShadowMap();
        }
    }
}