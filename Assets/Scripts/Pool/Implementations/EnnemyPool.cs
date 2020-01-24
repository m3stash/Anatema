using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPool : Pool<Ennemy> {
    [SerializeField] private EnnemyConfig config;

    /// <summary>
    /// This method is used to init our first reference object for pooling from an item config
    /// </summary>
    /// <param name="config">Item configuration</param>
    public void Setup(EnnemyConfig config) {
        this.config = config;

        GameObject obj = Instantiate(config.GetPrefab(), this.transform);
        Ennemy ennemy = obj.GetComponent<Ennemy>();

        obj.name = config.GetDisplayName();
        obj.SetActive(false);

        base.Setup(ennemy, config.GetPoolSize());
    }
}
