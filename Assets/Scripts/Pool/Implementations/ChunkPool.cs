using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkPool : Pool<Chunk> {

    public void Setup(Transform parent, int poolSize) {
        GameObject obj = Instantiate((GameObject)Resources.Load("Prefabs/Chunk"), new Vector3(0, 0, 0), transform.rotation);
        obj.transform.parent = parent;

        Chunk chunk = obj.GetComponent<Chunk>();
        obj.SetActive(false);

        base.Setup(chunk, poolSize);
    }

    public bool IsChunkExists(Vector2Int pos) {
        for(int i = 0; i < this.usedObjects.Count; i++) {
            if(this.usedObjects[i].chunkPosition.x == pos.x && this.usedObjects[i].chunkPosition.y == pos.y) {
                return true;
            }
        }

        return false;
    }

    public Chunk GetChunk(Vector2 pos) {
        return this.usedObjects.Find(chunk => chunk.chunkPosition.x == pos.x && chunk.chunkPosition.y == pos.y);
    }

    public void DeactivateTooFarChunks(Vector2 origin, Vector2 gap) {
        List<Chunk> chunksToDeactivate = this.usedObjects.FindAll((Chunk chunk) => Mathf.Abs(chunk.chunkPosition.x - origin.x) >= gap.x || Mathf.Abs(chunk.chunkPosition.y - origin.y) >= gap.y);
        this.ReturnObjects(chunksToDeactivate.ToArray());
    }
}
