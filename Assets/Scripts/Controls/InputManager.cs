using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {

    [SerializeField] private View currentView;

    private ViewControls viewControls;

    public static Vector2 mousePosition;

    public static Controls controls;

    public static InputManager instance;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        this.SetView(View.DEFAULT);
    }

    private void OnEnable() {
        if(this.viewControls == null) {
            this.viewControls = new ViewControls();
            this.viewControls.Enable();
        }

        if(controls == null) {
            controls = new Controls();
            controls.Enable();
        }

        controls.Core.Enable();
        controls.Core.Position.performed += ctx => this.SetMousePosition(ctx.ReadValue<Vector2>());

        this.viewControls.Inventory.Enable();
        this.viewControls.Inventory.SwitchDisplay.performed += ctx => SetView(View.INVENTORY);

        this.viewControls.EscapeMenu.Enable();
        this.viewControls.EscapeMenu.SwitchDisplay.performed += ctx => SetView(View.MENU);

        StartCoroutine(this.InitConsumers());
    }

    private IEnumerator InitConsumers() {
        while(!Player.instance) {
            yield return null;
        }

        Player.instance.InitControls();
    }

    private void OnDisable() {
        StopAllCoroutines();

        this.viewControls.Inventory.Disable();
        this.viewControls.EscapeMenu.Disable();

        this.viewControls.Disable();

        controls.Core.Disable();
        controls.Player.Disable();
        controls.Inventory.Disable();
        controls.Toolbar.Disable();
        controls.Disable();
    }

    private void SetMousePosition(Vector2 position) {
        mousePosition = position;
    }

    private void SetView(View view) {
        // If view is same than current view we need to undisplay it
        this.currentView = view != this.currentView ? view : View.DEFAULT;

        switch(this.currentView) {
            case View.INVENTORY:
                controls.Inventory.Enable();

                controls.Player.Disable();
                controls.TileSelector.Disable();
                break;

            case View.CRAFT:
                controls.Toolbar.Enable();

                controls.Player.Disable();
                controls.TileSelector.Disable();
                break;

            case View.MENU:
                controls.Player.Disable();
                controls.Inventory.Disable();
                controls.Toolbar.Disable();
                controls.TileSelector.Disable();
                break;

            case View.DEFAULT:
                controls.Player.Enable();
                controls.TileSelector.Enable();
                break;
        }
    }
}

public enum View {
    DEFAULT,
    INVENTORY,
    MENU,
    CRAFT,
}
