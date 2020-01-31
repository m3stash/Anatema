using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour {

    [SerializeField] private GameObject inventoryLayout;
    [SerializeField] private GameObject defaultLayout;

    private GameObject[] layouts;

    private void Awake() {
        this.layouts = new GameObject[2] {
            this.inventoryLayout,
            this.defaultLayout
        };

        InputManager.OnViewChanged += this.ChangeHUD;
    }

    private void OnDestroy() {
        InputManager.OnViewChanged -= this.ChangeHUD;
    }

    private void ChangeHUD(Layout layout) {
        switch(layout) {
            case Layout.INVENTORY:
                this.DisplayLayout(this.inventoryLayout);
                break;

            case Layout.MENU:
                // To implement
                break;

            case Layout.DEFAULT:
                this.DisplayLayout(this.defaultLayout);
                break;
        }
    }

    private void DisplayLayout(GameObject layoutToDisplay) {
        foreach(GameObject layout in this.layouts) {
            layout.SetActive(layout == layoutToDisplay);
        }
    }
}
