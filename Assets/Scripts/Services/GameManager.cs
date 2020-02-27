using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour {
    [Header("Fields to complete")]
    [SerializeField] private GameObject worldManager;
    [SerializeField] private BuildSelector tileSelectorPrefab;

    [Header("Don't touch it")]
    [SerializeField] private GameMode gameMode = GameMode.DEFAULT;
    [SerializeField] private BuildSelector buildSelector;
    [SerializeField] private ToolSelector toolSelector;

    public delegate void GameModeChanged(GameMode gameMode);
    public static event GameModeChanged OnGameModeChanged;

    public static GameManager instance;

    private void Awake() {
        instance = this;

        this.toolSelector = GetComponent<ToolSelector>();
        this.toolSelector.enabled = false;
    }

    private void Start() {
        ItemManager.instance.Init();
        worldManager.SetActive(true);
        // GameObject.FindGameObjectWithTag("WorldMap").gameObject.SetActive(true);
        InputManager.gameplayControls.Shortcuts.build.performed += BuildModeTriggered;
        InputManager.gameplayControls.Shortcuts.tool.performed += ToolModeTriggered;
        InputManager.gameplayControls.Shortcuts.weapon.performed += WeaponModeTriggered;

        this.buildSelector = Instantiate(this.tileSelectorPrefab);
        this.buildSelector.gameObject.SetActive(false);

        this.SetGameMode(GameMode.DEFAULT);
    }

    private void OnDestroy() {
        InputManager.gameplayControls.Shortcuts.build.performed -= BuildModeTriggered;
        InputManager.gameplayControls.Shortcuts.tool.performed -= ToolModeTriggered;
        InputManager.gameplayControls.Shortcuts.weapon.performed -= WeaponModeTriggered;
    }

    public void SetGameMode(GameMode gameMode) {
        this.gameMode = gameMode;

        // Disable all controls and enable only usefull
        this.DisableAllGameplayControls();

        this.buildSelector.gameObject.SetActive(false);
        this.toolSelector.enabled = false;

        switch(this.gameMode) {
            case GameMode.BUILD:
                this.buildSelector.SetShowGrid(true);
                this.buildSelector.gameObject.SetActive(true);

                InputManager.gameplayControls.TileSelector.Enable();
                InputManager.gameplayControls.Toolbar.Enable();
                break;
            case GameMode.TOOL:
                this.toolSelector.enabled = true;

                InputManager.gameplayControls.ToolSelector.Enable();
                InputManager.gameplayControls.Shortcuts.weapon.Enable();
                break;
            case GameMode.DEFAULT:
                // Put sword in hand
                InputManager.gameplayControls.Shortcuts.tool.Enable();
                break;
        }

        OnGameModeChanged?.Invoke(this.gameMode);
    }

    public GameMode GetGameMode() {
        return this.gameMode;
    }

    /// <summary>
    /// Callback when input build has been pressed
    /// If already build mode, pass to default (Switch system)
    /// </summary>
    /// <param name="ctx"></param>
    private void BuildModeTriggered(InputAction.CallbackContext ctx) {
        this.SetGameMode((this.gameMode == GameMode.BUILD && gameMode == GameMode.BUILD) ? GameMode.DEFAULT : GameMode.BUILD);
    }

    /// <summary>
    /// Callback when input tool has been pressed
    /// </summary>
    /// <param name="ctx"></param>
    private void ToolModeTriggered(InputAction.CallbackContext ctx) {
        this.SetGameMode(GameMode.TOOL);
    }

    /// <summary>
    /// Callback when input weapon has been pressed
    /// </summary>
    /// <param name="ctx"></param>
    private void WeaponModeTriggered(InputAction.CallbackContext ctx) {
        this.SetGameMode(GameMode.DEFAULT);
    }

    private void DisableAllGameplayControls() {
        InputManager.gameplayControls.TileSelector.Disable();
        InputManager.gameplayControls.ToolSelector.Disable();
        InputManager.gameplayControls.Toolbar.Disable();

        // Disable shortcuts to avoid to use them in build mode
        InputManager.gameplayControls.Shortcuts.weapon.Disable();
        InputManager.gameplayControls.Shortcuts.tool.Disable();
    }
}

public enum GameMode {
    DEFAULT,
    BUILD,
    TOOL,
    POTION
}
