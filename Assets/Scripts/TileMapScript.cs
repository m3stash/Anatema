using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileMapScript : MonoBehaviour {
    public int[,] map;
    public int PosX;
    public int PosY;
    private TileDataModel[,] tileMapdata;
    public bool hasAlreadyInit;
    private int boundX;
    private int boundY;
    public void Init(int PosX, int PosY, int[,]map, TileDataModel[,] tileMapdata, int boundX, int boundY) {
        hasAlreadyInit = false;
        this.PosX = PosX;
        this.PosY = PosY;
        this.tileMapdata = tileMapdata;
        this.map = map;
        this.boundX = boundX;
        this.boundY = boundY;
    }

    public void SetTileData(int x, int y, ref TileData tileData, Sprite[] m_Sprites) {
        if (hasAlreadyInit) {
            TileMapService.SetTileMapData(PosX, PosY, x, y, map, tileMapdata[x, y], boundX, boundY);
        }
        var index = tileMapdata[x, y].index;
        if (index >= 0) {
            tileData.sprite = m_Sprites[index];
            tileData.transform = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, tileMapdata[x, y].rotation), Vector3.one);
            tileData.flags = TileFlags.LockTransform;
            tileData.colliderType = (Tile.ColliderType)tileMapdata[x, y].colliderType;
        }
    }
}
