using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemManager : MonoBehaviour {
    private Dictionary<int, ItemConfig> itemDatabase; // Contains all items (Config)
    private Dictionary<int, ItemPool> pools;

    public static ItemManager instance;

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    private void Start() {
        // Initialize item database with all item configs
        this.itemDatabase = new List<ItemConfig>(Resources.LoadAll<ItemConfig>("Scriptables/Items")).ToDictionary((ItemConfig item) => item.GetId(), item => item);

        // Check all items validity
        foreach(KeyValuePair<int, ItemConfig> arg in this.itemDatabase) {
            this.CheckItemValidity(arg.Value);
        }

        // Initialize pools foreach item which are pooleable
        this.pools = this.itemDatabase
            .Where((KeyValuePair<int, ItemConfig> arg) => arg.Value.IsPooleable())
            .ToDictionary(pair => pair.Key, pair => this.CreatePool(pair.Value));
    }


    /// <summary>
    /// Return item config for specific item id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns></returns>
    public ItemConfig GetItemWithId(int id) {
        if(this.itemDatabase.ContainsKey(id)) {
            return this.itemDatabase[id];
        }
        return null;
    }

    /// <summary>
    /// Create a reference of specific item given by his id.
    /// Create item from pool if it is pooleable else it's instantiated
    /// </summary>
    /// <param name="itemIdx">Item index of scriptable item config (uniq)</param>
    /// <param name="status">Status to init item (ACTIVE, PICKABLE, INACTIVE)</param>
    public Item CreateItem(int itemIdx, ItemStatus status) {
        ItemConfig itemConfig = this.itemDatabase[itemIdx];
        Item item = null;

        if(this.itemDatabase.ContainsKey(itemIdx)) {
            if(this.pools.ContainsKey(itemIdx)) {
                ItemPool pool = this.pools[itemIdx];
                item = pool.GetOne();
                item.Setup(itemConfig, status, 1, pool);
            } else {
                GameObject obj = Instantiate(itemConfig.GetPrefab());
                item = obj.GetComponent<Item>();
                item.Setup(itemConfig, status, 1);
            }
        } else {
            Debug.LogErrorFormat("Item with id {0} not found in database", itemIdx);
        }

        return item;
    }

    /// <summary>
    /// Create a reference of specific item given by his id at specific position.
    /// Create item from pool if it is pooleable else it's instantiated
    /// </summary>
    /// <param name="itemIdx">Item index of scriptable item config (uniq)</param>
    /// <param name="position">Position to create item</param>
    /// <param name="status">Status to init item (ACTIVE, PICKABLE, INACTIVE)</param>
    public Item CreateItem(int itemIdx, ItemStatus status, Vector3 position) {
        Item item = this.CreateItem(itemIdx, status);
        item.transform.position = position;
        return item;
    }

    /// <summary>
    /// Create a reference of specific item given by his id at specific position with specific rotation.
    /// Create item from pool if it is pooleable else it's instantiated
    /// </summary>
    /// <param name="itemIdx">Item index of scriptable item config (uniq)</param>
    /// <param name="position">Position to create item</param>
    /// <param name="rotation">Rotation of item</param>
    /// <param name="status">Status to init item (ACTIVE, PICKABLE, INACTIVE)</param>
    public Item CreateItem(int itemIdx, ItemStatus status, Vector3 position, Quaternion rotation) {
        Item item = this.CreateItem(itemIdx, status, position);
        item.transform.rotation = rotation;
        return item;
    }

    /// <summary>
    /// Call destroy method for each item in array
    /// </summary>
    /// <param name="items"></param>
    public void DestroyItems(Item[] items) {
        for(int i = 0; i < items.Length; i++) {
            items[i].Destroy();
        }
    }

    /// <summary>
    /// Create a pool for a specific item config
    /// </summary>
    /// <param name="itemConfig">Item config</param>
    /// <returns></returns>
    private ItemPool CreatePool(ItemConfig itemConfig) {
        GameObject poolContainer = new GameObject("Pool of : " + itemConfig.GetDisplayName());
        poolContainer.transform.parent = this.transform;

        ItemPool pool = poolContainer.AddComponent<ItemPool>();
        pool.Setup(itemConfig);

        return pool;
    }

    /// <summary>
    /// Used check item config validity to avoid problem later
    /// Do all controls here
    /// </summary>
    /// <param name="itemConfig">Item to check</param>
    private void CheckItemValidity(ItemConfig itemConfig) {
        if(!itemConfig.GetPrefab()) {
            Debug.LogErrorFormat("Item config with id {0} haven't prefab", itemConfig.GetId());
        }

        if(itemConfig.GetDisplayName() == "") {
            Debug.LogErrorFormat("Item config with id {0} haven't display name", itemConfig.GetId());
        }

        if(!itemConfig.GetIcon()) {
            Debug.LogErrorFormat("Item config with id {0} haven't icon", itemConfig.GetId());
        }
    }
}
