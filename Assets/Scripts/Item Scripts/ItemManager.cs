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
        } else {
            Destroy(this);
        }
    }

    private void Start() {
        // Initialize item database with all item configs
        this.itemDatabase = new List<ItemConfig>(Resources.LoadAll<ItemConfig>("Scriptables/MyItems")).ToDictionary((ItemConfig item) => item.GetId(), item => item);

        // Check all items validity
        foreach(KeyValuePair<int, ItemConfig> arg in this.itemDatabase)
        {
            this.CheckItemValidity(arg.Value);
        }

        // Initialize pools foreach item which are pooleable
        this.pools = this.itemDatabase
            .Where((KeyValuePair<int, ItemConfig> arg) => arg.Value.IsPooleable())
            .ToDictionary(pair => pair.Key, pair => this.CreatePool(pair.Value));
    }

    /// <summary>
    /// Create a reference of specific item given by his id.
    /// Create item from pool if it is pooleable else it's instantiated
    /// </summary>
    /// <param name="itemIdx">Item index of scriptable item config (uniq)</param>
    public Item CreateItem(int itemIdx) {
        ItemPool pool = this.pools[itemIdx];
        ItemConfig itemConfig = this.itemDatabase[itemIdx];
        Item item = null;

        if (pool) {
            item = pool.GetOne();
        } else if(itemConfig){
            GameObject obj = Instantiate(itemConfig.GetPrefab());
            item = obj.GetComponent<Item>();
            item.Setup(itemConfig, 1);
            return item;
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
    public Item CreateItem(int itemIdx, Vector3 position) {
        Item item = this.CreateItem(itemIdx);
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
    public Item CreateItem(int itemIdx, Vector3 position, Quaternion rotation) {
        Item item = this.CreateItem(itemIdx, position);
        item.transform.rotation = rotation;
        return item;
    }

    /// <summary>
    /// Create a pool for a specific item config
    /// </summary>
    /// <param name="itemConfig">Item config</param>
    /// <returns></returns>
    private ItemPool CreatePool(ItemConfig itemConfig)
    {
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
    private void CheckItemValidity(ItemConfig itemConfig)
    {
        if(!itemConfig.GetPrefab())
        {
            Debug.LogErrorFormat("Item config with id {0} haven't prefab", itemConfig.GetId());
        }

        if (itemConfig.GetDisplayName() == "")
        {
            Debug.LogErrorFormat("Item config with id {0} haven't display name", itemConfig.GetId());
        }

        if (!itemConfig.GetIcon())
        {
            Debug.LogErrorFormat("Item config with id {0} haven't icon", itemConfig.GetId());
        }
    }
}
