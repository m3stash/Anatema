using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : Pool<Item>
{
    [SerializeField] private ItemConfig config;

    /// <summary>
    /// This method is used to init our first reference object for pooling from an item config
    /// </summary>
    /// <param name="config">Item configuration</param>
    public void Setup(ItemConfig config) {
        this.config = config;

        GameObject obj = Instantiate(config.GetPrefab(), this.transform);
        Item item = obj.GetComponent<Item>();
        item.Setup(this.config, this);

        obj.name = config.GetDisplayName();
        obj.SetActive(false);

        base.Setup(item, config.GetPoolSize());
    }
}
