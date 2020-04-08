using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StartMenuUI : MonoBehaviour {

    // [SerializeField] private Button continueButton;
    // [SerializeField] private Button newGameButton;
    private Vector2 lastMoveDirection;

    private void Awake() {
        // continueButton = continueButton.GetComponent<Button>();
        // newGameButton = newGameButton.GetComponent<Button>();
    }
    void Start() {
        /*continueButton.onClick.AddListener(() => {
            MainMenuManagement.instance.Continue();
        });

        newGameButton.onClick.AddListener(() => {
            MainMenuManagement.instance.NewGame();
        });*/
    }

    private void OnEnable() {
        EnablePerformed();
    }
    private void OnDisable() {
        DisablePerformed();
    }

    private void OnDestroy() {
        DisablePerformed();
        DisableListeners();
    }

    private void EnablePerformed() {
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Navigate.performed += Navigate;
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Cancel.performed += Cancel;
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Select.performed += Select;
        StartMenuSlot.OnSelect += SelectSlot;
        // InputMainMenuManager.instance.mainMenuControls.LoadSave.Delete.performed += Delete;
    }

    private void DisablePerformed() {
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Navigate.performed -= Navigate;
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Cancel.performed -= Cancel;
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Select.performed -= Select;
        // InputMainMenuManager.instance.mainMenuControls.LoadSave.Delete.performed -= Delete;
    }

    private void DisableListeners() {
        // continueButton.onClick.RemoveAllListeners();
        // newGameButton.onClick.RemoveAllListeners();
    }

    private void Navigate(InputAction.CallbackContext ctx) {
        Vector2 direction = ctx.ReadValue<Vector2>();
        if (direction != lastMoveDirection) {
            Debug.Log(direction.ToString());
            lastMoveDirection = direction;
        }
    }

    private void Cancel(InputAction.CallbackContext ctx) {
        MainMenuManagement.instance.GoToMainMenu();
    }

    private void SelectSlot(StartMenuSlot slot) {
        Debug.Log(slot.GetSlotNumber());
    }

    private void Select(InputAction.CallbackContext ctx) {
        // GameMaster.instance.NewGame();
        // Debug.Log("Select");
    }
}
