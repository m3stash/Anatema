using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkPos {
    public int x;
    public int y;
    public ChunkPos(int x, int y) {
        this.x = x;
        this.y = y;
    }
}

public class ChunkService : MonoBehaviour {

    public int numberOfPool;
    private int chunkSize;
    private GameObject player;
    private Camera playerCam;
    private int[,][,] tilesMapChunks;
    private int[,] tilesLightMap;
    private int[,] tilesWorldMap;
    private int[,] wallTilesMap;
    private int[,] tilesShadowMap;
    private Dictionary<int, TileBase> tilebaseDictionary;
    private Transform worldMapTransform;
    private float waitingTimeAfterCreateChunk = 0.1f;
    private List<Chunk> unUsedChunk = new List<Chunk>();
    private List<Chunk> usedChunk = new List<Chunk>();
    private int halfChunk;
    private int boundX;
    private int boundY;
    private LightService lightService;
    private int currentPlayerChunkX;
    private int currentPlayerChunkY;
    private readonly int maxChunkGapWithPlayerX = 6;
    private readonly int maxChunkGapWithPlayerY = 4;
    private string chunkDirectory = "chunk-data";
    private ChunkDataModel[,] cacheChunkData;
    private int chunkXLength;
    private int chunkYLength;
    private int oldPosX;
    private int oldPosY;

    public void FixedUpdate() {
        ManageChunkPoolFromPlayerPos();
    }
    private void ManageChunkPoolFromPlayerPos() {
        currentPlayerChunkX = (int)player.transform.position.x / chunkSize;
        currentPlayerChunkY = (int)player.transform.position.y / chunkSize;
        if (oldPosX != currentPlayerChunkX || oldPosY != currentPlayerChunkY) {
            var chuncksToDesactivate = usedChunk.FindAll(chunk => Mathf.Abs(chunk.indexX - currentPlayerChunkX) >= maxChunkGapWithPlayerX || Mathf.Abs(chunk.indexY - currentPlayerChunkY) >= maxChunkGapWithPlayerY);
            chuncksToDesactivate.ForEach(chunk => PlayerIsTooFar(chunk));
        }
        if (currentPlayerChunkX > oldPosX) { // right
            StartCoroutine(StartPool(currentPlayerChunkX + 1, currentPlayerChunkY));
        } else if (currentPlayerChunkX < oldPosX) { // left
            StartCoroutine(StartPool(currentPlayerChunkX - 1, currentPlayerChunkY));
        }
        if (currentPlayerChunkY > oldPosY) { // top
            StartCoroutine(StartPool(currentPlayerChunkX, currentPlayerChunkY + 1));
        } else if (currentPlayerChunkY < oldPosY) { // bottom
            StartCoroutine(StartPool(currentPlayerChunkX, currentPlayerChunkY - 1));
        }
        oldPosX = currentPlayerChunkX;
        oldPosY = currentPlayerChunkY;
    }
    public void SetWallMap(int[,] map) {
        wallTilesMap = map;
    }
    public int[,] GetTilesMapChunks(int x, int y) {
        return tilesMapChunks[x, y];
    }
    public Chunk GetChunk(int posX, int posY) {
        return usedChunk.Find(chunk => chunk.indexX == posX && chunk.indexY == posY);
    }
    public void Init(int chunkSize, Dictionary<int, TileBase> _tilebaseDictionary, int[,] tilesWorldMap, int[,] tilesLightMap, GameObject player, LightService lightService, int[,] tilesShadowMap) {
        boundX = tilesWorldMap.GetUpperBound(0);
        boundY = tilesWorldMap.GetUpperBound(1);
        playerCam = player.GetComponentInChildren<Camera>();
        halfChunk = chunkSize / 2;
        this.player = player;
        worldMapTransform = GameObject.FindGameObjectWithTag("WorldMap").gameObject.transform;
        tilebaseDictionary = _tilebaseDictionary;
        this.tilesLightMap = tilesLightMap;
        this.tilesWorldMap = tilesWorldMap;
        this.chunkSize = chunkSize;
        this.lightService = lightService;
        this.tilesShadowMap = tilesShadowMap;
        cacheChunkData = new ChunkDataModel[boundX, boundY];
        CreatePoolChunk(20, 52);
    }
    public void CreateChunksFromMaps(int[,] tilesMap, int chunkSize) {
        chunkXLength = (tilesMap.GetUpperBound(0) + 1) / chunkSize;
        chunkYLength = (tilesMap.GetUpperBound(1) + 1) / chunkSize;
        int[,][,] tilesMapChunksArray = new int[chunkXLength, chunkYLength][,];
        for (var chkX = 0; chkX < chunkXLength; chkX++) {
            for (var chkY = 0; chkY < chunkYLength; chkY++) {
                int[,] tileMap = new int[chunkSize, chunkSize];
                for (var x = 0; x < chunkSize; x++) {
                    for (var y = 0; y < chunkSize; y++) {
                        tileMap[x, y] = tilesMap[(chkX * chunkSize) + x, (chkY * chunkSize) + y];
                    }
                }
                tilesMapChunksArray[chkX, chkY] = tileMap;
            }
        }
        tilesMapChunks = tilesMapChunksArray;
    }
    // for debug;
    /*public void RenderPartialMapForTest() {
        GameObject chunk = Instantiate((GameObject)Resources.Load("Prefabs/Chunk"), new Vector3(0, 0, 0), transform.rotation);
        chunk.transform.parent = worldMapTransform;
        Chunk ck = chunk.GetComponent<Chunk>();
        ck.tilesLightMap = tilesLightMap;
        ck.wallTilesMap = wallTilesMap;
        ck.lightService = lightService;
        ck.tilesShadowMap = tilesShadowMap;
        ck.chunkSize = chunkSize;
        ck.tilebaseDictionary = tilebaseDictionary;
        ck.indexX = 0;
        ck.indexY = 0;
    }*/
    public void InitialiseChunkPooling() {
        for (var i = 0; i < numberOfPool; i++) {
            GameObject chunk = Instantiate((GameObject)Resources.Load("Prefabs/Chunk"), new Vector3(0, 0, 0), transform.rotation);
            chunk.transform.parent = worldMapTransform;
            Chunk ck = chunk.GetComponent<Chunk>();
            IsVisible isVisibleScript = ck.tilemapTile.GetComponent<IsVisible>();
            isVisibleScript.cam = playerCam;
            isVisibleScript.chunk = ck;
            ck.tileMapTileMapScript = ck.tilemapTile.GetComponent<TileMapScript>();
            ck.wallTileMapScript = ck.tilemapWall.GetComponent<TileMapScript>();
            ck.shadowTileMapScript = ck.tilemapShadow.GetComponent<TileMapScript>();
            chunk.gameObject.SetActive(false);
            ck.tilesLightMap = tilesLightMap;
            ck.wallTilesMap = wallTilesMap;
            ck.chunkSize = chunkSize;
            ck.lightService = lightService;
            ck.tilebaseDictionary = tilebaseDictionary;
            ck.tilesMap = null;
            ck.tilesShadowMap = tilesShadowMap;
            ck.indexX = -1;
            ck.indexY = -1;
            unUsedChunk.Add(ck);
        }
    }
    public void CreatePoolChunk(int xStart, int yStart) {
        InitialiseChunkPooling();
        // voir à améliorer ça pour faire de l'auto calc sur la range
        for (var x = xStart - 4; x < xStart + 5; x++) {
            for (var y = yStart - 3; y < yStart + 4; y++) {
                ManageChunkFromPool(x, y);
            }
        }
        // spawn player on center start chunk
        oldPosX = xStart;
        oldPosY = yStart;
        player.transform.position = new Vector3(xStart * chunkSize + (chunkSize / 2), yStart * chunkSize + (chunkSize / 2), 0);
    }
    private ChunkDataModel GetChunkData(int PosX, int PosY) {
        /*var chunkPath = "/" + chunkDirectory + "/" + "chunk_" + PosX + "_" + PosY;
        var chunkData = FileManager.GetFile<ChunkDataModel>(chunkPath);
        if (chunkData == null) {
            var chunkDataModel = TileMapService.CreateChunkDataModel(PosX, PosY, tilesWorldMap, wallTilesMap, chunkSize);
            // FileManager.Save(chunkDataModel, chunkPath);
            return chunkDataModel;
        } 
        return chunkData;*/
        /*if(cacheChunkData[PosX, PosY] == null) {
            var chunkData = TileMapService.CreateChunkDataModel(PosX, PosY, tilesWorldMap, wallTilesMap, chunkSize);
            return chunkData;
        }
        return cacheChunkData[PosX, PosY];*/
        return TileMapService.CreateChunkDataModel(PosX, PosY, tilesWorldMap, wallTilesMap, tilesShadowMap, chunkSize);
    }
    private void ManageChunkFromPool(int chunkPosX, int chunkPosY) {
        if (unUsedChunk.Count == 0) {
            Debug.Log("ATTENTION pool vide !!!!!!!!!!!"); // voir à créer d'autres objets au cas ou ?!?
        }
        usedChunk.Add(unUsedChunk[0]);
        unUsedChunk.RemoveAt(0);
        Chunk ck = usedChunk[usedChunk.Count - 1];
        GameObject chunkGo = ck.gameObject;
        ck.indexX = chunkPosX;
        ck.indexY = chunkPosY;
        int worldPosX = chunkPosX * chunkSize;
        int worldPosY = chunkPosY * chunkSize;
        ck.indexXWorldPos = worldPosX;
        ck.indexYWorldPos = worldPosY;
        chunkGo.transform.position = new Vector3(worldPosX, worldPosY, 0);
        // ck.player = player;
        ck.tilesMap = tilesMapChunks[chunkPosX, chunkPosY]; // ToDo régler le pb de out of range !!!!!!!!! => voir si pas out of bound
        var chunkData = GetChunkData(chunkPosX, chunkPosY);
        ck.tileMapTileMapScript.Init(worldPosX, worldPosY, tilesWorldMap, chunkData.tilemapData, boundX, boundY);
        ck.wallTileMapScript.Init(worldPosX, worldPosY, wallTilesMap, chunkData.wallmapData, boundX, boundY);
        ck.shadowTileMapScript.Init(worldPosX, worldPosY, tilesShadowMap, chunkData.shadowmapData, boundX, boundY);

        chunkGo.SetActive(true);
    }
    private void PlayerIsTooFar(Chunk ck) {
        var i = 0;
        int findIndex = -1;
        usedChunk.ForEach(chunk => {
            if (chunk.indexX == ck.indexX && chunk.indexY == ck.indexY) {
                findIndex = i;
                return;
            }
            i++;
        });
        if (findIndex != -1) {
            ck.gameObject.SetActive(false);
            unUsedChunk.Add(usedChunk[findIndex]);
            usedChunk.RemoveAt(findIndex);
        }
    }
    private bool ChunkAlreadyCreate(int x, int y) {
        return usedChunk.Find(chunk => chunk.indexX == x && chunk.indexY == y);
    }

    private IEnumerator StartPoolChunk(int x, int y) {
        ManageChunkFromPool(x, y);
        yield return new WaitForSeconds(waitingTimeAfterCreateChunk);
    }

    private IEnumerator StartPool(int chunkIndexX, int chunkIndexY) {
        // top
        if (!ChunkAlreadyCreate(chunkIndexX, chunkIndexY + 1)) {
            ManageChunkFromPool(chunkIndexX, chunkIndexY + 1);
            yield return new WaitForSeconds(waitingTimeAfterCreateChunk);
        }
        // top right
        if (!ChunkAlreadyCreate(chunkIndexX + 1, chunkIndexY + 1)) {
            ManageChunkFromPool(chunkIndexX + 1, chunkIndexY + 1);
            yield return new WaitForSeconds(waitingTimeAfterCreateChunk);
        }
        // top left
        if (!ChunkAlreadyCreate(chunkIndexX - 1, chunkIndexY + 1)) {
            ManageChunkFromPool(chunkIndexX - 1, chunkIndexY + 1);
            yield return new WaitForSeconds(waitingTimeAfterCreateChunk);
        }
        // left
        if (!ChunkAlreadyCreate(chunkIndexX - 1, chunkIndexY)) {
            ManageChunkFromPool(chunkIndexX - 1, chunkIndexY);
            yield return new WaitForSeconds(waitingTimeAfterCreateChunk);
        }
        // right
        if (!ChunkAlreadyCreate(chunkIndexX + 1, chunkIndexY)) {
            ManageChunkFromPool(chunkIndexX + 1, chunkIndexY);
            yield return new WaitForSeconds(waitingTimeAfterCreateChunk);
        }
        // bottom
        if (!ChunkAlreadyCreate(chunkIndexX, chunkIndexY - 1)) {
            ManageChunkFromPool(chunkIndexX, chunkIndexY - 1);
            yield return new WaitForSeconds(waitingTimeAfterCreateChunk);
        }
        // bottom left
        if (!ChunkAlreadyCreate(chunkIndexX - 1, chunkIndexY - 1)) {
            ManageChunkFromPool(chunkIndexX - 1, chunkIndexY - 1);
            yield return new WaitForSeconds(waitingTimeAfterCreateChunk);
        }
        // bottom right
        if (!ChunkAlreadyCreate(chunkIndexX + 1, chunkIndexY - 1)) {
            ManageChunkFromPool(chunkIndexX + 1, chunkIndexY - 1);
            yield return new WaitForSeconds(waitingTimeAfterCreateChunk);
        }
    }
}