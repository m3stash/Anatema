﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour {

    private Renderer render;
    private TilemapCollider2D tc2d;
    public TileMapScript tileMapTileMapScript;
    public TileMapScript wallTileMapScript;
    public Tilemap tilemapTile;
    public Tilemap tilemapWall;
    public int[,] tilesMap;
    public GameObject player;
    public Vector2Int chunkPosition;
    public Vector2Int worldPosition;
    private bool firstInitialisation = true;
    private bool alreadyVisible = false;
    private int chunkSize;
    [SerializeField] private List<Item> items;

    private void OnEnable() {
        CycleDay.RefreshIntensity += RefreshShadowMap;
        WorldManager.RefreshLight += RefreshShadowMap;
        DynamicLight.RefreshLight += RefreshShadowMap;
        Item.OnItemMoved += OnItemMoved;
        Item.OnItemDestroyed += OnItemDestroyed;
        if (!firstInitialisation) {
            RefreshTiles();
        }
    }

    private void OnItemMoved(Item item) {
        int itemPosX = (int)item.transform.position.x / WorldManager.GetChunkSize();
        int itemPosY = (int)item.transform.position.y / WorldManager.GetChunkSize();
        int itemIdxFound = this.items.FindIndex(elem => elem.GetInstanceID() == item.GetInstanceID());

        // Add item to this chunk if it's inside position interval and it's not already referenced
        if (itemPosX == chunkPosition.x && itemPosY == chunkPosition.y) {
            if (itemIdxFound == -1) {
                this.items.Add(item);
            }
        } else if (itemIdxFound != -1) { // Else delete it if it was referenced
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

        if (itemIdxFound != -1) {
            this.items.RemoveAt(itemIdxFound);
        }
    }

    private void generateObjectsMap() {
        for (var x = 0; x < WorldManager.GetChunkSize(); x++) {
            for (var y = 0; y < WorldManager.GetChunkSize(); y++) {
                // toDo refacto is just a poc
                if (WorldManager.objectsMap[worldPosition.x + x, worldPosition.y + y] == 31) {
                    Item item = ItemManager.instance.CreateItem(31, ItemStatus.ACTIVE, new Vector3(worldPosition.x + x, worldPosition.y + y));
                    this.items.Add(item);
                }
            }
        }
    }

    private void RefreshShadowMap() {
        if (!render || !render.isVisible)
            return;
        var intensity = CycleDay.GetIntensity();
        for (var x = 0; x < WorldManager.GetChunkSize(); x++) {
            for (var y = 0; y < WorldManager.GetChunkSize(); y++) {
                Vector3Int vec3 = new Vector3Int(x, y, 0);
                if (tilesMap[x, y] > 0 || WorldManager.wallTilesMap[worldPosition.x + x, worldPosition.y + y] > 0) {
                    var shadow = WorldManager.tilesShadowMap[worldPosition.x + x, worldPosition.y + y] + intensity;
                    var light = WorldManager.tilesLightMap[worldPosition.x + x, worldPosition.y + y];
                    float l;
                    if (light <= shadow && light < 100) {
                        l = 1 - light * 0.01f;
                    } else {
                        l = 1 - shadow * 0.01f;
                    }
                    Color c = new Color(l, l, l, 1);
                    tilemapWall.SetColor(vec3, c);
                    tilemapTile.SetColor(vec3, c);
                }
            }
        }
    }

    private void OnDisable() {
        ItemManager.instance.DestroyItems(this.items.ToArray());
        tilemapWall.ClearAllTiles();
        tilemapTile.ClearAllTiles(); // for tile not refresh (display with bad sprite number!!)
        CycleDay.RefreshIntensity -= RefreshShadowMap;
        WorldManager.RefreshLight -= RefreshShadowMap;
        DynamicLight.RefreshLight -= RefreshShadowMap;
        Item.OnItemMoved -= OnItemMoved;
        Item.OnItemDestroyed -= OnItemDestroyed;
        alreadyVisible = false;
    }

    private void RefreshTiles() {
        Vector3Int[] positions = new Vector3Int[chunkSize * chunkSize];
        TileBase[] tileArray = new TileBase[positions.Length];
        TileBase[] tileArrayWall = new TileBase[positions.Length];
        for (int index = 0; index < positions.Length; index++) {
            var x = index % chunkSize;
            var y = index / chunkSize;
            positions[index] = new Vector3Int(x, y, 0);
            var tileBaseIndex = tilesMap[x, y];
            if (tileBaseIndex > 0) {
                tileArray[index] = ChunkService.tilebaseDictionary[tileBaseIndex];
            }
            var tileWallIndex = WorldManager.wallTilesMap[x + worldPosition.x, y + worldPosition.y];
            if (tileWallIndex > 0) {
                tileArrayWall[index] = ChunkService.tilebaseDictionary[tileWallIndex];
            }
        }
        tilemapTile.SetTiles(positions, tileArray);
        tilemapWall.SetTiles(positions, tileArrayWall);
    }
    void Start() {
        chunkSize = WorldManager.GetChunkSize();
        this.items = new List<Item>();
        render = tilemapTile.GetComponent<Renderer>();
        // !! call just one time afer instanciate !!
        InitColliders();
        firstInitialisation = false;
        RefreshTiles();
    }
    private void InitColliders() {
        tc2d = GetComponentInChildren<TilemapCollider2D>();
        tc2d.enabled = false;
        var bc2d = GetComponentInChildren<BoxCollider2D>();
        bc2d.offset = new Vector2(WorldManager.GetChunkSize() / 2, WorldManager.GetChunkSize() / 2);
        bc2d.size = new Vector2(WorldManager.GetChunkSize(), WorldManager.GetChunkSize());
    }
    public void SetTile(Vector3Int vector3, TileBase tilebase) {
        tilemapTile.SetTile(vector3, tilebase);
    }
    public void RefreshTile(Vector3Int vector3) {
        tilemapTile.RefreshTile(vector3);
    }

    void Update() {
        if (render.isVisible && !alreadyVisible) {
            alreadyVisible = true;
            tc2d.enabled = true;
            generateObjectsMap(); // toDO revoir orchestration item manager
            RefreshShadowMap();
        }
    }
}