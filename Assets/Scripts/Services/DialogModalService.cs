using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogModalConf {
    public string title;
    public string message;
    public Action<bool> closeCallbackAction;

    public DialogModalConf(string title, string message, Action<bool> action) {
        this.title = title;
        this.message = message;
        closeCallbackAction = action;
    }
}

public class DialogModalService : MonoBehaviour {

    public static DialogModalService instance;
    private GameObject dialogModalGo;
    private DialogModal dialogModal;
    public delegate void OnClose(bool status);
    public static OnClose closeModalDelegate;
    private Action<bool> callbackAction;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        closeModalDelegate += OnModalClose;
    }

    public void Open(DialogModalConf conf) {
        callbackAction = conf.closeCallbackAction;
        Create(conf);
    }

    public void OnModalClose(bool status) {
        callbackAction?.Invoke(status);
        Destroy(dialogModalGo);
    }

    private void Create(DialogModalConf conf) {
        dialogModalGo = Instantiate((GameObject)Resources.Load("Prefabs/UI/Common/CanvasDialogModale"));
        dialogModal = dialogModalGo.GetComponent<DialogModal>();
        dialogModal.SetMessage(conf);
    }

    private void OnDestroy() {
        closeModalDelegate -= OnModalClose;
        callbackAction?.Invoke(false);
        Destroy(dialogModalGo);
    }
}
