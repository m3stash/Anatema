using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public static class TileMapService {

    private static System.Random rand = new System.Random();
    public static ChunkDataModel CreateChunkDataModel(int PosX, int PosY, int chunkSize) {
        var boundX = WorldManager.tilesWorldMap.GetUpperBound(0);
        var boundY = WorldManager.tilesWorldMap.GetUpperBound(1);
        var tilemapData = new TileDataModel[chunkSize, chunkSize];
        var wallmapData = new TileDataModel[chunkSize, chunkSize];
        var shadowmapData = new TileDataModel[chunkSize, chunkSize];
        var tilePosX = PosX * chunkSize;
        var tilePosY = PosY * chunkSize;
        for (var x = 0; x < chunkSize; x++) {
            for (var y = 0; y < chunkSize; y++) {
                var currentTilePosX = tilePosX + x;
                var currentTilePosY = tilePosY + y;
                int l = currentTilePosX - 1;
                int r = currentTilePosX + 1;
                int t = currentTilePosY + 1;
                int b = currentTilePosY - 1;
                int maskTilemap = 0;
                int maskWallmap = 0;
                int maskShadowmap = 0;

                if (t <= boundY) {
                    maskTilemap += WorldManager.tilesWorldMap[currentTilePosX, t] > 0 ? 1 : 0;
                    maskWallmap += WorldManager.wallTilesMap[currentTilePosX, t] > 0 ? 1 : 0;
                    maskShadowmap += WorldManager.tilesShadowMap[currentTilePosX, t] > 0 ? 1 : 0;
                }

                if (r <= boundX) {
                    maskTilemap += WorldManager.tilesWorldMap[r, currentTilePosY] > 0 ? 4 : 0;
                    maskWallmap += WorldManager.wallTilesMap[r, currentTilePosY] > 0 ? 4 : 0;
                    maskShadowmap += WorldManager.tilesShadowMap[r, currentTilePosY] > 0 ? 4 : 0;

                    if (t <= boundY) {
                        maskTilemap += WorldManager.tilesWorldMap[r, t] > 0 ? 2 : 0;
                        maskWallmap += WorldManager.wallTilesMap[r, t] > 0 ? 2 : 0;
                        maskShadowmap += WorldManager.tilesShadowMap[r, currentTilePosY] > 0 ? 2 : 0;
                    }

                    if (b > -1) {
                        maskTilemap += WorldManager.tilesWorldMap[r, b] > 0 ? 8 : 0;
                        maskWallmap += WorldManager.wallTilesMap[r, b] > 0 ? 8 : 0;
                        maskShadowmap += WorldManager.tilesShadowMap[r, currentTilePosY] > 0 ? 8 : 0;
                    }
                }

                if (b > -1) {
                    maskTilemap += WorldManager.tilesWorldMap[currentTilePosX, b] > 0 ? 16 : 0;
                    maskWallmap += WorldManager.wallTilesMap[currentTilePosX, b] > 0 ? 16 : 0;
                    maskShadowmap += WorldManager.tilesShadowMap[r, currentTilePosY] > 0 ? 16 : 0;
                }

                if (l > -1) {
                    maskTilemap += WorldManager.tilesWorldMap[l, currentTilePosY] > 0 ? 64 : 0;
                    maskWallmap += WorldManager.wallTilesMap[l, currentTilePosY] > 0 ? 64 : 0;
                    maskShadowmap += WorldManager.tilesShadowMap[r, currentTilePosY] > 0 ? 64 : 0;

                    if (b > -1) {
                        maskTilemap += WorldManager.tilesWorldMap[l, b] > 0 ? 32 : 0;
                        maskWallmap += WorldManager.wallTilesMap[l, b] > 0 ? 32 : 0;
                        maskShadowmap += WorldManager.tilesShadowMap[r, currentTilePosY] > 0 ? 32 : 0;
                    }

                    if (t <= boundY) {
                        maskTilemap += WorldManager.tilesWorldMap[l, t] > 0 ? 128 : 0;
                        maskWallmap += WorldManager.wallTilesMap[l, b] > 0 ? 128 : 0;
                        maskShadowmap += WorldManager.tilesShadowMap[r, currentTilePosY] > 0 ? 128 : 0;
                    }
                }
                maskTilemap = GetMaskByOriginal(maskTilemap);
                tilemapData[x, y] = new TileDataModel {
                    index = GetIndex((byte)maskTilemap),
                    rotation = GetTransform((byte)maskTilemap),  // todo void a ne pas stocker de transform
                    colliderType = 2,
                };
                maskWallmap = GetMaskByOriginal(maskWallmap);
                wallmapData[x, y] = new TileDataModel {
                    index = GetIndex((byte)maskWallmap),
                    rotation = GetTransform((byte)maskWallmap),
                    colliderType = 0,
                };
                maskShadowmap = GetMaskByOriginal(maskShadowmap);
                shadowmapData[x, y] = new TileDataModel {
                    index = GetIndex((byte)maskShadowmap),
                    rotation = GetTransform((byte)maskShadowmap),
                    colliderType = 0,
                };
            }
        }
        return new ChunkDataModel {
            tilemapData = tilemapData,
            wallmapData = wallmapData,
            shadowmapData = shadowmapData,
        };
    }

    private static bool CheckId(int currentId, int neightboorId) {
        if (currentId == 1 && neightboorId == 2 || currentId == 2 && neightboorId == 1) {
            return true;
        }
        return (neightboorId > 0 && currentId == neightboorId) ? true : false;
    }

    public static void SetTileMapData(int PosX, int PosY, int x, int y, int[,] map, TileDataModel tileDataModel, int boundX, int boundY) {
        var currentTilePosX = PosX + x;
        var currentTilePosY = PosY + y;
        int xLess = currentTilePosX - 1;
        int xMore = currentTilePosX + 1;
        int yMore = currentTilePosY + 1;
        int yLess = currentTilePosY - 1;
        var currentId = map[currentTilePosX, currentTilePosY];
        int maskTilemap = 0;
        if (yMore <= boundY) {
            maskTilemap += CheckId(currentId, map[currentTilePosX, yMore]) ? 1 : 0;
        }
        if (xMore <= boundX) {
            maskTilemap += CheckId(currentId, map[xMore, currentTilePosY]) ? 4 : 0;
            if (yMore <= boundY) {
                maskTilemap += CheckId(currentId, map[xMore, yMore]) ? 2 : 0;
            }
            if (yLess > -1) {
                maskTilemap += CheckId(currentId, map[xMore, yLess]) ? 8 : 0;
            }
        }
        if (yLess > -1) {
            maskTilemap += CheckId(currentId, map[currentTilePosX, yLess]) ? 16 : 0;
        }
        if (xLess > -1) {
            maskTilemap += CheckId(currentId, map[xLess, currentTilePosY]) ? 64 : 0;
            if (yLess > -1) {
                maskTilemap += CheckId(currentId, map[xLess, yLess]) ? 32 : 0;
            }

            if (yMore <= boundY) {
                maskTilemap += CheckId(currentId, map[xLess, yMore]) ? 128 : 0;
            }
        }
        maskTilemap = GetMaskByOriginal(maskTilemap);
        tileDataModel.index = GetIndex((byte)maskTilemap);
        tileDataModel.rotation = GetTransform((byte)maskTilemap);
    }

    private static int GetMaskByOriginal(int mask) {
        byte original = (byte)mask;
        if ((original | 254) < 255) { mask = mask & 125; }
        if ((original | 251) < 255) { mask = mask & 245; }
        if ((original | 239) < 255) { mask = mask & 215; }
        if ((original | 191) < 255) { mask = mask & 95; }
        return mask;
    }

    public static int GetIndex(byte mask) {
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

    public static int GetRand(int[] array) {
        return array[rand.Next(0, 3)];
    }

    public static float GetTransform(byte mask) {
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
