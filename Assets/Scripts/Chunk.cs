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
    public GameObject[,] tilesObjetMap;
    public GameObject player;
    public Dictionary<int, TileBase> tilebaseDictionary;
    public int indexX;
    public int indexY;
    public int indexXWorldPos;
    public int indexYWorldPos;
    public int chunkSize;
    private bool firstInitialisation = true;
    private bool isChunkVisible = false;
    private bool alreadyVisible = false;

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
    private void RefreshShadowMap(int intensity) {
        if (!isChunkVisible)
            return;
        for (var x = 0; x < chunkSize; x++) {
            for (var y = 0; y < chunkSize; y++) {
                var shadow = tilesShadowMap[indexXWorldPos + x, indexYWorldPos + y] + intensity;
                var light = tilesLightMap[indexXWorldPos + x, indexYWorldPos + y];
                if ((wallTilesMap[indexXWorldPos + x, indexYWorldPos + y] == 0 && tilesMap[x, y] == 0) || (shadow == 0 && light == 0)) {
                    tilemapShadow.SetColor(new Vector3Int(x, y, 0), new Color(0, 0, 0, 0));
                } else {
                    if (light <= shadow && light < 100) {
                        // tilemapShadow.SetColor(new Vector3Int(x, y, 0), new Color(0, 0, 0, 0));
                        tilemapShadow.SetColor(new Vector3Int(x, y, 0), new Color(0, 0, 0, (float)light * 0.01f));
                    } else {
                        tilemapShadow.SetColor(new Vector3Int(x, y, 0), new Color(0, 0, 0, (float)shadow * 0.01f));
                        // tilemapShadow.SetColor(new Vector3Int(x, y, 0), new Color(0, 0, 0, 0));
                    }
                }
            }
        }
    }
    private void OnDisable() {
        tilemapTile.ClearAllTiles(); // for tile not refresh (display with bad sprite number!!)
        alreadyVisible = false;
        WorldManager.RefreshLight -= RefreshShadowMap;
        CycleDay.RefreshIntensity -= RefreshShadowMap;
        ChunkService.RefreshLight -= RefreshShadowMap;
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
            if (wallTilesMap[x + indexXWorldPos, y + indexYWorldPos] > 0) {
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
            RefreshShadowMap(CycleDay.GetIntensity());
        }
    }
}