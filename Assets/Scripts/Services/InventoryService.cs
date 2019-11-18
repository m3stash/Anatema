using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryService : MonoBehaviour {

    public InventoryToolbar inventoryToolbar;
    private Inventory inventory;
    public InventoryItem seletedItem;
    public static InventoryService Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start() {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        inventoryToolbar = GameObject.FindGameObjectWithTag("InventoryToolbar").GetComponent<InventoryToolbar>();
        var itemGo = ManageItems.CreateItem(4);
        inventoryToolbar.AddItem(itemGo);
        var itemGo2 = ManageItems.CreateItem(11); // TODO a enlever juste pour tester (torche)
        inventoryToolbar.AddItem(itemGo2);
    }

    public static void RefreshToolBar() {
        Instance.inventoryToolbar.RefreshSelectedItem();
    }

    public static bool CanStack(GameObject currentCell, GameObject movingCell) {
        var currentCellConf = currentCell.GetComponent<InventoryItem>();
        var movingCellConf = movingCell.GetComponent<InventoryItem>();
        if((int)(movingCellConf.config.stacks) == 1 || (int)(currentCellConf.config.stacks) == 1) {
            Instance.inventoryToolbar.RefreshSelectedItem();
            return false;
        }
        var newStack = currentCellConf.currentStack + movingCellConf.currentStack;
        if (currentCellConf.config.name == movingCellConf.config.name) {
            if (newStack <= currentCellConf.maxStacks) {
                movingCellConf.currentStack = newStack;
                movingCellConf.text.text = (newStack).ToString();
                Destroy(currentCell);
                Instance.inventoryToolbar.RefreshSelectedItem();
                return false;
            } else {
                if (currentCellConf.currentStack == currentCellConf.maxStacks || movingCellConf.currentStack == movingCellConf.maxStacks) {
                    Instance.inventoryToolbar.RefreshSelectedItem();
                    return false;
                }
                var diffStack = newStack - currentCellConf.maxStacks;
                currentCellConf.currentStack = currentCellConf.maxStacks;
                currentCellConf.text.text = currentCellConf.maxStacks.ToString();
                movingCellConf.currentStack = diffStack;
                movingCellConf.text.text = diffStack.ToString();
                Instance.inventoryToolbar.RefreshSelectedItem();
                return true;
            }
            
        }
        Instance.inventoryToolbar.RefreshSelectedItem();
        return true;
    }

    public void RemoveItem() {
        Instance.inventoryToolbar.RemoveItem();
    }

}
