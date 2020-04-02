using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// [RequireComponent(typeof(Button))]
public class InputMainMenuManager : MonoBehaviour {

    public static InputMainMenuManager instance;
    private MainMenuControls mainMenuControls;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;

    private void Awake() {
        instance = this;
        // Init layout controls
        mainMenuControls = new MainMenuControls();
        mainMenuControls.Enable();
        continueButton = continueButton.GetComponent<Button>();
        newGameButton = newGameButton.GetComponent<Button>();
    }

    private void Start() {

        EnableMainControls();

        continueButton.onClick.AddListener(() => {
            MainMenuManagement.instance.Continue();
        });

        newGameButton.onClick.AddListener(() => {
            MainMenuManagement.instance.NewGame();
        });
    }

    private void EnableMainControls() {
        mainMenuControls.Main.Enable();
        // mainMenuControls.Main.Navigate.performed += FunctXXX;
        mainMenuControls.Main.Cancel.performed += Cancel;
        mainMenuControls.Main.Select.performed += Select;
    }

    private void Cancel(InputAction.CallbackContext ctx) {
        MainMenuManagement.instance.GoToMainMenu();
    }

    private void Select(InputAction.CallbackContext ctx) {
        // Debug.Log("Select");
    }

    private void EnableContinueControls() {
        mainMenuControls.Continue.Enable();
        // mainMenuControls.Continue.Navigate.performed += FunctXXX;
        // mainMenuControls.Continue.Select.performed += FunctXXX;
        // mainMenuControls.Continue.Delete.performed += FunctXXX;
        // mainMenuControls.Continue.Cancel.performed += FunctXXX;
    }

    private void DisableAllControls() {
        CleanDelegates();
        mainMenuControls.Disable();
        mainMenuControls.Continue.Disable();
    }
    private void CleanDelegates() {
        // mainMenuControls.Main.Navigate.performed -= FunctXXX;
        mainMenuControls.Main.Cancel.performed -= Cancel;
        mainMenuControls.Main.Select.performed -= Select;
        // mainMenuControls.Continue.Navigate.performed -= FunctXXX;
        // mainMenuControls.Continue.Select.performed -= FunctXXX;
        // mainMenuControls.Continue.Delete.performed -= FunctXXX;
        // mainMenuControls.Continue.Cancel.performed -= FunctXXX;
    }

    private void OnDestroy() {
        DisableAllControls();
        DisableListeners();
    }

    private void DisableListeners() {
        continueButton.onClick.RemoveAllListeners();
        newGameButton.onClick.RemoveAllListeners();
    }
}
