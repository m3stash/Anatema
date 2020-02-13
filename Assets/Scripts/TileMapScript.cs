using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapScript : MonoBehaviour {

    private System.Random rand = new System.Random();

    public int[][] map;
    public int PosX;
    public int PosY;
    private int boundX;
    private int boundY;
    public void Init(int PosX, int PosY, int[][] map, int boundX, int boundY) {
        this.PosX = PosX;
        this.PosY = PosY;
        this.map = map;
        this.boundX = boundX;
        this.boundY = boundY;
    }

    public void SetTileData(int x, int y, ref TileData tileData, Sprite[] m_Sprites) {
        var currentTilePosX = PosX + x;
        var currentTilePosY = PosY + y;
        int xLess = currentTilePosX - 1;
        int xMore = currentTilePosX + 1;
        int yMore = currentTilePosY + 1;
        int yLess = currentTilePosY - 1;
        var currentId = map[currentTilePosX][currentTilePosY];
        int maskTilemap = 0;
        if (yMore <= boundY) {
            maskTilemap += CheckId(currentId, map[currentTilePosX][yMore], 1);
        }
        if (xMore <= boundX) {
            maskTilemap += CheckId(currentId, map[xMore][currentTilePosY], 4);
            if (yMore <= boundY) {
                maskTilemap += CheckId(currentId, map[xMore][yMore], 2);
            }
            if (yLess > -1) {
                maskTilemap += CheckId(currentId, map[xMore][yLess], 8);
            }
        }
        if (yLess > -1) {
            maskTilemap += CheckId(currentId, map[currentTilePosX][yLess], 16);
        }
        if (xLess > -1) {
            maskTilemap += CheckId(currentId, map[xLess][currentTilePosY], 64);
            if (yLess > -1) {
                maskTilemap += CheckId(currentId, map[xLess][yLess], 32);
            }
            if (yMore <= boundY) {
                maskTilemap += CheckId(currentId, map[xLess][yMore], 128);
            }
        }
        maskTilemap = GetMaskByOriginal(maskTilemap);
        int index = GetIndex((byte)maskTilemap);
        if (index > 0) {
            tileData.sprite = m_Sprites[index];
            tileData.transform = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, GetTransform((byte)maskTilemap)), Vector3.one);
            tileData.flags = TileFlags.LockTransform;
            tileData.colliderType = (Tile.ColliderType)1;
        }
    }

    private int GetMaskByOriginal(int mask) {
        byte original = (byte)mask;
        if ((original | 254) < 255) { mask = mask & 125; }
        if ((original | 251) < 255) { mask = mask & 245; }
        if ((original | 239) < 255) { mask = mask & 215; }
        if ((original | 191) < 255) { mask = mask & 95; }
        return mask;
    }

    public int GetIndex(byte mask) {
        switch (mask) {
            case 0:
                return GetRand(new int[] { 0, 12, 24 });
            case 1:
            case 4:
            case 16:
            case 64:
                return GetRand(new int[] { 1, 13, 25 });
            case 7:
            case 28:
            case 112:
            case 193:
                return GetRand(new int[] { 2, 14, 26 });
            case 17:
            case 68:
                return GetRand(new int[] { 3, 15, 27 });
            case 31:
            case 124:
            case 241:
            case 199:
                return GetRand(new int[] { 4, 16, 28 });
            case 255:
                return GetRand(new int[] { 5, 17, 29 });
            case 5:
            case 20:
            case 80:
            case 65:
                return GetRand(new int[] { 6, 18, 30 });
            case 21:
            case 84:
            case 81:
            case 69:
                return GetRand(new int[] { 7, 19, 31 });
            case 23:
            case 92:
            case 113:
            case 197:
                return GetRand(new int[] { 8, 20, 32 });
            case 29:
            case 116:
            case 209:
            case 71:
                return GetRand(new int[] { 9, 21, 33 });
            case 85:
                return 10;
            case 87:
            case 93:
            case 117:
            case 213:
                return 11;
            case 95:
            case 125:
            case 245:
            case 215:
                return 22;
            case 119:
            case 221:
                return 23;
            case 127:
            case 253:
            case 247:
            case 223:
                return 34;
        }
        return -1;
    }
    private int CheckId(int currentId, int neightboorId, int mask) {
        if (neightboorId == 0)
            return 0;
        int maskTilemap = 0;
        switch (currentId) {
            case 1:
                if (neightboorId != 2) {
                    maskTilemap = currentId == neightboorId ? mask : 0;
                } else {
                    maskTilemap = mask;
                }
                break;
            case 2:
                if (neightboorId != 1) {
                    maskTilemap = currentId == neightboorId ? mask : 0;
                } else {
                    maskTilemap = mask;
                }
                break;
            default:
                maskTilemap = currentId == neightboorId ? mask : 0;
                break;
        }
        return maskTilemap;
    }
    public int GetRand(int[] array) {
        return array[rand.Next(0, 3)];
    }
    public float GetTransform(byte mask) {
        switch (mask) {
            case 4:
            case 20:
            case 28:
            case 68:
            case 84:
            case 92:
            case 116:
            case 124:
            case 93:
            case 125:
            case 221:
            case 253:
                return -90f;
            case 16:
            case 80:
            case 112:
            case 81:
            case 113:
            case 209:
            case 241:
            case 117:
            case 245:
            case 247:
                return -180f;
            case 64:
            case 65:
            case 193:
            case 69:
            case 197:
            case 71:
            case 199:
            case 213:
            case 215:
            case 223:
                return -270f;
        }
        return 0;
    }
}
