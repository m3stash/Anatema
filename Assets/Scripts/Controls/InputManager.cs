using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LayoutManager))]
[RequireComponent(typeof(CursorManager))]
public class InputManager : MonoBehaviour
{
    [SerializeField] private bool mouseEnabled = true;
    [SerializeField] private Layout currentLayout;

    private LayoutManager layoutManager;
    private CursorManager cursorManager;

    private LayoutControls layoutControls;

    public static Vector2 mousePosition;

    public static GameplayControls gameplayControls;

    public static InputManager instance;

    private void Awake() {
        instance = this;

        this.layoutManager = GetComponent<LayoutManager>();
        this.cursorManager = GetComponent<CursorManager>();

        // Init layout controls
        this.layoutControls = new LayoutControls();
        this.layoutControls.Enable();

        // Init gameplay controls
        gameplayControls = new GameplayControls();
        gameplayControls.Enable();
    }

    private void Start() {
        gameplayControls.Core.Enable();
        gameplayControls.Core.Position.performed += SetMousePosition;

        layoutControls.Inventory.Enable();
        layoutControls.Inventory.SwitchDisplay.performed += SwitchInventoryLayout;

        layoutControls.EscapeMenu.Enable();
        layoutControls.EscapeMenu.SwitchDisplay.performed += SwitchMenuLayout;

        this.SetLayout(Layout.DEFAULT);
    }

    private void OnDestroy() {
        layoutControls.Inventory.SwitchDisplay.performed -= SwitchInventoryLayout;
        layoutControls.EscapeMenu.SwitchDisplay.performed -= SwitchMenuLayout;
        gameplayControls.Core.Position.performed -= SetMousePosition;

        // Disable layout controls
        this.DisableLayoutControls();
        layoutControls.Disable();

        // Disable gameplay controls
        this.DisableGameplayControls();
        gameplayControls.Core.Disable();
        gameplayControls.Disable();
    }

    private void SetMousePosition(InputAction.CallbackContext ctx) {
        mousePosition = ctx.ReadValue<Vector2>();
    }

    public bool IsMouseEnabled() {
        return this.mouseEnabled;
    }

    public void SetMouseEnable(bool value) {
        this.mouseEnabled = value;
    }

    private void DisableLayoutControls() {
        layoutControls.Inventory.Disable();
        layoutControls.EscapeMenu.Disable();
    }

    private void DisableGameplayControls() {
        gameplayControls.Player.Disable();
        gameplayControls.Inventory.Disable();
        gameplayControls.Toolbar.Disable();
        gameplayControls.Shortcuts.Disable();
        gameplayControls.TileSelector.Disable();
    }

    private void SwitchInventoryLayout(InputAction.CallbackContext ctx) {
        this.SetLayout(Layout.INVENTORY);
    }

    private void SwitchMenuLayout(InputAction.CallbackContext ctx) {
        this.SetLayout(Layout.MENU);
    }

    private void SetLayout(Layout layout) {
        // If view is same than current view we need to undisplay it
        this.currentLayout = layout != this.currentLayout ? layout : Layout.DEFAULT;

        this.DisableGameplayControls();

        switch (this.currentLayout) {
            case Layout.INVENTORY:
                gameplayControls.Inventory.Enable();
                GameManager.instance.SetGameMode(GameMode.DEFAULT);
                this.cursorManager.SetCursorState(CursorState.INVENTORY_NAVIGATION);
                break;

            case Layout.MENU:
                // TODO to implement
                break;

            case Layout.DEFAULT:
                gameplayControls.Toolbar.Enable();
                gameplayControls.Player.Enable();
                gameplayControls.TileSelector.Enable();
                gameplayControls.Shortcuts.Enable();

                // Disabled cursor in default mode
                this.cursorManager.SetCursorState(CursorState.DISABLED);
                break;
        }

        this.layoutManager.ChangeHUD(this.currentLayout);
    }
}

public enum Layout
{
    DEFAULT,
    INVENTORY,
    MENU
}

public enum InputType
{
    MOUSE,
    CONTROLLER
}
