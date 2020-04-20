using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogModal : MonoBehaviour {

    [SerializeField] private Button cancel;
    [SerializeField] private Button validate;
    [SerializeField] private Text text;
    private Button currentSelectedButton;
    private void Awake() {

        cancel = cancel.GetComponent<Button>();
        SetButtonColors(cancel);
        validate = validate.GetComponent<Button>();
        SetButtonColors(validate);

        cancel.onClick.AddListener(() => {
            DialogModalService.closeModalDelegate?.Invoke(false);
        });

        validate.onClick.AddListener(() => {
            DialogModalService.closeModalDelegate?.Invoke(true);
        });

    }

    private void OnEnable() {
        EnablePerformed();
    }

    private void Start() {
        SetButtonState(validate);
    }

    private void EnablePerformed() {
        InputMainMenuManager.instance.mainMenuControls.DialogModal.Navigate.performed += Navigate;
        InputMainMenuManager.instance.mainMenuControls.DialogModal.Select.performed += Select;
        InputMainMenuManager.instance.mainMenuControls.DialogModal.Cancel.performed += Cancel;
    }

    private void DisablePerformed() {
        cancel.onClick.RemoveAllListeners();
        validate.onClick.RemoveAllListeners();
        InputMainMenuManager.instance.mainMenuControls.DialogModal.Navigate.performed -= Navigate;
        InputMainMenuManager.instance.mainMenuControls.DialogModal.Select.performed -= Select;
        InputMainMenuManager.instance.mainMenuControls.DialogModal.Cancel.performed -= Cancel;
    }
    private void Select(InputAction.CallbackContext ctx) {
        currentSelectedButton.onClick.Invoke();
    }

    private void Cancel(InputAction.CallbackContext ctx) {
        cancel.onClick.Invoke();
    }

    private void SetButtonColors(Button button) {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = new Color(1, 1, 1, 1);
        colorBlock.selectedColor = new Color(0.69f, 0.69f, 0.69f, 0.69f);
        colorBlock.highlightedColor = new Color(0.69f, 0.69f, 0.69f, 0.69f);
        button.colors = colorBlock;
    }

    private void SetButtonState(Button button) {
        button.Select();
        currentSelectedButton = button;
    }

    public void SetMessage(DialogModalConf conf) {
        text.text = conf.message;
    }

    private void OnDestroy() {
        DisablePerformed();
    }

    private void Navigate(InputAction.CallbackContext ctx) {
        Selectable neighbour = CommonService.GetNeighboorSelectable(ctx.ReadValue<Vector2>(), currentSelectedButton);
        if (neighbour) {
            Button button = neighbour.GetComponent<Button>();
            SetButtonState(button);
        }
    }

}
