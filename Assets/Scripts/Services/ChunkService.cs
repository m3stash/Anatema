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

public enum Direction {
    TOP,
    BOTTOM,
    LEFT,
    RIGHT
}

public class ChunkService : MonoBehaviour {

    public int numberOfPool;
    private GameObject player;
    private Camera playerCam;
    private int[,][,] tilesMapChunks;
    public static GameObject[,] tilesObjetMap; // toDO fixer ça pour de bon!
    public static Dictionary<int, TileBase> tilebaseDictionary;
    private Transform worldMapTransform;
    private float waitingTimeAfterCreateChunk = 0.1f;
    private ChunkPool pool;
    private int halfChunk;
    private int boundX;
    private int boundY;
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
    private int oldPlayerPosX;
    private int oldPlayerPosY;
    // event
    public delegate void LightEventHandler(int intensity);
    public static event LightEventHandler RefreshLight;

    public void FixedUpdate() {
        currentPlayerChunkX = (int)player.transform.position.x / WorldManager.chunkSize;
        currentPlayerChunkY = (int)player.transform.position.y / WorldManager.chunkSize;
        if ((oldPosX != currentPlayerChunkX || oldPosY != currentPlayerChunkY)) {
            this.pool.DeactivateTooFarChunks(new Vector2(currentPlayerChunkX, currentPlayerChunkY), new Vector2(maxChunkGapWithPlayerX, maxChunkGapWithPlayerY));
        }
        if (currentPlayerChunkX > oldPosX) { // right
            StartPool(currentPlayerChunkX, currentPlayerChunkY, Direction.RIGHT);
        } else if (currentPlayerChunkX < oldPosX) { // left
            StartPool(currentPlayerChunkX, currentPlayerChunkY, Direction.LEFT);
        }
        if (currentPlayerChunkY > oldPosY) { // top
            StartPool(currentPlayerChunkX, currentPlayerChunkY, Direction.TOP);
        } else if (currentPlayerChunkY < oldPosY) { // bottom
            StartPool(currentPlayerChunkX, currentPlayerChunkY, Direction.BOTTOM);
        }
        // dynamic light
        var playerX = (int)player.transform.position.x;
        var playerY = (int)player.transform.position.y;
        if (oldPlayerPosX != playerX || oldPlayerPosY != playerY) {
            LightService.RecursivDeleteLight(oldPlayerPosX, oldPlayerPosY, true);
            LightService.RecursivAddNewLight(playerX, playerY, 0);
        }
        oldPosX = currentPlayerChunkX;
        oldPosY = currentPlayerChunkY;
        oldPlayerPosX = (int)player.transform.position.x;
        oldPlayerPosY = (int)player.transform.position.y;
        RefreshLight(CycleDay.GetIntensity());
    }

    public int[,] GetTilesMapChunks(int x, int y) {
        return tilesMapChunks[x, y];
    }
    public Chunk GetChunk(int posX, int posY) {
        return this.pool.GetChunk(new Vector2(posX, posY));
    }
    public void Init(Dictionary<int, TileBase> _tilebaseDictionary, GameObject player) {
        boundX = WorldManager.tilesWorldMap.GetUpperBound(0);
        boundY = WorldManager.tilesWorldMap.GetUpperBound(1);
        playerCam = player.GetComponentInChildren<Camera>();
        halfChunk = WorldManager.chunkSize / 2;
        this.player = player;
        worldMapTransform = GameObject.FindGameObjectWithTag("WorldMap").gameObject.transform;
        tilebaseDictionary = _tilebaseDictionary;
        cacheChunkData = new ChunkDataModel[boundX, boundY];
        CreatePoolChunk(20, 52);
    }
    public void CreateChunksFromMaps(int[,] tilesMap) {
        chunkXLength = (tilesMap.GetUpperBound(0) + 1) / WorldManager.chunkSize;
        chunkYLength = (tilesMap.GetUpperBound(1) + 1) / WorldManager.chunkSize;
        int[,][,] tilesMapChunksArray = new int[chunkXLength, chunkYLength][,];
        for (var chkX = 0; chkX < chunkXLength; chkX++) {
            for (var chkY = 0; chkY < chunkYLength; chkY++) {
                int[,] tileMap = new int[WorldManager.chunkSize, WorldManager.chunkSize];
                for (var x = 0; x < WorldManager.chunkSize; x++) {
                    for (var y = 0; y < WorldManager.chunkSize; y++) {
                        tileMap[x, y] = tilesMap[(chkX * WorldManager.chunkSize) + x, (chkY * WorldManager.chunkSize) + y];
                    }
                }
                tilesMapChunksArray[chkX, chkY] = tileMap;
            }
        }
        tilesMapChunks = tilesMapChunksArray;
    }
    public void CreatePoolChunk(int xStart, int yStart) {
        this.pool = this.gameObject.AddComponent<ChunkPool>();
        this.pool.Setup(worldMapTransform, numberOfPool);

        // voir à améliorer ça pour faire de l'auto calc sur la range
        for (var x = xStart - 4; x < xStart + 5; x++) {
            for (var y = yStart - 3; y < yStart + 4; y++) {
                StartCoroutine(ManageChunkFromPool(new Vector2Int(x, y)));
            }
        }
        // spawn player on center start chunk
        oldPosX = xStart;
        oldPosY = yStart;
        oldPlayerPosX = xStart * WorldManager.chunkSize + (WorldManager.chunkSize / 2);
        oldPlayerPosY = yStart * WorldManager.chunkSize + (WorldManager.chunkSize / 2);
        player.transform.position = new Vector3(xStart * WorldManager.chunkSize + (WorldManager.chunkSize / 2), yStart * WorldManager.chunkSize + (WorldManager.chunkSize / 2), 0);
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
        return TileMapService.CreateChunkDataModel(PosX, PosY, WorldManager.chunkSize);
    }
    private IEnumerator ManageChunkFromPool(Vector2Int chunkPos) {
        Chunk ck = this.pool.GetOne();
        GameObject chunkGo = ck.gameObject;
        ck.tilesMap = null;
        ck.chunkPosition = chunkPos;
        ck.worldPosition = new Vector2Int(chunkPos.x * WorldManager.chunkSize, chunkPos.y * WorldManager.chunkSize);
        chunkGo.transform.position = new Vector3(ck.worldPosition.x, ck.worldPosition.y, 0);
        ck.tilesMap = tilesMapChunks[chunkPos.x, chunkPos.y]; // ToDo régler le pb de out of range !!!!!!!!! => voir si pas out of bound
        var chunkData = GetChunkData(chunkPos.x, chunkPos.y);
        ck.tileMapTileMapScript.Init(ck.worldPosition.x, ck.worldPosition.y, WorldManager.tilesWorldMap, chunkData.tilemapData, boundX, boundY);
        ck.wallTileMapScript.Init(ck.worldPosition.x, ck.worldPosition.y, WorldManager.wallTilesMap, chunkData.wallmapData, boundX, boundY);
        ck.shadowTileMapScript.Init(ck.worldPosition.x, ck.worldPosition.y, WorldManager.tilesShadowMap, chunkData.shadowmapData, boundX, boundY);
        IsVisible isVisibleScript = chunkGo.GetComponent<IsVisible>();
        isVisibleScript.cam = playerCam;
        isVisibleScript.chunk = ck;

        chunkGo.SetActive(true);
        yield return new WaitForSeconds(0.1f);
    }

    private void StartPool(int chunkIndexX, int chunkIndexY, Direction direction) {
        List<Vector2Int> chunksPos;
        switch (direction) {
            case Direction.TOP:
                chunksPos = new List<Vector2Int> {
                new Vector2Int(chunkIndexX, chunkIndexY + 1),
                new Vector2Int(chunkIndexX + 1, chunkIndexY + 1),
                new Vector2Int(chunkIndexX - 1, chunkIndexY + 1),
                new Vector2Int(chunkIndexX + 2, chunkIndexY + 1),
                new Vector2Int(chunkIndexX - 2, chunkIndexY + 1),
                new Vector2Int(chunkIndexX, chunkIndexY + 2),
                new Vector2Int(chunkIndexX + 1, chunkIndexY + 2),
                new Vector2Int(chunkIndexX - 1, chunkIndexY + 2),
                new Vector2Int(chunkIndexX + 2, chunkIndexY + 2),
                new Vector2Int(chunkIndexX - 2, chunkIndexY + 2)
            };
                CheckIfChunkLoaded(chunksPos);
                break;
            case Direction.RIGHT:
                chunksPos = new List<Vector2Int> {
                    new Vector2Int(chunkIndexX + 1, chunkIndexY),
                    new Vector2Int(chunkIndexX + 1, chunkIndexY + 1),
                    new Vector2Int(chunkIndexX + 1, chunkIndexY - 1),
                    new Vector2Int(chunkIndexX + 1, chunkIndexY + 2),
                    new Vector2Int(chunkIndexX + 1, chunkIndexY - 2),
                    new Vector2Int(chunkIndexX + 2, chunkIndexY),
                    new Vector2Int(chunkIndexX + 2, chunkIndexY + 1),
                    new Vector2Int(chunkIndexX + 2, chunkIndexY - 1),
                    new Vector2Int(chunkIndexX + 2, chunkIndexY + 2),
                    new Vector2Int(chunkIndexX + 2, chunkIndexY - 2)
                };
                CheckIfChunkLoaded(chunksPos);
                break;

            case Direction.BOTTOM:
                chunksPos = new List<Vector2Int> {
                new Vector2Int(chunkIndexX, chunkIndexY - 1),
                new Vector2Int(chunkIndexX - 1, chunkIndexY - 1),
                new Vector2Int(chunkIndexX + 1, chunkIndexY - 1),
                new Vector2Int(chunkIndexX + 2, chunkIndexY - 1),
                new Vector2Int(chunkIndexX - 2, chunkIndexY - 1),
                new Vector2Int(chunkIndexX, chunkIndexY - 2),
                new Vector2Int(chunkIndexX - 1, chunkIndexY - 2),
                new Vector2Int(chunkIndexX + 1, chunkIndexY - 2),
                new Vector2Int(chunkIndexX + 2, chunkIndexY - 2),
                new Vector2Int(chunkIndexX - 2, chunkIndexY - 2)
            };
                CheckIfChunkLoaded(chunksPos);
                break;

            case Direction.LEFT:
                chunksPos = new List<Vector2Int> {
                new Vector2Int(chunkIndexX - 1, chunkIndexY),
                new Vector2Int(chunkIndexX - 1, chunkIndexY - 1),
                new Vector2Int(chunkIndexX - 1, chunkIndexY + 1),
                new Vector2Int(chunkIndexX - 1, chunkIndexY - 2),
                new Vector2Int(chunkIndexX - 1, chunkIndexY + 2),
                new Vector2Int(chunkIndexX - 2, chunkIndexY),
                new Vector2Int(chunkIndexX - 2, chunkIndexY - 1),
                new Vector2Int(chunkIndexX - 2, chunkIndexY + 1),
                new Vector2Int(chunkIndexX - 2, chunkIndexY - 2),
                new Vector2Int(chunkIndexX - 2, chunkIndexY + 2)
            };
                CheckIfChunkLoaded(chunksPos);
                break;
        }
    }
    private void CheckIfChunkLoaded(List<Vector2Int> chunksToVerify) {
        chunksToVerify.ForEach(chunkToVerify => {
            if (!this.pool.IsChunkExists(chunkToVerify)) {
                StartCoroutine(ManageChunkFromPool(chunkToVerify));
            }
        });
    }
}