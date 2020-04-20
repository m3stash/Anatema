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
        SetButtonColors(startButton);
        SetButtonColors(optionsButton);
        SetButtonColors(quitButton);
    }

    private void OnEnable() {
        EnablePerformed();
        EnableListeners();
    }

    private void OnDisable() {
        DisablePerformed();
        DisableListeners();
    }

    private void OnDestroy() {
        DisablePerformed();
        DisableListeners();
    }

    private void SetButtonColors(Button button) {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = new Color(1, 1, 1, 0);
        colorBlock.selectedColor = new Color(0.7843137f, 0.1568628f, 0.1568628f, 0.8f);
        colorBlock.highlightedColor = new Color(0.5960785f, 0.1372549f, 0.1372549f, 1f);
        button.colors = colorBlock;
    }

    private void SetNormalColor() {
        ColorBlock colorBlock = currentSelectedButton.colors;
        colorBlock.normalColor = new Color(0.7843137f, 0.1568628f, 0.1568628f, 0.8f);
        currentSelectedButton.colors = colorBlock;
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

    private void EnableListeners() {
        startButton.onClick.AddListener(() => SetStartMenu());
        optionsButton.onClick.AddListener(() => SetOptionMenu());
        quitButton.onClick.AddListener(() => SetQuit());
    }

    private void DisableListeners() {
        startButton.onClick.RemoveAllListeners();
        optionsButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }

    private void SetButtonState(Button button) {
        SetButtonColors(startButton);
        SetButtonColors(optionsButton);
        SetButtonColors(quitButton);
        button.Select();
        currentSelectedButton = button;
        SetNormalColor();
    }

    private void Navigate(InputAction.CallbackContext ctx) {
        Selectable neighbour = CommonService.GetNeighboorSelectable(ctx.ReadValue<Vector2>(), currentSelectedButton);
        if (neighbour) {
            Button button = neighbour.GetComponent<Button>();
            SetButtonState(button);
        }
    }

    void Update() {
        // for preserve selection => clicking anywhere makes you lose button focus !
        if (Input.GetMouseButtonDown(0)) {
            currentSelectedButton.Select();
        }
    }

    private void Cancel(InputAction.CallbackContext ctx) {
        MainMenuManagement.instance.MainMenu();
    }

    private void Select(InputAction.CallbackContext ctx) {
        currentSelectedButton.onClick.Invoke();
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
