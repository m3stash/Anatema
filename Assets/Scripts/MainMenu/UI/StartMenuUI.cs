using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class StartMenuUI : MonoBehaviour {

    public Action<bool> dialogCallBack;
    private StartMenuSlot currentSlot;
    [SerializeField]private Button defaultButtonSelected;
    private Button currentSelectedButton;

    private void OnEnable() {
        EnablePerformed();
        currentSelectedButton = defaultButtonSelected;
    }
    private void OnDisable() {
        DisablePerformed();
    }

    private void Start() {
        SetButtonState(defaultButtonSelected);
    }

    private void OnDestroy() {
        DisablePerformed();
    }
    private void SetButtonState(Button button) {
        button.Select();
        currentSelectedButton = button;
    }

    private void EnablePerformed() {
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Navigate.performed += Navigate;
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Cancel.performed += Cancel;
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Select.performed += Select;
        StartMenuSlot.OnSelect += SelectSlot;
        StartMenuSlot.OnDelete += DeleteSlot;
        dialogCallBack += Delete;
    }

    private void DisablePerformed() {
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Navigate.performed -= Navigate;
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Cancel.performed -= Cancel;
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Select.performed -= Select;
        StartMenuSlot.OnSelect -= SelectSlot;
        StartMenuSlot.OnDelete -= DeleteSlot;
        dialogCallBack -= Delete;
    }

    private void Navigate(InputAction.CallbackContext ctx) {
        Selectable neighbour = CommonService.GetNeighboorSelectableEnable(ctx.ReadValue<Vector2>(), currentSelectedButton);
        if (neighbour) {
            // EventSystem.current.SetSelectedGameObject(startButton.gameObject);
            Button button = neighbour.GetComponent<Button>();
            SetButtonState(button);
        }
    }

    private void Cancel(InputAction.CallbackContext ctx) {
        MainMenuManagement.instance.MainMenu();
    }

    private void Select(InputAction.CallbackContext ctx) {
        currentSelectedButton.onClick.Invoke();
    }


    private void SelectSlot(StartMenuSlot slot, Button button) {
        SetButtonState(button);
        if (slot.IsNewGame()) {
            GameMaster.instance.NewGame(slot.GetSlotNumber());
        } else {
            GameMaster.instance.LoadSave(slot.GetSlotNumber());
        }

    }
    private void DeleteSlot(StartMenuSlot slot, Button button) {
        SetButtonState(button);
        currentSlot = slot;
        string modalTitle = "title";
        string msg = "Etes vous sur de vouloir supprimer la partie " + slot.GetSlotNumber() + " ?";
        MainMenuManagement.instance.ShowDialogModal(true);
        DialogModalService.instance.Open(new DialogModalConf(modalTitle, msg, Delete));
    }

    private void Delete(bool canDelete) {
        if (canDelete) {
            int slotNumber = currentSlot.GetSlotNumber();
            GameMaster.instance.DeleteSave(slotNumber);
            currentSlot.InitValue();
            SetButtonState(currentSlot.GetSlotButton());
        } else {
            SetButtonState(currentSelectedButton);
        }
        currentSlot = null;
        MainMenuManagement.instance.ShowDialogModal(false);
    }

}
