using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogModal : MonoBehaviour {

    [SerializeField] private Button cancel;
    [SerializeField] private Button validate;
    [SerializeField] private Text text;

    private void EnablePerformed() {
        // InputMainMenuManager.instance.mainMenuControls.StartMenu.Navigate.performed += Navigate;
        // InputMainMenuManager.instance.mainMenuControls.StartMenu.Cancel.performed += Cancel;
    }

    private void DisablePerformed() {
        cancel.onClick.RemoveAllListeners();
        validate.onClick.RemoveAllListeners();
        // InputMainMenuManager.instance.mainMenuControls.StartMenu.Navigate.performed -= Navigate;
        // InputMainMenuManager.instance.mainMenuControls.StartMenu.Cancel.performed -= Cancel;
    }


    private void Awake() {

        cancel = cancel.GetComponent<Button>();
        validate = validate.GetComponent<Button>();

        EnablePerformed();
        cancel.onClick.AddListener(() => {
            DialogModalService.closeModalDelegate?.Invoke(false);
        });
        validate.onClick.AddListener(() => {
            DialogModalService.closeModalDelegate?.Invoke(true);
        });

    }
    
    public void SetMessage(DialogModalConf conf) {
        text.text = conf.message;
    }

    private void OnDestroy() {
        DisablePerformed();
    }

    private void Navigate(InputAction.CallbackContext ctx) {
        Vector2 direction = ctx.ReadValue<Vector2>();
        /*if (direction != lastMoveDirection) {
            Debug.Log(direction.ToString());
            lastMoveDirection = direction;
        }*/
    }

    private void Valid(InputAction.CallbackContext ctx) {

    }

    private void Cancel(InputAction.CallbackContext ctx) {

    }

}
