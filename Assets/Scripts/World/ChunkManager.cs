using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkManager : MonoBehaviour {

    public int poolSize;
    private GameObject player;
    private Camera playerCam;
    public static Dictionary<int, TileBase> tilebaseDictionary;
    private Transform worldMapTransform;
    private ChunkPool pool;
    private int boundX;
    private int boundY;
    private int currentPlayerChunkX;
    private int currentPlayerChunkY;
    private int chunkSize;
    private readonly int maxChunkGapWithPlayerX = 6;
    private readonly int maxChunkGapWithPlayerY = 4;
    private int oldPosX = -1;
    private int oldPosY = -1;

    public void FixedUpdate() {
        if (!WorldManager.instance.MapIsInit())
            return;
        currentPlayerChunkX = (int)player.transform.position.x / chunkSize;
        currentPlayerChunkY = (int)player.transform.position.y / chunkSize;
        if(oldPosX == - 1 && oldPosY == -1) {
            oldPosX = currentPlayerChunkX;
            oldPosY = currentPlayerChunkY;
        }
        if (oldPosX != currentPlayerChunkX || oldPosY != currentPlayerChunkY) {
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
    }

    public Chunk GetChunk(int posX, int posY) {
        return pool.GetChunk(new Vector2(posX, posY));
    }

    public void Init(Dictionary<int, TileBase> _tilebaseDictionary, GameObject player) {
        chunkSize = WorldManager.instance.GetChunkSize();
        boundX = WorldManager.instance.worldMapTile.GetUpperBound(0);
        boundY = WorldManager.instance.worldMapTile.GetUpperBound(1);
        playerCam = player.GetComponentInChildren<Camera>();
        this.player = player;
        tilebaseDictionary = _tilebaseDictionary;
        Vector2Int spanwPlayer = CheckStartPlayerStartPosition(boundX / 2);
        Vector2Int chunkPosStart = new Vector2Int(spanwPlayer.x / chunkSize, spanwPlayer.y / chunkSize);
        CreatePoolChunk(chunkPosStart, spanwPlayer);
    }

    public Vector2Int CheckStartPlayerStartPosition(int posX) {
        for (var y = boundY; y >= 0; y--) {
            int currentTile = WorldManager.instance.worldMapTile[posX, y];
            if (currentTile == 2 || currentTile == 1) {
                if (PlayerHavePlaceToSpawn(posX, y + 2)) {
                    return new Vector2Int(posX, y + 1);
                }
                break;
            }
        }
        return CheckStartPlayerStartPosition(posX + 1);
    }

    private bool PlayerHavePlaceToSpawn(int x, int y) {
        bool allIsVoid = true;
        for (var newX = x - 1; newX <= x + 1; newX++) {
            for (var newY = y - 1; newY <= y + 1; newY++) {
                if (WorldManager.instance.worldMapTile[newX, newY] > 0) {
                    allIsVoid = false;
                }
            }
        }
        return allIsVoid;
    }

    public void CreatePoolChunk(Vector2Int chunkPosStart, Vector2Int spanwPlayer) {
        pool = gameObject.AddComponent<ChunkPool>();
        pool.Setup(WorldManager.instance.getMapGo().transform, poolSize);
        for (var x = chunkPosStart.x - 4; x < chunkPosStart.x + 5; x++) {
            for (var y = chunkPosStart.y - 3; y < chunkPosStart.y + 4; y++) {
                StartCoroutine(ManageChunkFromPool(new Vector2Int(x, y)));
            }
        }
        SetPlayerPosition(spanwPlayer);
    }

    private void SetPlayerPosition(Vector2Int spanwPlayer) {
        oldPosX = spanwPlayer.x;
        oldPosY = spanwPlayer.y;
        player.transform.position = new Vector3(spanwPlayer.x, spanwPlayer.y, player.transform.position.z);
    }

    private IEnumerator ManageChunkFromPool(Vector2Int chunkPos) {
        Chunk ck = pool.GetOne();
        GameObject chunkGo = ck.gameObject;
        ck.chunkPosition = chunkPos;
        ck.worldPosition = new Vector2Int(chunkPos.x * chunkSize, chunkPos.y * chunkSize);
        chunkGo.transform.position = new Vector3(ck.worldPosition.x, ck.worldPosition.y, 0);
        ck.tileMapTileMapScript.Init(ck.worldPosition.x, ck.worldPosition.y, WorldManager.instance.worldMapTile, boundX, boundY);
        ck.wallTileMapScript.Init(ck.worldPosition.x, ck.worldPosition.y, WorldManager.instance.worldMapWall, boundX, boundY);
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
            if (!pool.IsChunkExists(chunkToVerify)) {
                StartCoroutine(ManageChunkFromPool(chunkToVerify));
            }
        });
    }
}