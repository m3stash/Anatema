using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageItems : MonoBehaviour {

    public static ManageItems Instance;
    private static GameObject item;
    private static Dictionary<int, Item_cfg> itmCfgDict;

    private void Start() {

    }

    void Awake() {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            itmCfgDict = new Dictionary<int, Item_cfg>();
            item = (GameObject)Resources.Load("Prefabs/Item");
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public static void CreateItemToolbar(int x, int y, int id) {
        
    }

    public static void CreateItemOnMap(int x, int y, int id) {
        Instance.ManageDictionary(id);
        // GameObject itemGo = Instantiate(item, new Vector3(x + 0.5f, y + 0.5f, 0), item.transform.rotation);
        var item = (GameObject)Resources.Load("Prefabs/items/item_" + id);
        GameObject itemGo = Instantiate(item, new Vector3(x , y, 0), item.transform.rotation);
        itemGo.GetComponent<Item>().config = itmCfgDict[id];
    }

    public static GameObject CreateItem(int id) {
        Instance.ManageDictionary(id);
        GameObject itemGo = Instantiate(item, Vector3.zero, item.transform.rotation);
        itemGo.GetComponent<Item>().config = itmCfgDict[id];
        return itemGo;
    }

    public void ManageDictionary(int id) {
        if (!itmCfgDict.ContainsKey(id)) {
            // to do ajouter pool ici!
            itmCfgDict[id] = Instantiate(Resources.Load<Item_cfg>("Scriptables/Items/Item_" + id));
        }
    }
}
