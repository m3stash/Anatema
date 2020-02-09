using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [Header("Fields need to be completed manually")]
    [SerializeField] private CursorUI cursorUI;
    [SerializeField] private CursorConfig[] configs;

    [Header("Don't touch it")]
    [SerializeField] private CursorConfig currentConfig;
    [SerializeField] private CursorState currentState;

    public static CursorManager instance;

    private void Awake() {
        instance = this;

        this.cursorUI.gameObject.SetActive(true);
    }

    public void SetCursorState(CursorState state) {
        this.currentState = state;

        if (state == CursorState.DISABLED) {
            Cursor.visible = false;
            this.cursorUI.Display(false);
            return;
        }

        this.currentConfig = this.GetConfig(state);

        if (this.currentConfig) {
            if (InputManager.instance.IsMouseEnabled()) {
                Cursor.visible = true;

                this.cursorUI.Display(false);
                Cursor.SetCursor(this.currentConfig.GetMouseTexture(), Vector2.zero, CursorMode.Auto);
            } else {
                Cursor.visible = false;

                this.cursorUI.Display(true);
                this.cursorUI.SetSprite(this.currentConfig.GetControllerSprite());
            }
        }
    }

    public void SetPosition(Vector2 pos) {
        this.cursorUI.SetPosition(pos);
    }

    private CursorConfig GetConfig(CursorState state) {
        foreach (CursorConfig config in this.configs) {
            if (config.GetCursorState().Equals(state)) {
                return config;
            }
        }

        Debug.LogErrorFormat("No cursor config for cursor state : {0}", state.ToString());

        return null;
    }
}
