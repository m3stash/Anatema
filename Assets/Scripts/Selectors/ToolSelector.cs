using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolSelector : MonoBehaviour {
    [Header("Fields to complete")]
    [SerializeField] private GameObject selectorPrefab;

    [Header("Don't touch it")]
    [SerializeField] private ToolType currentToolType = ToolType.PICKAXE;
    [SerializeField] private Item currentHoveredItem;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject selector;

    private Ray ray;
    private int posX;
    private int posY;

    private void OnEnable() {
        cam = Camera.main;

        if(InputManager.gameplayControls != null) { // TODO remove that after orchestrator
            InputManager.gameplayControls.ToolSelector.PressClick.performed += OnInteractButtonPress;
            InputManager.gameplayControls.ToolSelector.ReleaseClick.performed += OnInteractButtonReleased;
        }

        this.selector = Instantiate(this.selectorPrefab);
        this.selector.SetActive(false);
    }

    private void OnDisable() {
        if(InputManager.gameplayControls != null) { // TODO remove that after orchestrator
            InputManager.gameplayControls.ToolSelector.PressClick.performed -= OnInteractButtonPress;
            InputManager.gameplayControls.ToolSelector.ReleaseClick.performed -= OnInteractButtonReleased;
        }

        Destroy(this.selector);
    }

    // Update is called once per frame
    void Update() {
        if(this.currentHoveredItem) {
            this.currentHoveredItem.DisplayOutlineShader(false);
            this.currentHoveredItem = null;
        }

        // Don't do anymore if there isn't player
        if(!Player.instance) {
            return;
        }

        if(this.currentToolType == ToolType.PICKAXE) {
            this.ManagePickaxeSystem();
        } else if(this.currentToolType == ToolType.AXE) {
            this.ManageAxeSystem();
        } else if(this.currentToolType == ToolType.HAMMER) {
            this.ManageHammerSystem();
        }
    }

    private void ManagePickaxeSystem() {
        Vector3 dir = Vector3.zero;

        if(InputManager.instance.IsMouseEnabled()) { // For mouse
            ray = cam.ScreenPointToRay(InputManager.mousePosition);

            dir = new Vector3(ray.origin.x - Player.instance.transform.position.x, ray.origin.y - Player.instance.transform.position.y + 1f);

        } else { // For controllers
            //dir = 
        }

        int layers = (1 << 10); // Only tiles
        RaycastHit2D hit = Physics2D.Raycast(Player.instance.transform.position, dir.normalized, 2f, layers);

        Debug.DrawLine(Player.instance.transform.position, new Vector3(ray.origin.x, ray.origin.y), Color.white);
        Debug.DrawRay(Player.instance.transform.position, dir.normalized * 2f, Color.red);

        if(hit.collider) {
            this.selector.SetActive(true);

            int x = 0;

            if(dir.x > 0) {
                x = Mathf.FloorToInt(hit.point.x + 0.05f);
            } else if(dir.x < 0) {
                x = Mathf.FloorToInt(hit.point.x - 0.05f);
            } else {
                x = Mathf.FloorToInt(hit.point.x);
            }

            int y = 0;

            if(dir.y > 0) {
                y = Mathf.FloorToInt(hit.point.y + 0.05f);
            } else if(dir.y < 0) {
                y = Mathf.FloorToInt(hit.point.y - 0.05f);
            } else {
                y = Mathf.FloorToInt(hit.point.y);
            }

            this.selector.transform.position = new Vector3(x + 0.5f, y + 0.5f);

        } else {
            this.selector.SetActive(false);
        }
    }

    private void ManageAxeSystem() {

    }

    private void ManageHammerSystem() {
        Vector3 dir = Vector3.zero;

        if(InputManager.instance.IsMouseEnabled()) { // For mouse
            ray = cam.ScreenPointToRay(InputManager.mousePosition);

            dir = new Vector3(ray.origin.x - Player.instance.transform.position.x, ray.origin.y - Player.instance.transform.position.y);

        } else { // For controllers
            //dir = 
        }

        int layers = (1 << 14); // Only items
        RaycastHit2D hit = Physics2D.Raycast(Player.instance.transform.position, dir.normalized, 2f, layers);

        Debug.DrawLine(Player.instance.transform.position, new Vector3(ray.origin.x, ray.origin.y), Color.white);
        Debug.DrawRay(Player.instance.transform.position, dir.normalized * 2f, Color.red);

        if(hit.collider) {
            Item item = hit.collider.GetComponentInChildren<Item>();

            if(item.GetStatus() == ItemStatus.ACTIVE) {
                this.currentHoveredItem = item;
                this.currentHoveredItem.DisplayOutlineShader(true);
            }
        }
    }

    private void OnInteractButtonPress(InputAction.CallbackContext ctx) {
        // TODO do destroy animation on tiles
    }

    private void OnInteractButtonReleased(InputAction.CallbackContext ctx) {
        if(this.currentToolType == ToolType.PICKAXE) {
            this.ManagePickaxeSystem();
        } else if(this.currentToolType == ToolType.AXE) {
            this.ManageAxeSystem();
        } else if(this.currentToolType == ToolType.HAMMER) {
            if(this.currentHoveredItem) {
                WorldManager.instance.DeleteItem(this.currentHoveredItem);
                this.currentHoveredItem = null;
            }
        }
    }
}

