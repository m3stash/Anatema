using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class StartMenuUI : MonoBehaviour {

    public Action<bool> dialogCallBack;
    private StartMenuSlot currentSlot;
    private Button currentSelectedButton;

    private void OnEnable() {
        EnablePerformed();
    }
    private void OnDisable() {
        DisablePerformed();
    }

    private void OnDestroy() {
        DisablePerformed();
    }

    private void EnablePerformed() {
        InputMainMenuManager.instance.mainMenuControls.DialogModal.Select.performed += ModalSelect;
        InputMainMenuManager.instance.mainMenuControls.DialogModal.Navigate.performed += ModalNavigate;
        InputMainMenuManager.instance.mainMenuControls.DialogModal.Cancel.performed += ModalCancel;
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Navigate.performed += Navigate;
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Cancel.performed += Cancel;
        StartMenuSlot.OnSelect += SelectSlot;
        StartMenuSlot.OnDelete += DeleteSlot;
        dialogCallBack += Delete;
    }

    private void DisablePerformed() {
        InputMainMenuManager.instance.mainMenuControls.DialogModal.Select.performed -= ModalSelect;
        InputMainMenuManager.instance.mainMenuControls.DialogModal.Navigate.performed -= ModalNavigate;
        InputMainMenuManager.instance.mainMenuControls.DialogModal.Cancel.performed -= ModalCancel;
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Navigate.performed -= Navigate;
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Cancel.performed -= Cancel;
        dialogCallBack -= Delete;
    }

    private void ModalSelect(InputAction.CallbackContext ctx) {
    }

    private void ModalNavigate(InputAction.CallbackContext ctx) {
        //Vector2 direction = ctx.ReadValue<Vector2>();
    }

    private void ModalCancel(InputAction.CallbackContext ctx) {

    }

    private void Navigate(InputAction.CallbackContext ctx) {
        Selectable neighbour = CommonService.GetNeighboorSelectable(ctx.ReadValue<Vector2>(), currentSelectedButton);
        if (neighbour) {
            // EventSystem.current.SetSelectedGameObject(startButton.gameObject);
            Button button = neighbour.GetComponent<Button>();
            // SetButtonState(button);
        }
    }

    private void Cancel(InputAction.CallbackContext ctx) {
        MainMenuManagement.instance.MainMenu();
    }

    private void SelectSlot(StartMenuSlot slot) {
        if (slot.IsNewGame()) {
            GameMaster.instance.NewGame(slot.GetSlotNumber());
        } else {
            GameMaster.instance.LoadSave(slot.GetSlotNumber());
        }

    }
    private void DeleteSlot(StartMenuSlot slot) {
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
        }
        currentSlot = null;
        MainMenuManagement.instance.ShowDialogModal(false);
    }


}
