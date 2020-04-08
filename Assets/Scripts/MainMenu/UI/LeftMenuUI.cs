using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class LeftMenuUI : MonoBehaviour {

    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    private Vector2 lastMoveDirection;

    private void Awake() {
        startButton = startButton.GetComponent<Button>();
        optionsButton = optionsButton.GetComponent<Button>();
        quitButton = quitButton.GetComponent<Button>();
    }
    void Start() {
        startButton.onClick.AddListener(() => {
            MainMenuManagement.instance.StartMenu();
        });

        optionsButton.onClick.AddListener(() => {
            // options
        });

        quitButton.onClick.AddListener(() => {
            // quit
        });
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
        InputMainMenuManager.instance.mainMenuControls.LeftMenu.Navigate.performed += Navigate;
        InputMainMenuManager.instance.mainMenuControls.LeftMenu.Cancel.performed += Cancel;
        InputMainMenuManager.instance.mainMenuControls.LeftMenu.Select.performed += Select;
    }

    private void DisablePerformed() {
        InputMainMenuManager.instance.mainMenuControls.LeftMenu.Navigate.performed -= Navigate;
        InputMainMenuManager.instance.mainMenuControls.LeftMenu.Cancel.performed -= Cancel;
        InputMainMenuManager.instance.mainMenuControls.LeftMenu.Select.performed -= Select;
    }

    private void DisableListeners() {
        startButton.onClick.RemoveAllListeners();
        optionsButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
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

    private void Select(InputAction.CallbackContext ctx) {
        // Debug.Log("Select");
    }

}
