using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMainMenuManager : MonoBehaviour {

    public static InputMainMenuManager instance;
    public MainMenuControls mainMenuControls;

    private void Awake() {
        instance = this;
        this.mainMenuControls = new MainMenuControls();
    }

    private void Start() {
       
    }

    private void OnDestroy() {
        
    }
}
