using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class LeftMenuUI : MonoBehaviour {

    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    private Button currentSelectedButton;

    private void Awake() {
        startButton = startButton.GetComponent<Button>();
        optionsButton = optionsButton.GetComponent<Button>();
        quitButton = quitButton.GetComponent<Button>();
        SetButtonState(startButton);
    }
    void Start() {
        startButton.onClick.AddListener(() => SetStartMenu());
        optionsButton.onClick.AddListener(() => SetOptionMenu());
        quitButton.onClick.AddListener(() => SetQuit());
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

    private void SetButtonState(Button button) {
        button.Select();
        currentSelectedButton = button;
        button.GetComponent<Image>().color = new Color(200, 40, 40, 0.7f);
    }

    private void Navigate(InputAction.CallbackContext ctx) {
        Selectable neighbour = CommonService.GetNeighboorSelectable(ctx.ReadValue<Vector2>(), currentSelectedButton);
        if (neighbour) {
            // EventSystem.current.SetSelectedGameObject(startButton.gameObject);
            Button button = neighbour.GetComponent<Button>();
            SetButtonState(button);
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            currentSelectedButton.Select();
        }
    }

    private void Cancel(InputAction.CallbackContext ctx) {
        MainMenuManagement.instance.MainMenu();
    }

    private void Select(InputAction.CallbackContext ctx) {
        MainMenuLayout layout = currentSelectedButton.GetComponent<MainMenuButton>().GetLayoutType();
        switch (layout) {
            case MainMenuLayout.STARTMENU:
                SetStartMenu();
                break;
            case MainMenuLayout.OPTIONS:
                SetOptionMenu();
                break;
            case MainMenuLayout.QUIT:
                SetQuit();
                break;
        }
    }

    private void SetStartMenu() {
        MainMenuManagement.instance.StartMenu();
        SetButtonState(startButton);
    }

    private void SetOptionMenu() {
        MainMenuManagement.instance.Options();
        SetButtonState(optionsButton);
    }

    private void SetQuit() {
        MainMenuManagement.instance.Quit();
        SetButtonState(quitButton);
    }

}
