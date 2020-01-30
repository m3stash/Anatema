﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DefaultLayoutUI : MonoBehaviour {

    [Header("Fields need to be completed manually")]
    [SerializeField] private ToolbarUI toolbar;

    private void OnEnable() {
        InputManager.gameplayControls.Toolbar.Navigate.performed += this.ManageMouseScroll;
        GameManager.OnGameModeChanged += GameModeChanged;

        this.toolbar.UpdateCellState();
    }

    private void OnDisable() {
        InputManager.gameplayControls.Toolbar.Navigate.performed -= this.ManageMouseScroll;
        GameManager.OnGameModeChanged -= GameModeChanged;
    }

    private void ManageMouseScroll(InputAction.CallbackContext ctx) {
        float value = ctx.ReadValue<float>();

        if (value > 0f) {
            this.toolbar.SelectNextSlot();
        } else if(value < 0f) {
            this.toolbar.SelectPreviousSlot();
        }
    }

    private void GameModeChanged(GameMode gameMode) {
        this.toolbar.gameObject.SetActive(gameMode == GameMode.BUILD);
    }
}