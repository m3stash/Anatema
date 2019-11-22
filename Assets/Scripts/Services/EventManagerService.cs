using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerService : MonoBehaviour {

    public delegate void PlayerLoaded(int intenisty);
    public static event PlayerLoaded SendPlayer;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
