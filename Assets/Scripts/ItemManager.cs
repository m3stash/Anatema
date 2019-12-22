using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemManager : MonoBehaviour
{
    private Dictionary<int, ItemConfig> itemDatabase; // Contains all items (Config)
    private Dictionary<int, ItemPool> pools;

    public static ItemManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
        }
    }

    private void Start() {
        this.itemDatabase = new List<ItemConfig>(Resources.LoadAll<ItemConfig>("Scriptables/MyItems")).ToDictionary((ItemConfig item) => item.GetId(), item => item);
        this.pools = this.itemDatabase.ToDictionary(pair => pair.Key, pair => this.CreatePool(pair.Value));

        Debug.Log("Item Database count : " + this.itemDatabase.Count);
        Debug.Log("Pools count : " + this.pools.Count);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)) {
            this.GetOne(1);
        }
    }

    public void GetOne(int itemIdx) {
        ItemPool pool = this.pools[itemIdx];

        if(pool) {
            Item item = pool.GetOne();
            item.gameObject.SetActive(true);
        } else {
            Debug.LogError("Pool not found for item idx : " + itemIdx);
        }
    }

    private ItemPool CreatePool(ItemConfig config)
    {
        GameObject poolContainer = new GameObject("Pool of : " + config.GetDisplayName());
        ItemPool pool = poolContainer.AddComponent<ItemPool>();
        pool.Setup(config);
        return pool;
    }
}
