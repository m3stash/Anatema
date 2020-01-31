using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    [SerializeField] private Layout currentLayout;

    private LayoutControls layoutControls;

    public static Vector2 mousePosition;

    public static GameplayControls gameplayControls;

    public static InputManager instance;

    public delegate void LayoutChanged(Layout layout);
    public static LayoutChanged OnViewChanged;

    private void Awake() {
        instance = this;

        // Init layout controls
        this.layoutControls = new LayoutControls();
        this.layoutControls.Enable();

        // Init gameplay controls
        gameplayControls = new GameplayControls();
        gameplayControls.Enable();
    }

    private void OnEnable() {
        gameplayControls.Core.Enable();
        gameplayControls.Core.Position.performed += SetMousePosition;

        layoutControls.Inventory.Enable();
        layoutControls.Inventory.SwitchDisplay.performed += SwitchInventoryLayout;

        layoutControls.EscapeMenu.Enable();
        layoutControls.EscapeMenu.SwitchDisplay.performed += SwitchMenuLayout;

        this.SetLayout(Layout.DEFAULT);
    }

    private void OnDisable() {
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
                break;

            case Layout.MENU:
                // TODO to implement
                break;

            case Layout.DEFAULT:
                gameplayControls.Toolbar.Enable();
                gameplayControls.Player.Enable();
                gameplayControls.TileSelector.Enable();
                gameplayControls.Shortcuts.Enable();
                break;
        }

        OnViewChanged?.Invoke(this.currentLayout);
    }
}

public enum Layout
{
    DEFAULT,
    INVENTORY,
    MENU
}
