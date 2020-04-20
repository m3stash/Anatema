using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BuildSelector : MonoBehaviour
{
    [Header("Fields to complete manually")]
    [SerializeField] private GameObject gridCellPrefab;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private GameObject selectorCellPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private bool showGrid;

    [Header("Don't touch it")]
    [SerializeField] private GameObject[] grid;
    [SerializeField] private float cellWidth;
    [SerializeField] private float halfGridWidth;
    [SerializeField] private float halfGridHeight;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject selector;

    [SerializeField] private SpriteRenderer previewItemRenderer;
    [SerializeField] private CellCollider[] cellsToCheck;
    [SerializeField] private bool canPoseItem;

    private Ray ray;
    private int posX;
    private int posY;
    private Vector2 previousMoveDir;

    private int horipadX, horipadY;

    private void Awake() {
        this.selector = Instantiate(this.selectorCellPrefab, this.transform);
        this.selector.SetActive(false);

        this.CreateGrid();
    }

    private void OnEnable() {
        cam = Camera.main;

        InputManager.gameplayControls.TileSelector.PressClick.performed += OnInteractButtonPress;
        InputManager.gameplayControls.TileSelector.ReleaseClick.performed += OnInteractButtonReleased;
        InputManager.gameplayControls.TileSelector.Navigate.performed += OnNavigate;
        InputManager.gameplayControls.TileSelector.HorizontalMove.performed += OnHorizontalMove;
        InputManager.gameplayControls.TileSelector.VerticalMove.performed += OnVerticalMove;
        ToolbarManager.OnSelectedItemChanged += OnCurrentSelectedItemChanged;

        if (Player.instance) { // Todo should be deleted when orchestration where refactored
            player = Player.instance.gameObject;
        }

        this.ManageGridDisplay();

        if (!InputManager.instance.IsMouseEnabled()) {
            this.previousMoveDir = Vector2.zero;
            this.SetTileSelectorPosition(0, 0, true);
        }

        if (GameManager.instance.GetGameMode() == GameMode.BUILD) {
            this.OnCurrentSelectedItemChanged();
        }
    }

    private void OnDisable() {
        InputManager.gameplayControls.TileSelector.PressClick.performed -= OnInteractButtonPress;
        InputManager.gameplayControls.TileSelector.ReleaseClick.performed -= OnInteractButtonReleased;
        InputManager.gameplayControls.TileSelector.Navigate.performed -= OnNavigate;
        InputManager.gameplayControls.TileSelector.HorizontalMove.performed -= OnHorizontalMove;
        InputManager.gameplayControls.TileSelector.VerticalMove.performed -= OnVerticalMove;
        ToolbarManager.OnSelectedItemChanged -= OnCurrentSelectedItemChanged;

        this.DestroyPreviewItem();
    }

    public void SetShowGrid(bool state) {
        this.DestroyPreviewItem();

        this.showGrid = state;
        this.ManageGridDisplay();
    }

    private void OnInteractButtonPress(InputAction.CallbackContext ctx) {
        // TODO do destroy animation on tiles
    }

    private void OnInteractButtonReleased(InputAction.CallbackContext ctx) {
        if (canPoseItem) {
            this.AddItem(new Vector2Int(posX, posY));
        }
    }

    private void OnHorizontalMove(InputAction.CallbackContext ctx) {
        this.horipadX = (int)ctx.ReadValue<float>();

        Vector2 dir = new Vector2(this.horipadX, this.horipadY);

        if (dir != this.previousMoveDir) {
            this.DoNavigation(dir);
        }
    }

    private void OnVerticalMove(InputAction.CallbackContext ctx) {
        this.horipadY = (int)-ctx.ReadValue<float>();

        Vector2 dir = new Vector2(this.horipadX, this.horipadY);

        if (dir != this.previousMoveDir) {
            this.DoNavigation(dir);
        }
    }

    private void OnNavigate(InputAction.CallbackContext ctx) {
        Vector2 dir = ctx.ReadValue<Vector2>();

        if (dir != this.previousMoveDir) {
            this.DoNavigation(dir);
        }
    }

    private void DoNavigation(Vector2 dir) {
        this.previousMoveDir = dir;
        this.SetTileSelectorPosition((int)this.selector.transform.position.x + (int)dir.x, (int)this.selector.transform.position.y + (int)dir.y);
    }

    private void ManageGridDisplay() {
        foreach (GameObject cell in this.grid) {
            cell.SetActive(this.showGrid);
        }
    }

    private void MoveToPlayer() {
        if (player && Vector3.Distance(player.transform.position, transform.position) > 0.5f) {
            transform.position = new Vector3((int)player.transform.position.x + 0.5f, (int)player.transform.position.y + 0.5f);

            if (previewItemRenderer) {
                CheckPreviewItemValidity((int)previewItemRenderer.transform.position.x, (int)previewItemRenderer.transform.position.y);
            }
        }
    }

    private void OnCurrentSelectedItemChanged() {
        this.DestroyPreviewItem();
        this.CreatePreviewItem();
    }

    private void CreateGrid() {
        this.grid = new GameObject[this.gridSize.x * this.gridSize.y];
        int counter = 0;
        this.cellWidth = this.gridCellPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        this.halfGridWidth = ((this.gridSize.x / 2) * this.cellWidth);
        this.halfGridHeight = ((this.gridSize.y / 2) * this.cellWidth);

        for (int x = 0; x < this.gridSize.x; x++) {
            for (int y = 0; y < this.gridSize.y; y++) {
                GameObject gridCell = Instantiate(this.gridCellPrefab,
                    this.transform.position + new Vector3((x * this.cellWidth) - this.halfGridWidth, (y * this.cellWidth) - this.halfGridHeight),
                    Quaternion.identity,
                    this.transform);

                this.grid[counter] = gridCell;
                counter++;
            }
        }
    }

    private void AddItem(Vector2Int pos) {
        InventoryItemData itemData = ToolbarManager.instance.UseSelectedItemData();

        if (itemData == null) {
            return;
        }

        if (itemData.GetConfig().GetItemType().Equals(ItemType.BLOCK)) {
            WorldManager.instance.AddTile(pos.x, pos.y, itemData.GetConfig().GetId());
        } else {
            WorldManager.instance.AddItem(pos, itemData);
        }
    }

    private void CreatePreviewItem() {
        InventoryItemData itemData = ToolbarManager.instance.GetSelectedItemData();

        if (itemData == null) {
            return;
        }

        GameObject obj = Instantiate(itemData.GetConfig().GetPrefab(), this.selector.transform.position + new Vector3(-0.5f, -0.5f), Quaternion.identity);
        obj.transform.parent = this.selector.transform;

        foreach (Component component in obj.GetComponents<Component>()) {
            if (component.GetType() != typeof(SpriteRenderer) && component.GetType() != typeof(Transform) && component.GetType() != typeof(ItemRotation)) {
                Destroy(component);
            }
        }

        this.previewItemRenderer = obj.GetComponent<SpriteRenderer>();
        this.cellsToCheck = itemData.GetConfig().GetColliderConfig().GetCellColliders();
        this.CheckPreviewItemValidity((int)obj.transform.position.x, (int)obj.transform.position.y);
    }

    private void CheckPreviewItemValidity(int originX, int originY) {
        if (!this.previewItemRenderer) {
            this.canPoseItem = false;
            this.RefreshPreviewItemRenderer();
            return;
        }

        bool allIsValid = true;

        // Used to check simple contacts
        int allContacts = 0;
        int validContacts = 0;

        foreach (CellCollider cell in cellsToCheck) {
            // Check if position is free on tileMap and objectMap
            bool objectMapValid = WorldManager.instance.worldMapObject[originX + cell.GetRelativePosition().x, originY + cell.GetRelativePosition().y] == 0;
            bool tilesWorlMapValid = WorldManager.instance.worldMapTile[originX + cell.GetRelativePosition().x, originY + cell.GetRelativePosition().y] == 0;

            if (!objectMapValid || !tilesWorlMapValid) {
                allIsValid = false;
                break;
            }

            bool mandatoriesContactValid = true;

            // Check left cell if a contact has been set
            if (mandatoriesContactValid && cell.GetLeftContactType() != ContactType.NONE) {
                bool leftContactValid = WorldManager.instance.worldMapTile[originX + cell.GetRelativePosition().x - 1, originY + cell.GetRelativePosition().y] > 0;
                allContacts += 1;
                validContacts += leftContactValid ? 1 : 0;

                if (cell.GetLeftContactType() == ContactType.MANDATORY) {
                    mandatoriesContactValid = leftContactValid;
                }
            }

            // Check right cell if a contact has been set
            if (mandatoriesContactValid && cell.GetRightContactType() != ContactType.NONE) {
                bool rightContactValid = WorldManager.instance.worldMapTile[originX + cell.GetRelativePosition().x + 1, originY + cell.GetRelativePosition().y] > 0;
                allContacts += 1;
                validContacts += rightContactValid ? 1 : 0;

                if (cell.GetRightContactType() == ContactType.MANDATORY) {
                    mandatoriesContactValid = rightContactValid;
                }
            }

            // Check top cell if a contact has been set
            if (mandatoriesContactValid && cell.GetTopContactType() != ContactType.NONE) {
                bool topContactValid = WorldManager.instance.worldMapTile[originX + cell.GetRelativePosition().x, originY + cell.GetRelativePosition().y + 1] > 0;
                allContacts += 1;
                validContacts += topContactValid ? 1 : 0;

                if (cell.GetTopContactType() == ContactType.MANDATORY) {
                    mandatoriesContactValid = topContactValid;
                }
            }

            // Check bottom cell if a contact has been set
            if (mandatoriesContactValid && cell.GetBottomContactType() != ContactType.NONE) {
                bool bottomContactValid = WorldManager.instance.worldMapTile[originX + cell.GetRelativePosition().x, originY + cell.GetRelativePosition().y - 1] > 0;
                allContacts += 1;
                validContacts += bottomContactValid ? 1 : 0;

                if (cell.GetBottomContactType() == ContactType.MANDATORY) {
                    mandatoriesContactValid = bottomContactValid;
                }
            }

            // If a mandatory contact set, it need to be valid !
            if (!mandatoriesContactValid) {
                allIsValid = false;
                break;
            }
        }

        // If all checks are valid and simple contacts are valids
        this.canPoseItem = allIsValid && ((allContacts > 0 && validContacts > 0) || allContacts == validContacts);
        this.RefreshPreviewItemRenderer();
    }

    private void RefreshPreviewItemRenderer() {
        if (this.previewItemRenderer) {
            this.previewItemRenderer.color = this.canPoseItem ? new Color(1, 1, 1, 0.5f) : new Color(1, 0, 0, 0.5f);
        }
    }

    private void DestroyPreviewItem() {
        if (this.previewItemRenderer) {
            Destroy(this.previewItemRenderer.gameObject);
        }
    }

    private void SetTileSelectorPosition(int x, int y, bool force = false) {
        if (!this.selector.activeSelf) {
            this.selector.SetActive(true);
        }

        if (force) {
            this.selector.SetActive(true);
            this.selector.transform.localPosition = new Vector2(x, y);
            this.CheckPreviewItemValidity(x, y);
            return;
        }

        if (this.selector.transform.position != new Vector3(x + 0.5f, y + 0.5f)) {
            if (x <= (int)player.transform.position.x + 0.5f + this.halfGridWidth &&
                x >= (int)player.transform.position.x - 0.5f - this.halfGridWidth &&
                y >= (int)player.transform.position.y - 0.5f - this.halfGridHeight &&
                y <= (int)player.transform.position.y + 0.5f + this.halfGridHeight) {

                this.selector.transform.position = new Vector2(x + 0.5f, y + 0.5f);

                if (this.previewItemRenderer && this.previewItemRenderer.GetComponent<ItemRotation>()) {
                    this.previewItemRenderer.GetComponent<ItemRotation>().RefreshUI();
                }

                this.CheckPreviewItemValidity(x, y);
            } else {
                this.canPoseItem = false;
                this.selector.SetActive(false);
            }
        }
    }


    private void Update() {
        this.MoveToPlayer();

        // Manage selector tile for mouse
        if (InputManager.instance.IsMouseEnabled()) {
            ray = cam.ScreenPointToRay(InputManager.mousePosition);

            posX = (int)ray.origin.x;
            posY = (int)ray.origin.y;
            this.SetTileSelectorPosition(posX, posY);
        } else {
            posX = (int)this.selector.transform.position.x;
            posY = (int)this.selector.transform.position.y;
        }
    }
}