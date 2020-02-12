using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
namespace UnityEngine.Tilemaps {
    [Serializable]
    [CreateAssetMenu(fileName = "New Terrain Tile", menuName = "Tiles/Terrain Tile")]

    public class TerrainTile : TileBase {

        [SerializeField]
        public Sprite[] m_Sprites;
        public Texture2D texture2d;

        /*public override void RefreshTile(Vector3Int location, ITilemap tileMap) {
            for (int yd = -1; yd <= 1; yd++) {
                for (int xd = -1; xd <= 1; xd++) {
                    Vector3Int position = new Vector3Int(location.x + xd, location.y + yd, location.z);
                    // utilité ???
                    //if (TileValue(tileMap, position)) {
                    //    tileMap.RefreshTile(position);
                    //}
                    tileMap.RefreshTile(position);
                }
            }
        }*/

        public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData) {
            tileMap.GetComponent<TileMapScript>().SetTileData(location.x, location.y, ref tileData, m_Sprites);
        }

        private bool TileValue(ITilemap tileMap, Vector3Int position) {
            TileBase tile = tileMap.GetTile(position);
            return (tile != null && tile == this);
        }

    }


#if UNITY_EDITOR
    [CustomEditor(typeof(TerrainTile))]
    public class TerrainTileEditor : Editor {
        private TerrainTile Tile { get { return (target as TerrainTile); } }
        private readonly String folderPath = "Sprites/Tiles/";
        private readonly int numberTiles = 35;

        public void OnEnable() {
            if (Tile.m_Sprites == null || Tile.m_Sprites.Length != numberTiles) {
                Tile.m_Sprites = new Sprite[numberTiles];
                EditorUtility.SetDirty(Tile);
            }
        }

        private void SetTiles(Texture2D texture2d) {
            if (Tile.texture2d) {
                var name = Tile.texture2d.name;
                var tiles = Resources.LoadAll(folderPath + name, typeof(Sprite));
                for (var i = 0; i < numberTiles; i++) {
                    var newName = name + "_" + i;
                    Tile.m_Sprites[i] = (Sprite)EditorGUILayout.ObjectField(newName, tiles[i], typeof(Sprite), false, null);
                }
                EditorUtility.SetDirty(Tile);
            }
        }

        public override void OnInspectorGUI() {
            EditorGUIUtility.labelWidth = 210;
            SetTiles(Tile.texture2d);
            EditorGUI.BeginChangeCheck();
            Tile.texture2d = (Texture2D)EditorGUILayout.ObjectField(Tile.texture2d, typeof(Texture2D), false);
            if (EditorGUI.EndChangeCheck()) {
                AssetDatabase.SaveAssets(); // toDo tester si ça marche !
                SetTiles(Tile.texture2d);
            }
        }

        /*public override void OnInspectorGUI() {
            EditorGUIUtility.labelWidth = 210;
            if (Tile.texture2d) {
                var name = Tile.texture2d.name;
                var folderName = folderPath + "/" + name + "/";
                for (var i = 0; i < numberTiles; i++) {
                    var newName = name + "_" + i;
                    var sprite = AssetDatabase.LoadAssetAtPath(folderName + newName + ".asset", typeof(Sprite)) as Sprite;
                    Tile.m_Sprites[i] = (Sprite)EditorGUILayout.ObjectField(newName, sprite, typeof(Sprite), false, null);
                }
                EditorUtility.SetDirty(Tile);
            }
            EditorGUI.BeginChangeCheck();
            Tile.texture2d = (Texture2D)EditorGUILayout.ObjectField(Tile.texture2d, typeof(Texture2D), false);

            if (EditorGUI.EndChangeCheck()) {
                if (Tile.texture2d) {
                    var numberOfTileX = Tile.texture2d.width / tileWidth;
                    var numberOfTileY = Tile.texture2d.height / tileWidth;
                    var name = Tile.texture2d.name;
                    var newFolderName = folderPath + "/" + name;
                    if (!IsFolderExist(newFolderName)) {
                        CreateFolder(name);
                    }
                    var count = 0;
                    for (var x = 0; x < numberOfTileX; x++) {
                        for (var y = 0; y < numberOfTileY; y++) {
                            var newName = name + "_" + count;
                            // texture2D > rect > pivot > pixel per unit > extrude edges > mesh type, border
                            var sprite = Sprite.Create(Tile.texture2d, new Rect(x * tileWidth, y * tileWidth, tileWidth, tileWidth), new Vector2(0, 0), pixelPerUnit, 0, SpriteMeshType.Tight, Vector4.zero);
                            AssetDatabase.CreateAsset(UnityEngine.Object.Instantiate(sprite), newFolderName + "/" + newName + ".asset");
                            Tile.m_Sprites[count] = (Sprite)EditorGUILayout.ObjectField(newName, sprite, typeof(Sprite), false, null);
                            count++;
                        }
                    }
                    EditorUtility.SetDirty(Tile);
                }
            }
        }*/
    }
#endif
}