using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StartMenuUI : MonoBehaviour {

    private Vector2 lastMoveDirection;

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
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Navigate.performed += Navigate;
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Cancel.performed += Cancel;
        StartMenuSlot.OnSelect += SelectSlot;
        StartMenuSlot.OnDelete += DeleteSlot;
    }

    private void DisablePerformed() {
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Navigate.performed -= Navigate;
        InputMainMenuManager.instance.mainMenuControls.StartMenu.Cancel.performed -= Cancel;
    }

    private void Navigate(InputAction.CallbackContext ctx) {
        Vector2 direction = ctx.ReadValue<Vector2>();
        if (direction != lastMoveDirection) {
            // Debug.Log(direction.ToString());
            // lastMoveDirection = direction;
        }
    }

    private void Cancel(InputAction.CallbackContext ctx) {
        MainMenuManagement.instance.GoToMainMenu();
    }

    private void SelectSlot(StartMenuSlot slot) {
        if (slot.IsNewGame()) {
            GameMaster.instance.NewGame(slot.GetSlotNumber());
        } else {
            GameMaster.instance.LoadSave(slot.GetSlotNumber());
        }
        
    }
    private void DeleteSlot(StartMenuSlot slot) {
        int slotNumber = slot.GetSlotNumber();
        GameMaster.instance.DeleteSave(slotNumber);
        slot.InitValue();
    }

}
