using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour {
    // KEEP only this properties bellow
    [Header("Base configuration (DON'T PUT SOMETHING IN BELOW FIELD)")]
    [SerializeField] private EnnemyConfig configuration;
    [SerializeField] private EnnemyPool associatedPool;

    /// <summary>
    /// Used to remove object from the map
    /// If object was get from a pool it is return to it;
    /// If object is just an simple instantiation it'll be destroyed completely
    /// </summary>
    public virtual void Destroy() {
        // Check if this is not null to avoid error after game stopped
        if (this) {
            if (this.associatedPool) {
                this.associatedPool.ReturnObject(this);
            } else {
                Destroy(this.gameObject);
            }
        }
    }

    public virtual void Setup(EnnemyConfig config, EnnemyPool associatedPool = null) {
        // Get components references
        this.configuration = config;
        this.associatedPool = associatedPool;
        this.transform.name = this.configuration.GetDisplayName();
        this.gameObject.SetActive(true);
        this.GetComponent<EnnemyBrain>().Setup(this.configuration);
        // this.rigidbody = GetComponent<Rigidbody2D>();
        // this.renderer = GetComponent<SpriteRenderer>();
        // this.defaultScale = this.transform.localScale;
    }

}
