using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    EnnemyConfig config;
    public void Setup(EnnemyConfig config) {
        this.config = config;
        if (this.config.CanSee()) {
            this.GetComponentInChildren<Eyes>().Setup();
        }
    }
}
