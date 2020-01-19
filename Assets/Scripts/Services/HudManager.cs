using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryLayout;
    [SerializeField] private GameObject defaultLayout;

    private GameObject[] layouts;

    private void Awake() {
        this.layouts = new GameObject[2] {
            this.inventoryLayout,
            this.defaultLayout
        };

        this.DisplayLayout(this.defaultLayout);
    }

    // Start is called before the first frame update
    private void OnEnable() {
        InputManager.OnViewChanged += this.ChangeHUD;
    }

    private void OnDisable() {
        InputManager.OnViewChanged -= this.ChangeHUD;
    }

    private void ChangeHUD(View view) {
        switch (view) {
            case View.INVENTORY:
                this.DisplayLayout(this.inventoryLayout);
                break;

            case View.CRAFT:
                break;

            case View.MENU:
                break;

            case View.DEFAULT:
                this.DisplayLayout(this.defaultLayout);
                break;
        }
    }

    private void DisplayLayout(GameObject layoutToDisplay) {
        foreach (GameObject layout in this.layouts) {
            layout.SetActive(layout == layoutToDisplay);
        }
    }
}
