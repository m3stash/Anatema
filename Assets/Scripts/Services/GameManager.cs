using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Fields to complete")]
    [SerializeField] private TileSelector tileSelectorPrefab;

    [Header("Don't touch it")]
    [SerializeField] private GameMode gameMode = GameMode.DEFAULT;
    [SerializeField] private TileSelector tileSelectorReference;

    public delegate void GameModeChanged(GameMode gameMode);
    public static event GameModeChanged OnGameModeChanged;

    public static GameManager instance;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        InputManager.gameplayControls.Shortcuts.build.performed += BuildModeTriggered;
        InputManager.gameplayControls.Shortcuts.tool.performed += ToolModeTriggered;
        InputManager.gameplayControls.Shortcuts.weapon.performed += WeaponModeTriggered;

        this.tileSelectorReference = Instantiate(this.tileSelectorPrefab);
        this.tileSelectorReference.gameObject.SetActive(false);

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

        this.tileSelectorReference.gameObject.SetActive(false);

        switch (this.gameMode) {
            case GameMode.BUILD:
                this.tileSelectorReference.SetShowGrid(true);
                this.tileSelectorReference.gameObject.SetActive(true);

                InputManager.gameplayControls.TileSelector.Enable();
                InputManager.gameplayControls.Toolbar.Enable();
                break;
            case GameMode.TOOL:
                this.tileSelectorReference.SetShowGrid(false);
                this.tileSelectorReference.gameObject.SetActive(true);

                InputManager.gameplayControls.TileSelector.Enable();
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
        InputManager.gameplayControls.Toolbar.Disable();

        // Disable shortcuts to avoid to use them in build mode
        InputManager.gameplayControls.Shortcuts.weapon.Disable();
        InputManager.gameplayControls.Shortcuts.tool.Disable();
    }
}

public enum GameMode
{
    DEFAULT,
    BUILD,
    TOOL,
    POTION
}
