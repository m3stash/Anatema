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
        // Initialize item database with all item configs
        this.itemDatabase = new List<ItemConfig>(Resources.LoadAll<ItemConfig>("Scriptables/MyItems")).ToDictionary((ItemConfig item) => item.GetId(), item => item);

        // Initialize pools foreach item which are pooleable
        this.pools = this.itemDatabase
            .Where((KeyValuePair<int, ItemConfig> arg) => arg.Value.IsPooleable())
            .ToDictionary(pair => pair.Key, pair => this.CreatePool(pair.Value));

        Debug.Log(this.pools.Count);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)) {
            this.GetOne(1);
        }
    }

    /// <summary>
    /// Get a reference of specific item given by his id.
    /// Get item from pool if it is pooleable else it's instantiated
    /// </summary>
    /// <param name="itemIdx">Item index of scriptable item config (uniq)</param>
    public Item GetOne(int itemIdx) {
        ItemPool pool = this.pools[itemIdx];
        ItemConfig itemConfig = this.itemDatabase[itemIdx];
        Item item = null;

        if (pool) {
            Debug.Log("Item get from a pool");
            item = pool.GetOne();
        } else if(itemConfig){
            Debug.Log("Item instantiated in runtime");
            GameObject obj = Instantiate(itemConfig.GetPrefab());
            item = obj.GetComponent<Item>();
            item.Setup(itemConfig, null);
            return item;
        } else {
            Debug.LogErrorFormat("Item with id {0} not found in database", itemIdx);
        }

        return item;
    }

    private ItemPool CreatePool(ItemConfig config)
    {
        GameObject poolContainer = new GameObject("Pool of : " + config.GetDisplayName());
        ItemPool pool = poolContainer.AddComponent<ItemPool>();
        pool.Setup(config);
        return pool;
    }
}
