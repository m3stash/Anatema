using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryToolbar : MonoBehaviour {

    private GameObject inventoryItemGo;
    private int currentPos;
    private Dictionary<int, GameObject> cells;
    private Sprite selectedSlot, slot;
    private InventoryService inventoryService;

    private void Awake() {
        inventoryItemGo = (GameObject)Resources.Load("Prefabs/Inventory_item");
    }

    private void Start() {
        slot = Resources.Load<Sprite>("Sprites/Cells/slot");
        selectedSlot = Resources.Load<Sprite>("Sprites/Cells/slot_selected");
        inventoryService = GetComponentInParent<InventoryService>();
        cells = new Dictionary<int, GameObject>();
        currentPos = 0;
        var i = 0;
        foreach (Transform child in transform) {
            cells[i] = child.gameObject;
            i++;
        }
        cells[0].GetComponent<Image>().sprite = selectedSlot;
        inventoryService.seletedItem = cells[0].transform.GetChild(0).gameObject.GetComponent<InventoryItem>();
    }

    private void SelectSlotImage(int currentPos) {
        for (var i = 0; i < cells.Count; i++) {
            cells[i].GetComponent<Image>().sprite = slot;
        }
        cells[currentPos].GetComponent<Image>().sprite = selectedSlot;
        if (cells[currentPos].transform.childCount > 0) {
            inventoryService.seletedItem = cells[currentPos].transform.GetChild(0).gameObject.GetComponent<InventoryItem>();
        } else {
            inventoryService.seletedItem = null;
        }
    }

    public void RefreshSelectedItem() {
        if(cells[currentPos].transform.childCount > 0) {
            inventoryService.seletedItem = cells[currentPos].transform.GetChild(0).gameObject.GetComponent<InventoryItem>();
        } else {
            inventoryService.seletedItem = null;
        }
    }

    private void Update() {
        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f) {
            if(currentPos < 7) {
                currentPos++;
            } else {
                currentPos = 0;
            }
            SelectSlotImage(currentPos);
        } else if (d < 0f) {
            if (currentPos > 0) {
                currentPos--;
            } else {
                currentPos = 7;
            }
            SelectSlotImage(currentPos);
        }
        
    }

    public void AddItem(GameObject item) {
        /*foreach (Transform child in transform) {
            var itmOnGround = item.GetComponent<Item>();
            var itemCfg = itmOnGround.GetConfig();
            // if already have item
            if (child.transform.childCount > 0) {
                var inventoryItem = child.transform.GetChild(0).gameObject.GetComponent<InventoryItem>();
                if (inventoryItem.config.name == itemCfg.name && inventoryItem.currentStack < inventoryItem.maxStacks) {
                    inventoryItem.currentStack = inventoryItem.currentStack + itmOnGround.GetStacks();
                    inventoryItem.text.text = inventoryItem.maxStacks > 1 ? (inventoryItem.currentStack).ToString() : "";
                    Destroy(item);
                    break;
                }
            } else {
                // if no item in toolbar then add new one
                GameObject itemGo = Instantiate(inventoryItemGo);
                var newInventoryItem = itemGo.GetComponent<InventoryItem>();
                var itemOnGround = item.GetComponent<Item>();
                newInventoryItem.config = itemOnGround.GetConfig();
                newInventoryItem.currentStack = itemOnGround.GetStacks();
                newInventoryItem.text.text = itemOnGround.GetConfig().GetStackLimit() > 1 ? itemOnGround.GetStacks().ToString() : "";
                itemGo.transform.parent = child.transform;
                Destroy(item);
                break;
            }
        }*/
    }

    public void RemoveItem() {
        /*var inventoryItem = cells[currentPos].transform.GetChild(0).gameObject.GetComponent<InventoryItem>();
        var currentStack = inventoryItem.currentStack;
        inventoryItem.currentStack--;
        if (currentStack > 1) {
            inventoryItem.text.text = (inventoryItem.currentStack).ToString();
        }
        else {
            Destroy(cells[currentPos].transform.GetChild(0).gameObject);
        }*/
    }
}
