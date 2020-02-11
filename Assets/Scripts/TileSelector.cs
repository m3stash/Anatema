using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelector : MonoBehaviour {
    [Header("Fields to complete manually")]
    [SerializeField] private GameObject gridCellPrefab;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private GameObject selectorCellPrefab;
    [SerializeField] private GameObject target;
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
    private bool onClick = false;

    private void Awake() {
        this.selector = Instantiate(this.selectorCellPrefab, this.transform);
        this.selector.SetActive(false);

        this.CreateGrid();
    }

    private void OnEnable() {
        cam = Player.instance.GetComponentInChildren<Camera>();

        InputManager.gameplayControls.TileSelector.PressClick.performed += ctx => this.SetOnClick(true);
        InputManager.gameplayControls.TileSelector.ReleaseClick.performed += ctx => this.SetOnClick(false);
        ToolbarManager.OnSelectedItemChanged += OnCurrentSelectedItemChanged;

        this.target = Player.instance.gameObject;

        this.ManageGridDisplay();
    }

    private void OnDisable() {
        InputManager.gameplayControls.TileSelector.PressClick.performed -= ctx => this.SetOnClick(true);
        InputManager.gameplayControls.TileSelector.ReleaseClick.performed -= ctx => this.SetOnClick(false);
        ToolbarManager.OnSelectedItemChanged -= OnCurrentSelectedItemChanged;

        this.DestroyPreviewItem();
    }

    public void SetShowGrid(bool state) {
        this.showGrid = state;
        this.ManageGridDisplay();
    }

    private void SetOnClick(bool value) {
        this.onClick = value;
    }

    private void ManageGridDisplay() {
        foreach(GameObject cell in this.grid) {
            cell.SetActive(this.showGrid);
        }
    }

    private void MoveToTarget() {
        if(this.target) {
            this.transform.position = new Vector3((int)this.target.transform.position.x + 0.5f, (int)this.target.transform.position.y + 0.5f);
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

        for(int x = 0; x < this.gridSize.x; x++) {
            for(int y = 0; y < this.gridSize.y; y++) {
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

        if(itemData == null) {
            return;
        }

        if(itemData.GetConfig().GetItemType().Equals(ItemType.BLOCK)) {
            WorldManager.instance.AddTile(pos.x, pos.y, itemData.GetConfig().GetId());
        } else {
            WorldManager.instance.AddItem(pos, itemData);
        }

        onClick = false;
    }

    private void CreatePreviewItem() {
        InventoryItemData itemData = ToolbarManager.instance.GetSelectedItemData();

        if(itemData == null) {
            return;
        }

        GameObject obj = Instantiate(itemData.GetConfig().GetPrefab(), this.selector.transform.position, Quaternion.identity);
        obj.transform.parent = this.selector.transform;

        this.previewItemRenderer = obj.GetComponent<SpriteRenderer>();
        this.cellsToCheck = itemData.GetConfig().GetColliderConfig().GetCellColliders();
        this.CheckPreviewItemValidity((int)this.transform.position.x, (int)this.transform.position.y);
    }

    private void CheckPreviewItemValidity(int originX, int originY) {
        if(!this.previewItemRenderer) {
            this.canPoseItem = false;
            this.RefreshPreviewItemRenderer();
            return;
        }

        bool allIsValid = true;

        foreach(CellCollider cell in cellsToCheck) {
            // Check if position is free on tileMap and objectMap
            bool objectMapValid = WorldManager.objectsMap[originX + cell.GetRelativePosition().x, originY + cell.GetRelativePosition().y] == 0;
            bool tilesWorlMapValid = WorldManager.tilesWorldMap[originX + cell.GetRelativePosition().x, originY + cell.GetRelativePosition().y] == 0;

            // TODO: Check neighbour constraint to avoid fly item

            if(!objectMapValid || !tilesWorlMapValid) {
                allIsValid = false;
                break;
            }
        }

        this.canPoseItem = allIsValid;
        this.RefreshPreviewItemRenderer();
    }

    private void RefreshPreviewItemRenderer() {
        if(this.previewItemRenderer) {
            this.previewItemRenderer.color = this.canPoseItem ? new Color(1, 1, 1, 0.5f) : new Color(1, 0, 0, 0.5f);
        }
    }

    private void DestroyPreviewItem() {
        if(this.previewItemRenderer) {
            Destroy(this.previewItemRenderer.gameObject);
        }
    }


    private void Update() {
        this.MoveToTarget();

        ray = cam.ScreenPointToRay(InputManager.mousePosition);
        int posX = (int)ray.origin.x;
        int posY = (int)ray.origin.y;

        // Manage selector tile
        if(posX <= (int)this.target.transform.position.x + 0.5f + this.halfGridWidth &&
            posX >= (int)this.target.transform.position.x - 0.5f - this.halfGridWidth &&
            posY >= (int)this.target.transform.position.y - 0.5f - this.halfGridHeight &&
            posY <= (int)this.target.transform.position.y + 0.5f + this.halfGridHeight) {

            if(!this.selector.activeSelf) {
                this.selector.SetActive(true);
            }

            if(this.selector.transform.position != new Vector3(posX + 0.5f, posY + 0.5f)) {
                this.selector.transform.position = new Vector2(posX + 0.5f, posY + 0.5f);
                this.CheckPreviewItemValidity(posX, posY);
            }
        } else if(this.selector.activeSelf) {
            this.selector.SetActive(false);
        }

        // Perform action on click
        if(onClick) {
            switch(GameManager.instance.GetGameMode()) {
                case GameMode.BUILD:
                    if(canPoseItem) {
                        this.AddItem(new Vector2Int(posX, posY));
                    }
                    break;
                case GameMode.TOOL:
                    if(WorldManager.tilesWorldMap[posX, posY] > 0) {
                        if(WorldManager.objectsMap[posX, posY] > 0) {
                            WorldManager.instance.DeleteItem(posX, posY);
                        } else {
                            WorldManager.instance.DeleteTile((int)posX, (int)posY);
                        }
                    }
                    break;
            }
        }
    }
}