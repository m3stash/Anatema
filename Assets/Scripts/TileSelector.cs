using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelector : MonoBehaviour {

    private Ray ray;
    private Camera cam;
    private float timeClick;
    private bool onClick = false;
    private GameObject tile_dig_1, tile_dig_2, tile_dig_3, selector;
    private Vector2 currentTile;
    private GameObject player;
    private WorldManager worldManager;
    private InventoryService inventoryService;
    private SpriteRenderer spriteRender;
    private int[,] wallTilesMap;
    private int[,] tilesWorldMap;
    private GameObject[,] tilesObjetMap;

    public void Init(GameObject _player, WorldManager _worldManager, int[,] _wallTilesMap, int[,] _tilesWorldMap, GameObject[,] _tilesObjetMap) {
        worldManager = _worldManager;
        inventoryService = GameObject.FindGameObjectWithTag("InventoryContainer").GetComponent<InventoryService>();
        player = _player;
        cam = player.GetComponentInChildren<Camera>();
        tile_dig_1 = gameObject.transform.GetChild(0).gameObject;
        tile_dig_2 = gameObject.transform.GetChild(1).gameObject;
        tile_dig_3 = gameObject.transform.GetChild(2).gameObject;
        selector = gameObject.transform.GetChild(3).gameObject;
        spriteRender = selector.GetComponent<SpriteRenderer>();
        wallTilesMap = _wallTilesMap;
        tilesWorldMap = _tilesWorldMap;
        tilesObjetMap = _tilesObjetMap;
        StartCoroutine("InitSelector");
    }

    private void DisableCursor() {
        timeClick = 0;
        tile_dig_1.SetActive(false);
        tile_dig_2.SetActive(false);
        tile_dig_3.SetActive(false);
    }

    IEnumerator InitSelector() {
        while (true) {
            if (Input.GetMouseButtonUp(0)) {
                DisableCursor();
                onClick = false;
            }
            if (Input.GetMouseButtonDown(0)) {
                onClick = true;
            }
            if (inventoryService.seletedItem) {
                // toDo refaire tout le system et récupérer les différents tiles, object, wall afin de comparer les valeurs et ne plus utiliser le hit!
                ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero);
                var tsX = (int)hit.point.x + 0.5f;
                var tsY = (int)hit.point.y + 0.5f;
                var posX = (int)ray.origin.x;
                var posY = (int)ray.origin.y;
                // ToDo passer par un ENUM
                switch (inventoryService.seletedItem.type) {
                    case "Block":
                        ActiveSelector((int)ray.origin.x + 0.5f, (int)ray.origin.y + 0.5f);
                        if (tilesWorldMap[posX, posY] == 0) {
                            spriteRender.color = Color.white;
                            if (onClick)
                                AddBlock(posX, posY, inventoryService.seletedItem.config.id);
                        } else {
                            spriteRender.color = Color.red;
                        }
                        break;
                    case "Crafting":
                        ActiveSelector(tsX, tsY);
                        break;
                    case "Tool":
                        if (tilesWorldMap[posX, posY] > 0) {
                            ActiveSelector(tsX, tsY);
                            spriteRender.color = Color.white;
                            if (onClick) {
                                if(tilesObjetMap[posX, posY] != null) {
                                    DeleteItem(posX, posY);
                                } else {
                                    DeleteTile(hit);
                                }
                                // tester si tile ou torche !
                            }
                        } else {
                            DisableCursor();
                            selector.SetActive(false);
                        }
                        break;
                    case "Furniture":
                        if (tilesWorldMap[posX, posY] > 0) {
                            ActiveSelector(tsX, tsY);
                            spriteRender.color = Color.white;
                            if (onClick) {
                                Debug.Log("posX"+ posX);
                                Debug.Log("posY" + posY);
                                AddConsummable(inventoryService.seletedItem, posX, posY);
                            }
                        } else {
                            DisableCursor();
                            selector.SetActive(false);
                        }
                        break;
                    default:
                        DisableCursor();
                        selector.SetActive(false);
                        break;
                }
            } else {
                DisableCursor();
                selector.SetActive(false);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void ActiveSelectorSize() {
        selector.transform.localScale = new Vector3(inventoryService.seletedItem.config.width, inventoryService.seletedItem.config.height, 0);
    }

    private void ActiveSelector(float tsX, float tsY) {
        if ((int)player.transform.position.x - tsX < 2 && (int)player.transform.position.x - tsX > -3 && (int)player.transform.position.y - tsY < 2 && (int)player.transform.position.y - tsY > -3) {
            gameObject.transform.position = new Vector3(tsX, tsY, gameObject.transform.position.z);
            selector.SetActive(true);
        } else {
            DisableCursor();
            selector.SetActive(false);
        }
    }

    private void DeleteItem(int posX, int posY) {
        worldManager.DeleteItem(posX, posY);
    }

    private void DeleteTile(RaycastHit2D hit) {
        if (timeClick > 0.1f && onClick) {
            tile_dig_1.SetActive(true);
        }
        if (timeClick > 0.2f && onClick) {
            tile_dig_1.SetActive(false);
            tile_dig_2.SetActive(true);
        }
        if (timeClick > 0.3f && onClick) {
            tile_dig_2.SetActive(false);
            tile_dig_3.SetActive(true);
            worldManager.DeleteTile((int)hit.point.x, (int)hit.point.y);
            // tile_dig_3.SetActive(false);
            DisableCursor();
            // timeClick = 0;
        }
        if (onClick) {
            timeClick += Time.deltaTime;
        }
    }

    private void AddBlock(int x, int y, int id) {
        if (timeClick > 0.4f && onClick) {
            worldManager.AddTile(x, y, id);
            inventoryService.RemoveItem();
        }
        if (onClick) {
            timeClick += Time.deltaTime;
        }
    }

    private void AddConsummable(InventoryItem item, int posX, int posY) {
        worldManager.AddItem(posX, posY, item);
        onClick = false;
    }
}