using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public static class TileMapService {

    private static System.Random rand = new System.Random();
    public static ChunkDataModel CreateChunkDataModel(int PosX, int PosY, int[,] tilesWorldMap, int[,] wallTilesMap, int[,] tilesShadowMap, int chunkSize) {
        var boundX = tilesWorldMap.GetUpperBound(0);
        var boundY = tilesWorldMap.GetUpperBound(1);
        var tilemapData = new TileDataModel[chunkSize, chunkSize];
        var wallmapData = new TileDataModel[chunkSize, chunkSize];
        var shadowmapData = new TileDataModel[chunkSize, chunkSize];
        for (var x = 0; x < chunkSize; x++) {
            for (var y = 0; y < chunkSize; y++) {
                var currentTilePosX = PosX * chunkSize + x;
                var currentTilePosY = PosY * chunkSize + y;
                // int currentId = tilesWorldMap[currentTilePosX, currentTilePosY];
                int xLess = currentTilePosX - 1;
                int xMore = currentTilePosX + 1;
                int yMore = currentTilePosY + 1;
                int yLess = currentTilePosY - 1;
                int maskTilemap = 0;
                int maskWallmap = 0;
                int maskShadowmap = 0;

                if (yMore <= boundY) {
                    // top = CheckIfSameId(top, currentId) ? top : 0;
                    maskTilemap += tilesWorldMap[currentTilePosX, yMore] > 0 ? 1 : 0;
                    maskWallmap += wallTilesMap[currentTilePosX, yMore] > 0 ? 1 : 0;
                    maskShadowmap += tilesShadowMap[currentTilePosX, yMore] > 0 ? 1 : 0;
                }

                if (xMore <= boundX) {
                    // right = CheckIfSameId(right, currentId) ? right : 0;
                    maskTilemap += tilesWorldMap[xMore, currentTilePosY] > 0 ? 4 : 0;
                    maskWallmap += wallTilesMap[xMore, currentTilePosY] > 0 ? 4 : 0;
                    maskShadowmap += tilesShadowMap[xMore, currentTilePosY] > 0 ? 4 : 0;

                    if (yMore <= boundY) {
                        // diaRightTop = CheckIfSameId(diaRightTop, currentId) ? diaRightTop : 0;
                        maskTilemap += tilesWorldMap[xMore, yMore] > 0 ? 2 : 0;
                        maskWallmap += wallTilesMap[xMore, yMore] > 0 ? 2 : 0;
                        maskShadowmap += tilesShadowMap[xMore, currentTilePosY] > 0 ? 2 : 0;
                    }

                    if (yLess > -1) {
                        // diagBottomRight = CheckIfSameId(diagBottomRight, currentId) ? diagBottomRight : 0;
                        maskTilemap += tilesWorldMap[xMore, yLess] > 0 ? 8 : 0;
                        maskWallmap += wallTilesMap[xMore, yLess] > 0 ? 8 : 0;
                        maskShadowmap += tilesShadowMap[xMore, currentTilePosY] > 0 ? 8 : 0;
                    }
                }

                if (yLess > -1) {
                    // bottom = CheckIfSameId(bottom, currentId) ? bottom : 0;
                    maskTilemap += tilesWorldMap[currentTilePosX, yLess] > 0 ? 16 : 0;
                    maskWallmap += wallTilesMap[currentTilePosX, yLess] > 0 ? 16 : 0;
                    maskShadowmap += tilesShadowMap[xMore, currentTilePosY] > 0 ? 16 : 0;
                }

                if (xLess > -1) {
                    // left = CheckIfSameId(left, currentId) ? left : 0;
                    maskTilemap += tilesWorldMap[xLess, currentTilePosY] > 0 ? 64 : 0;
                    maskWallmap += wallTilesMap[xLess, currentTilePosY] > 0 ? 64 : 0;
                    maskShadowmap += tilesShadowMap[xMore, currentTilePosY] > 0 ? 64 : 0;

                    if (yLess > -1) {
                        // diagBottomLeft = CheckIfSameId(diagBottomLeft, currentId) ? diagBottomLeft : 0;
                        maskTilemap += tilesWorldMap[xLess, yLess] > 0 ? 32 : 0;
                        maskWallmap += wallTilesMap[xLess, yLess] > 0 ? 32 : 0;
                        maskShadowmap += tilesShadowMap[xMore, currentTilePosY] > 0 ? 32 : 0;
                    }

                    if (yMore <= boundY) {
                        // diagTopLeft = CheckIfSameId(diagTopLeft, currentId) ? diagTopLeft : 0;
                        maskTilemap += tilesWorldMap[xLess, yMore] > 0 ? 128 : 0;
                        maskWallmap += wallTilesMap[xLess, yLess] > 0 ? 128 : 0;
                        maskShadowmap += tilesShadowMap[xMore, currentTilePosY] > 0 ? 128 : 0;
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
                    rotation = GetTransform((byte)maskWallmap), // todo void a ne pas stocker de transform
                    colliderType = 0,
                };
                maskShadowmap = GetMaskByOriginal(maskShadowmap);
                shadowmapData[x, y] = new TileDataModel {
                    index = GetIndex((byte)maskShadowmap),
                    rotation = GetTransform((byte)maskShadowmap), // todo void a ne pas stocker de transform
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

    public static void SetTileMapData(int PosX, int PosY, int x, int y, int[,] tilesWorldMap, TileDataModel tileDataModel, int boundX, int boundY) {
        var currentTilePosX = PosX + x;
        var currentTilePosY = PosY + y;
        int xLess = currentTilePosX - 1;
        int xMore = currentTilePosX + 1;
        int yMore = currentTilePosY + 1;
        int yLess = currentTilePosY - 1;
        int maskTilemap = 0;
        if (yMore <= boundY) {
            maskTilemap += tilesWorldMap[currentTilePosX, yMore] > 0 ? 1 : 0;
        }
        if (xMore <= boundX) {
            maskTilemap += tilesWorldMap[xMore, currentTilePosY] > 0 ? 4 : 0;

            if (yMore <= boundY) {
                maskTilemap += tilesWorldMap[xMore, yMore] > 0 ? 2 : 0;
            }

            if (yLess > -1) {
                maskTilemap += tilesWorldMap[xMore, yLess] > 0 ? 8 : 0;
            }
        }

        if (yLess > -1) {
            maskTilemap += tilesWorldMap[currentTilePosX, yLess] > 0 ? 16 : 0;
        }

        if (xLess > -1) {
            maskTilemap += tilesWorldMap[xLess, currentTilePosY] > 0 ? 64 : 0;

            if (yLess > -1) {
                maskTilemap += tilesWorldMap[xLess, yLess] > 0 ? 32 : 0;
            }

            if (yMore <= boundY) {
                maskTilemap += tilesWorldMap[xLess, yMore] > 0 ? 128 : 0;
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
