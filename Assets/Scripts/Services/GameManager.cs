using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Don't touch it")]
    [SerializeField] private GameMode gameMode = GameMode.DEFAULT;

    public delegate void GameModeChanged(GameMode gameMode);
    public static event GameModeChanged OnGameModeChanged;

    public static GameManager instance;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        InputManager.gameplayControls.Shortcuts.build.performed += BuildModeTriggered;

        this.SetGameMode(GameMode.DEFAULT);
    }

    private void OnDestroy() {
        InputManager.gameplayControls.Shortcuts.build.performed -= BuildModeTriggered;
    }

    public void SetGameMode(GameMode gameMode) {
        this.gameMode = gameMode;

        switch (this.gameMode) {
            case GameMode.BUILD:
                // Disable weapon
                // Display toolbar
                // Enable tile selector with grid
                InputManager.gameplayControls.TileSelector.Enable();
                InputManager.gameplayControls.Toolbar.Enable();
                break;
            case GameMode.TOOL:
                // Put tool item in hand
                // Remove toolbar
                // Enable tile selector without grid
                break;
            case GameMode.DEFAULT:
                // Put sword in hand
                // Disable tile selector
                InputManager.gameplayControls.TileSelector.Disable();
                InputManager.gameplayControls.Toolbar.Disable();
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
}

public enum GameMode
{
    DEFAULT,
    BUILD,
    TOOL,
    POTION
}
