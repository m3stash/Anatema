using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnnemyManager : MonoBehaviour {
    private Dictionary<int, EnnemyConfig> ennemyDatabase; // Contains all ennemies (Config)
    private Dictionary<int, EnnemyPool> pools;

    public static EnnemyManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    public static void Init() {
        EnnemyManager.instance.InitFromInstance();
    }

    private void InitFromInstance() {
        // Initialize ennemy database with all ennemy configs
        this.ennemyDatabase = new List<EnnemyConfig>(Resources.LoadAll<EnnemyConfig>("Scriptables/Ennemies")).ToDictionary((EnnemyConfig ennemy) => ennemy.GetId(), ennemy => ennemy);

        // Check all ennemies validity
        foreach (KeyValuePair<int, EnnemyConfig> arg in this.ennemyDatabase) {
            this.CheckEnnemyValidity(arg.Value);
        }

        // Initialize pools foreach ennemy which are pooleable
        this.pools = this.ennemyDatabase
            .Where((KeyValuePair<int, EnnemyConfig> arg) => arg.Value.IsPooleable())
            .ToDictionary(pair => pair.Key, pair => this.CreatePool(pair.Value));
    }


    /// <summary>
    /// Return ennemy config for specific ennemy id
    /// </summary>
    /// <param name="id">Id of ennemy</param>
    /// <returns></returns>
    public EnnemyConfig GetEnnemyWithId(int id) {
        if (this.ennemyDatabase.ContainsKey(id)) {
            return this.ennemyDatabase[id];
        }
        return null;
    }

    /// <summary>
    /// Create a reference of specific ennemy given by his id.
    /// Create ennemy from pool if it is pooleable else it's instantiated
    /// </summary>
    /// <param name="ennemyIdx">ennemy index of scriptable ennemy config (uniq)</param>
    public Ennemy CreateEnnemy(int ennemyIdx) {
        EnnemyConfig ennemyConfig = this.ennemyDatabase[ennemyIdx];
        Ennemy ennemy = null;

        if (this.ennemyDatabase.ContainsKey(ennemyIdx)) {
            if (this.pools.ContainsKey(ennemyIdx)) {
                EnnemyPool pool = this.pools[ennemyIdx];
                ennemy = pool.GetOne();
                ennemy.Setup(ennemyConfig, pool);
            } else {
                GameObject obj = Instantiate(ennemyConfig.GetPrefab());
                ennemy = obj.GetComponent<Ennemy>();
                ennemy.Setup(ennemyConfig);
            }
        } else {
            Debug.LogErrorFormat("ennemy with id {0} not found in database", ennemyIdx);
        }
        return ennemy;
    }

    /// <summary>
    /// Create a reference of specific ennemy given by his id at specific position.
    /// Create ennemy from pool if it is pooleable else it's instantiated
    /// </summary>
    /// <param name="ennemyIdx">ennemy index of scriptable ennemy config (uniq)</param>
    /// <param name="position">Position to create ennemy</param>
    public Ennemy CreateEnnemy(int ennemyIdx, Vector3 position) {
        Ennemy ennemy = this.CreateEnnemy(ennemyIdx);
        ennemy.transform.position = position;
        return ennemy;
    }

    /// <summary>
    /// Create a reference of specific ennemy given by his id at specific position with specific rotation.
    /// Create ennemy from pool if it is pooleable else it's instantiated
    /// </summary>
    /// <param name="ennemyIdx">ennemy index of scriptable ennemy config (uniq)</param>
    /// <param name="position">Position to create ennemy</param>
    /// <param name="rotation">Rotation of ennemy</param>
    public Ennemy CreateEnnemy(int ennemyIdx, Vector3 position, Quaternion rotation) {
        Ennemy ennemy = this.CreateEnnemy(ennemyIdx, position);
        ennemy.transform.rotation = rotation;
        return ennemy;
    }

    /// <summary>
    /// Call destroy method for each ennemy in array
    /// </summary>
    /// <param name="ennemies"></param>
    public void DestroyEnnemies(Ennemy[] ennemies) {
        for (int i = 0; i < ennemies.Length; i++) {
            ennemies[i].Destroy();
        }
    }

    /// <summary>
    /// Create a pool for a specific ennemy config
    /// </summary>
    /// <param name="ennemyConfig">ennemy config</param>
    /// <returns></returns>
    private EnnemyPool CreatePool(EnnemyConfig ennemyConfig) {
        GameObject poolContainer = new GameObject("Pool of : " + ennemyConfig.GetDisplayName());
        poolContainer.transform.parent = this.transform;
        EnnemyPool pool = poolContainer.AddComponent<EnnemyPool>();
        pool.Setup(ennemyConfig);
        return pool;
    }

    /// <summary>
    /// Used check ennemy config validity to avoid problem later
    /// Do all controls here
    /// </summary>
    /// <param name="ennemyConfig">ennemy to check</param>
    private void CheckEnnemyValidity(EnnemyConfig ennemyConfig) {
        if (!ennemyConfig.GetPrefab()) {
            Debug.LogErrorFormat("Ennemy config with id {0} haven't prefab", ennemyConfig.GetId());
        }
        if (ennemyConfig.GetDisplayName() == "") {
            Debug.LogErrorFormat("Ennemy config with id {0} haven't display name", ennemyConfig.GetId());
        }
    }
}
