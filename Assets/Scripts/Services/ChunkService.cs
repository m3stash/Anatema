using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkService : MonoBehaviour {

    public int poolSize;
    private GameObject player;
    private Camera playerCam;
    private int[,][,] tilesMapChunks;
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

    public void FixedUpdate() {
        if (!WorldManager.instance.MapIsInit())
            return;
        currentPlayerChunkX = (int)player.transform.position.x / WorldManager.instance.GetChunkSize();
        currentPlayerChunkY = (int)player.transform.position.y / WorldManager.instance.GetChunkSize();
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
        oldPosX = currentPlayerChunkX;
        oldPosY = currentPlayerChunkY;
    }

    public Chunk GetChunk(int posX, int posY) {
        return this.pool.GetChunk(new Vector2(posX, posY));
    }
    public void Init(Dictionary<int, TileBase> _tilebaseDictionary, GameObject player) {
        boundX = WorldManager.instance.tilesWorldMap.GetUpperBound(0);
        boundY = WorldManager.instance.tilesWorldMap.GetUpperBound(1);
        playerCam = player.GetComponentInChildren<Camera>();
        halfChunk = WorldManager.instance.GetChunkSize() / 2;
        this.player = player;
        worldMapTransform = GameObject.FindGameObjectWithTag("WorldMap").gameObject.transform;
        tilebaseDictionary = _tilebaseDictionary;
        cacheChunkData = new ChunkDataModel[boundX, boundY];
        CreatePoolChunk(20, 52);
    }
    public void CreateChunksFromMaps(int[,] tilesMap) {
        chunkXLength = (tilesMap.GetUpperBound(0) + 1) / WorldManager.instance.GetChunkSize();
        chunkYLength = (tilesMap.GetUpperBound(1) + 1) / WorldManager.instance.GetChunkSize();
        int[,][,] tilesMapChunksArray = new int[chunkXLength, chunkYLength][,];
        for (var chkX = 0; chkX < chunkXLength; chkX++) {
            for (var chkY = 0; chkY < chunkYLength; chkY++) {
                int[,] tileMap = new int[WorldManager.instance.GetChunkSize(), WorldManager.instance.GetChunkSize()];
                for (var x = 0; x < WorldManager.instance.GetChunkSize(); x++) {
                    for (var y = 0; y < WorldManager.instance.GetChunkSize(); y++) {
                        tileMap[x, y] = tilesMap[(chkX * WorldManager.instance.GetChunkSize()) + x, (chkY * WorldManager.instance.GetChunkSize()) + y];
                    }
                }
                tilesMapChunksArray[chkX, chkY] = tileMap;
            }
        }
        tilesMapChunks = tilesMapChunksArray;
    }
    public void CreatePoolChunk(int xStart, int yStart) {
        this.pool = this.gameObject.AddComponent<ChunkPool>();
        this.pool.Setup(worldMapTransform, poolSize);

        // voir à améliorer ça pour faire de l'auto calc sur la range
        for (var x = xStart - 4; x < xStart + 5; x++) {
            for (var y = yStart - 3; y < yStart + 4; y++) {
                StartCoroutine(ManageChunkFromPool(new Vector2Int(x, y)));
            }
        }
        // spawn player on center start chunk
        oldPosX = xStart;
        oldPosY = yStart;
        oldPlayerPosX = xStart * WorldManager.instance.GetChunkSize() + (WorldManager.instance.GetChunkSize() / 2);
        oldPlayerPosY = yStart * WorldManager.instance.GetChunkSize() + (WorldManager.instance.GetChunkSize() / 2);
        player.transform.position = new Vector3(xStart * WorldManager.instance.GetChunkSize() + (WorldManager.instance.GetChunkSize() / 2), yStart * WorldManager.instance.GetChunkSize() + (WorldManager.instance.GetChunkSize() / 2), 0);
    }

    private IEnumerator ManageChunkFromPool(Vector2Int chunkPos) {
        Chunk ck = this.pool.GetOne();
        GameObject chunkGo = ck.gameObject;
        ck.chunkPosition = chunkPos;
        ck.worldPosition = new Vector2Int(chunkPos.x * WorldManager.instance.GetChunkSize(), chunkPos.y * WorldManager.instance.GetChunkSize());
        chunkGo.transform.position = new Vector3(ck.worldPosition.x, ck.worldPosition.y, 0);
        ck.tileMapTileMapScript.Init(ck.worldPosition.x, ck.worldPosition.y, WorldManager.instance.tilesWorldMap, boundX, boundY);
        ck.wallTileMapScript.Init(ck.worldPosition.x, ck.worldPosition.y, WorldManager.instance.wallTilesMap, boundX, boundY);
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