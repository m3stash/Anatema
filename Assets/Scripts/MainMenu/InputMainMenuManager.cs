using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// [RequireComponent(typeof(Button))]
public class InputMainMenuManager : MonoBehaviour {

    public static InputMainMenuManager instance;
    public MainMenuControls mainMenuControls;
    private MainMenuLayout currentLayout;

    private void Awake() {
        instance = this;
        mainMenuControls = new MainMenuControls();
        mainMenuControls.Enable();
        currentLayout = MainMenuLayout.LEFTMENU;
    }

    private void Start() {
        // active descative les menu !!
        mainMenuControls.LeftMenu.Enable();
        mainMenuControls.StartMenu.Enable();
        mainMenuControls.OptionsMenu.Enable();
    }

    private void OnDestroy() {
        DisableLayouts();
        mainMenuControls.Disable();
    }

    private void DisableLayouts() {
        mainMenuControls.LeftMenu.Disable();
        mainMenuControls.StartMenu.Disable();
        mainMenuControls.OptionsMenu.Disable();
    }

    public void SetLayout(MainMenuLayout layout) {
        DisableLayouts();
        currentLayout = layout;
        switch (layout) {
            case MainMenuLayout.LEFTMENU:
                mainMenuControls.LeftMenu.Enable();
                break;
            case MainMenuLayout.STARTMENU:
                mainMenuControls.StartMenu.Enable();
                break;
            case MainMenuLayout.OPTIONS:
                mainMenuControls.OptionsMenu.Enable();
                break;
        }
    }
}

public enum MainMenuLayout {
    LEFTMENU,
    STARTMENU,
    OPTIONS
}