using System.Collections;
using UnityEngine;

[System.Serializable]
public class ChunkDataModel {
    public TileDataModel[,] tilemapData;
    public TileDataModel[,] wallmapData;
    public TileDataModel[,] shadowmapData;
}
