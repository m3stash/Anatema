using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolSelector : MonoBehaviour
{
    [Header("Fields to complete")]
    [SerializeField] private DigSelector digSelectorPrefab;
    [SerializeField] private GameObject digParticle;

    [Header("Don't touch it")]
    [SerializeField] private ToolType currentToolType = ToolType.PICKAXE;
    [SerializeField] private Item currentHoveredItem;
    [SerializeField] private Camera cam;
    [SerializeField] private DigSelector digSelector;

    private Ray ray;
    private int posX;
    private int posY;

    private Vector2Int selectedTilePosition;
    private bool selectedTile;
    private float currentTileDurability;
    private BlockConfig currentTileConfig;

    private Vector3 hitPoint;

    private float digTime; // Represent the time since button pressed to dig
    private bool isDiging;

    private Vector3 lookDirection;

    private void OnEnable() {
        cam = Camera.main;

        if (InputManager.gameplayControls != null) { // TODO remove that after orchestrator
            InputManager.gameplayControls.ToolSelector.PressClick.performed += OnInteractButtonPress;
            InputManager.gameplayControls.ToolSelector.ReleaseClick.performed += OnInteractButtonReleased;
            InputManager.gameplayControls.ToolSelector.LookDirection.performed += OnLookDirection;
            InputManager.gameplayControls.Core.Position.performed += OnMousePositionChanged;
        }

        this.digSelector = Instantiate(this.digSelectorPrefab);
        this.digSelector.gameObject.SetActive(false);
    }

    private void OnDisable() {
        if (InputManager.gameplayControls != null) { // TODO remove that after orchestrator
            InputManager.gameplayControls.ToolSelector.PressClick.performed -= OnInteractButtonPress;
            InputManager.gameplayControls.ToolSelector.ReleaseClick.performed -= OnInteractButtonReleased;
            InputManager.gameplayControls.ToolSelector.LookDirection.performed -= OnLookDirection;
            InputManager.gameplayControls.Core.Position.performed -= OnMousePositionChanged;
        }

        if (this.digSelector) {
            Destroy(this.digSelector.gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        if (this.currentHoveredItem) {
            this.currentHoveredItem.DisplayOutlineShader(false);
            this.currentHoveredItem = null;
        }

        // Don't do anymore if there isn't player
        if (!Player.instance) {
            return;
        }

        if (this.isDiging) {
            if (this.selectedTile) { // Always check it in case of player move without change its look direction
                this.digTime += Time.deltaTime;

                if (this.digTime >= 0.2f) { // Todo replace with tool speed
                    this.digTime = 0f;
                    this.currentTileDurability += 25f; // Todo replace with tool power

                    if (this.currentTileDurability >= this.currentTileConfig.GetDurability()) {
                        this.digSelector.ResetSetup();
                        WorldManager.instance.DeleteTile(this.selectedTilePosition.x, this.selectedTilePosition.y);
                    } else {
                        this.digSelector.Setup(this.currentTileConfig.GetDurability(), this.currentTileDurability);
                        Instantiate(this.digParticle, this.hitPoint + (-this.lookDirection.normalized * 0.2f), Quaternion.LookRotation(-this.lookDirection, Vector3.up));
                    }
                }
            } else {
                this.digTime = 0f;
            }

        }

        Debug.DrawRay(Player.instance.transform.position + new Vector3(0, 1f), this.lookDirection.normalized * 2f, Color.red);

        if (this.currentToolType == ToolType.PICKAXE) {
            this.ManagePickaxeSystem();
        } else if (this.currentToolType == ToolType.AXE) {
            this.ManageAxeSystem();
        } else if (this.currentToolType == ToolType.HAMMER) {
            this.ManageHammerSystem();
        }
    }

    private void ManagePickaxeSystem() {
        int layers = (1 << 10); // Only tiles
        RaycastHit2D hit = Physics2D.Raycast(Player.instance.transform.position + new Vector3(0, 1f), this.lookDirection.normalized, 2f, layers);

        if (hit.collider) {
            this.hitPoint = hit.point;

            int x = 0;

            if (this.lookDirection.x > 0) {
                x = Mathf.FloorToInt(hit.point.x + 0.01f);
            } else if (this.lookDirection.x < 0) {
                x = Mathf.FloorToInt(hit.point.x - 0.01f);
            } else {
                x = Mathf.FloorToInt(hit.point.x);
            }

            int y = 0;

            if (this.lookDirection.y > 0) {
                y = Mathf.FloorToInt(hit.point.y + 0.01f);
            } else if (this.lookDirection.y < 0) {
                y = Mathf.FloorToInt(hit.point.y - 0.01f);
            } else {
                y = Mathf.FloorToInt(hit.point.y);
            }

            if (WorldManager.tilesWorldMap[x, y] > 0) {
                this.digSelector.transform.position = new Vector3(x + 0.5f, y + 0.5f);
                this.digSelector.gameObject.SetActive(true);

                if (this.selectedTilePosition != new Vector2Int(x, y)) {
                    this.digTime = 0f;
                    this.currentTileDurability = 0;
                    this.currentTileConfig = (BlockConfig)ItemManager.instance.GetItemWithId(WorldManager.tilesWorldMap[x, y]);
                }

                this.selectedTile = true;
                this.selectedTilePosition = new Vector2Int(x, y);
            } else {
                this.digSelector.gameObject.SetActive(false);
            }
        } else {
            this.selectedTile = false;
            this.digSelector.gameObject.SetActive(false);
        }
    }

    private void ManageAxeSystem() {

    }

    private void ManageHammerSystem() {
        Vector3 dir = Vector3.zero;

        if (InputManager.instance.IsMouseEnabled()) { // For mouse
            ray = cam.ScreenPointToRay(InputManager.mousePosition);

            dir = new Vector3(ray.origin.x - Player.instance.transform.position.x, ray.origin.y - Player.instance.transform.position.y);

        }

        int layers = (1 << 14); // Only items
        RaycastHit2D hit = Physics2D.Raycast(Player.instance.transform.position, dir.normalized, 2f, layers);

        Debug.DrawLine(Player.instance.transform.position, new Vector3(ray.origin.x, ray.origin.y), Color.white);
        Debug.DrawRay(Player.instance.transform.position, dir.normalized * 2f, Color.red);

        if (hit.collider) {
            Item item = hit.collider.GetComponentInChildren<Item>();

            if (item.GetStatus() == ItemStatus.ACTIVE) {
                this.currentHoveredItem = item;
                this.currentHoveredItem.DisplayOutlineShader(true);
            }
        }
    }

    private void OnInteractButtonPress(InputAction.CallbackContext ctx) {
        if (this.currentToolType == ToolType.PICKAXE) {
            this.isDiging = true;
        }
    }

    private void OnInteractButtonReleased(InputAction.CallbackContext ctx) {
        if (this.currentToolType == ToolType.PICKAXE) {
            this.isDiging = false;
            this.digTime = 0f;
            this.digSelector.ResetSetup();
        } else if (this.currentToolType == ToolType.AXE) {
        } else if (this.currentToolType == ToolType.HAMMER) {
            if (this.currentHoveredItem) {
                WorldManager.instance.DeleteItem(this.currentHoveredItem);
                this.currentHoveredItem = null;
            }
        }
    }

    private void OnLookDirection(InputAction.CallbackContext ctx) {
        Vector2 dir = ctx.ReadValue<Vector2>();

        if (dir != Vector2.zero) {
            this.lookDirection = ctx.ReadValue<Vector2>();
        }
    }

    private void OnMousePositionChanged(InputAction.CallbackContext ctx) {
        ray = cam.ScreenPointToRay(InputManager.mousePosition);

        this.lookDirection = new Vector3(ray.origin.x - Player.instance.transform.position.x, ray.origin.y - (Player.instance.transform.position.y + 1f));
    }
}

