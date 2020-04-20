using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkManager : MonoBehaviour {

    public int poolSize;
    private GameObject player;
    public static Dictionary<int, TileBase> tilebaseDictionary;
    private Transform worldMapTransform;
    private ChunkPool pool;
    private int boundX;
    private int boundY;
    private int chunkSize;
    private readonly int maxChunkGapWithPlayerX = 6;
    private readonly int maxChunkGapWithPlayerY = 4;
    private Vector2Int oldPos;
    private Vector2Int currentPos;

    public void FixedUpdate() {
        if (!WorldManager.instance.MapIsInit())
            return;
        currentPos = new Vector2Int((int)player.transform.position.x / chunkSize, (int)player.transform.position.y / chunkSize);
        if (oldPos.x != currentPos.x || oldPos.y != currentPos.y) {
            pool.DeactivateTooFarChunks(currentPos, new Vector2(maxChunkGapWithPlayerX, maxChunkGapWithPlayerY));
            if (currentPos.x > oldPos.x) {
                StartPool(currentPos, Direction.RIGHT);
            } else if (currentPos.x < oldPos.x) {
                StartPool(currentPos, Direction.LEFT);
            }
            if (currentPos.y > oldPos.y) {
                StartPool(currentPos, Direction.TOP);
            } else if (currentPos.y < oldPos.y) {
                StartPool(currentPos, Direction.BOTTOM);
            }
            oldPos.x = currentPos.x;
            oldPos.y = currentPos.y;
        }
    }

    public Chunk GetChunk(int posX, int posY) {
        return pool.GetChunk(new Vector2Int(posX, posY));
    }

    public void Init(Dictionary<int, TileBase> _tilebaseDictionary) {
        chunkSize = WorldManager.instance.GetChunkSize();
        boundX = WorldManager.instance.worldMapTile.GetUpperBound(0);
        boundY = WorldManager.instance.worldMapTile.GetUpperBound(1);
        player = GameManager.instance.GetPlayer();
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
        oldPos = new Vector2Int(spanwPlayer.x, spanwPlayer.y);
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

    private void StartPool(Vector2Int chunkIndex, Direction direction) {
        List<Vector2Int> chunksPos;
        switch (direction) {
            case Direction.TOP:
                chunksPos = new List<Vector2Int> {
                new Vector2Int(chunkIndex.x, chunkIndex.y + 1),
                new Vector2Int(chunkIndex.x + 1, chunkIndex.y + 1),
                new Vector2Int(chunkIndex.x - 1, chunkIndex.y + 1),
                new Vector2Int(chunkIndex.x + 2, chunkIndex.y + 1),
                new Vector2Int(chunkIndex.x - 2, chunkIndex.y + 1),
                new Vector2Int(chunkIndex.x, chunkIndex.y + 2),
                new Vector2Int(chunkIndex.x + 1, chunkIndex.y + 2),
                new Vector2Int(chunkIndex.x - 1, chunkIndex.y + 2),
                new Vector2Int(chunkIndex.x + 2, chunkIndex.y + 2),
                new Vector2Int(chunkIndex.x - 2, chunkIndex.y + 2)
            };
                CheckIfChunkLoaded(chunksPos);
                break;
            case Direction.RIGHT:
                chunksPos = new List<Vector2Int> {
                    new Vector2Int(chunkIndex.x + 1, chunkIndex.y),
                    new Vector2Int(chunkIndex.x + 1, chunkIndex.y + 1),
                    new Vector2Int(chunkIndex.x + 1, chunkIndex.y - 1),
                    new Vector2Int(chunkIndex.x + 1, chunkIndex.y + 2),
                    new Vector2Int(chunkIndex.x + 1, chunkIndex.y - 2),
                    new Vector2Int(chunkIndex.x + 2, chunkIndex.y),
                    new Vector2Int(chunkIndex.x + 2, chunkIndex.y + 1),
                    new Vector2Int(chunkIndex.x + 2, chunkIndex.y - 1),
                    new Vector2Int(chunkIndex.x + 2, chunkIndex.y + 2),
                    new Vector2Int(chunkIndex.x + 2, chunkIndex.y - 2)
                };
                CheckIfChunkLoaded(chunksPos);
                break;

            case Direction.BOTTOM:
                chunksPos = new List<Vector2Int> {
                new Vector2Int(chunkIndex.x, chunkIndex.y - 1),
                new Vector2Int(chunkIndex.x - 1, chunkIndex.y - 1),
                new Vector2Int(chunkIndex.x + 1, chunkIndex.y - 1),
                new Vector2Int(chunkIndex.x + 2, chunkIndex.y - 1),
                new Vector2Int(chunkIndex.x - 2, chunkIndex.y - 1),
                new Vector2Int(chunkIndex.x, chunkIndex.y - 2),
                new Vector2Int(chunkIndex.x - 1, chunkIndex.y - 2),
                new Vector2Int(chunkIndex.x + 1, chunkIndex.y - 2),
                new Vector2Int(chunkIndex.x + 2, chunkIndex.y - 2),
                new Vector2Int(chunkIndex.x - 2, chunkIndex.y - 2)
            };
                CheckIfChunkLoaded(chunksPos);
                break;

            case Direction.LEFT:
                chunksPos = new List<Vector2Int> {
                new Vector2Int(chunkIndex.x - 1, chunkIndex.y),
                new Vector2Int(chunkIndex.x - 1, chunkIndex.y - 1),
                new Vector2Int(chunkIndex.x - 1, chunkIndex.y + 1),
                new Vector2Int(chunkIndex.x - 1, chunkIndex.y - 2),
                new Vector2Int(chunkIndex.x - 1, chunkIndex.y + 2),
                new Vector2Int(chunkIndex.x - 2, chunkIndex.y),
                new Vector2Int(chunkIndex.x - 2, chunkIndex.y - 1),
                new Vector2Int(chunkIndex.x - 2, chunkIndex.y + 1),
                new Vector2Int(chunkIndex.x - 2, chunkIndex.y - 2),
                new Vector2Int(chunkIndex.x - 2, chunkIndex.y + 2)
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