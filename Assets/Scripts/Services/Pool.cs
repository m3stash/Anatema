using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T>: MonoBehaviour {

    public static ManageItems Instance;
    private static Dictionary<int, Item_cfg> itmCfgDict;
    private protected T[] pool;
    private protected T CreateObject(T obj) {
        return default (T);
    }

    private protected void DestroyOject<T>(T obj) {
        // 
    }

    private protected GameObject CreateItem(int id) {
        /*Instance.ManageDictionary(id);
        // GameObject itemGo = Instantiate(item, new Vector3(x + 0.5f, y + 0.5f, 0), item.transform.rotation);
        var item = (GameObject)Resources.Load("Prefabs/items/item_" + id);
        GameObject itemGo = Instantiate(item, new Vector3(x, y, 0), item.transform.rotation);
        itemGo.GetComponent<Item>().config = itmCfgDict[id];
        return itemGo;*/
        return null;
    }
}
