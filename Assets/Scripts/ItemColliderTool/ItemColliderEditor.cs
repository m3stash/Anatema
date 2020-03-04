using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEditor;
using TMPro;
using System;

public class ItemColliderEditor : MonoBehaviour {
    [Header("Fields to complete manually")]
    [SerializeField] private GameObject gridCellPrefab;
    [SerializeField] private ItemConfigButton itemConfigButtonPrefab;
    [SerializeField] private GameObject itemScrollViewContent;
    [SerializeField] private BlockTypeButton blockTypeButtonPrefab;
    [SerializeField] private GameObject blockTypeScrollViewContent;
    [SerializeField] private TMP_InputField assetNameInputField;

    [Header("Modified in runtime")]
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private ItemColliderCell[] grid;
    [SerializeField] private float cellWidth;
    [SerializeField] private float halfGridWidth;
    [SerializeField] private float halfGridHeight;
    [SerializeField] private ItemConfig currentItemConfig;
    [SerializeField] private GameObject currentItemLoaded;
    [SerializeField] private ItemColliderCell cellOrigin;
    [SerializeField] private string assetName;
    [SerializeField] private new Camera camera;

    private ItemColliderControls controls;
    private Vector2 mousePosition;

    private void Awake() {
        this.camera = FindObjectOfType<Camera>();
    }

    // Start is called before the first frame update
    void Start() {
        this.controls = new ItemColliderControls();
        this.controls.Enable();

        this.controls.ItemColliderTool.Click.performed += Click;
        this.controls.ItemColliderTool.MousePosition.performed += SetMousePosition;

        this.InitItemConfigList();
        this.InitBlockTypeConfigList();

        this.cellWidth = this.gridCellPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        ItemConfigButton.OnSelect += LoadItem;
    }

#if UNITY_EDITOR

    /// <summary>
    /// Save asset to scriptable object
    /// </summary>
    public void SaveAsset() {
        CellCollider[] selectedCells = this.grid
            .Where((ItemColliderCell cell) => cell.IsSelected() || cell.IsOrigin())
            .Select(elem => new CellCollider(elem))
            .ToArray();

        BlockType[] unAllowedBlockTypes = GameObject.FindObjectsOfType<BlockTypeButton>()
            .Where((BlockTypeButton button) => !button.IsSelected())
            .Select(elem => elem.GetBlockType())
            .ToArray();

        ItemColliderConfig config = ScriptableObject.CreateInstance<ItemColliderConfig>();
        config.Setup(selectedCells, unAllowedBlockTypes);

        AssetDatabase.CreateAsset(config, "Assets/Resources/Scriptables/Items/ColliderConfigs/" + this.assetName + ".asset");

        Debug.Log("Saved to : " + AssetDatabase.GetAssetPath(config));
    }

#endif

    /// <summary>
    /// Action performed when text value changed in input field
    /// </summary>
    /// <param name="name"></param>
    public void SetAssetName(string name) {
        this.assetName = name;
    }

    /// <summary>
    /// Used to predefined an asset name after item loaded
    /// </summary>
    private void InitAssetName() {
        this.assetName = this.currentItemConfig.GetId() + "_" + this.currentItemConfig.GetDisplayName();
        this.assetNameInputField.SetTextWithoutNotify(this.assetName);
    }

    private void OnDestroy() {
        ItemConfigButton.OnSelect -= LoadItem;
        this.controls.ItemColliderTool.Click.performed -= Click;
        this.controls.ItemColliderTool.MousePosition.performed -= SetMousePosition;
    }

    /// <summary>
    /// Display all items config in scroll view
    /// </summary>
    private void InitItemConfigList() {
        ItemConfig[] items = Resources.LoadAll<ItemConfig>("Scriptables/Items");

        items = items.Where(elem => elem.IsPlaceable()).OrderBy(elem => elem.GetId()).ToArray();

        foreach(ItemConfig config in items) {
            ItemConfigButton current = Instantiate(this.itemConfigButtonPrefab, this.itemScrollViewContent.transform);
            current.Setup(config);
        }
    }

    private void InitBlockTypeConfigList() {
        foreach(BlockType blockType in (BlockType[])Enum.GetValues(typeof(BlockType))) {
            BlockTypeButton current = Instantiate(this.blockTypeButtonPrefab, this.blockTypeScrollViewContent.transform);
            current.Setup(blockType);
        }
    }

    /// <summary>
    /// Action performed when mouse clicked
    /// Set cell state
    /// </summary>
    /// <param name="ctx"></param>
    private void Click(InputAction.CallbackContext ctx) {
        // Need to catch all cause of collider surounding in some cases
        RaycastHit2D[] hits = Physics2D.RaycastAll(this.camera.ScreenToWorldPoint(this.mousePosition), Vector2.zero, 10, (1 << 25));

        if(hits.Length > 0) {
            CellContactPoint contactPoint = hits.Where(elem => elem.collider.GetComponent<CellContactPoint>()).Select(elem => elem.collider.GetComponent<CellContactPoint>()).FirstOrDefault();

            if(contactPoint) {
                contactPoint.Select();
                return;
            }

            ItemColliderCell cell = hits.Where(elem => elem.collider.GetComponent<ItemColliderCell>()).Select(elem => elem.collider.GetComponent<ItemColliderCell>()).First();

            if(cell) {
                cell.Select();
                return;
            }
        }
    }

    /// <summary>
    /// Action performed when mouse moved
    /// </summary>
    /// <param name="ctx"></param>
    private void SetMousePosition(InputAction.CallbackContext ctx) {
        this.mousePosition = ctx.ReadValue<Vector2>();
    }

    /// <summary>
    /// Used when item clicked on scroll view to init into the grid
    /// </summary>
    /// <param name="config"></param>
    private void LoadItem(ItemConfig config) {
        this.currentItemConfig = config;

        // Reset all grid + preview item
        this.Clear();

        // Create new item and remove all useless components
        this.currentItemLoaded = Instantiate(config.GetPrefab(), this.transform.position - new Vector3(0.5f, 0.5f), Quaternion.identity, this.transform);
        this.RemoveAllUselessComponents(this.currentItemLoaded);

        // Set asset name by default
        this.InitAssetName();

        // Set variables to construct grid and keep data for other uses
        SpriteRenderer spriteRenderer = this.currentItemLoaded.GetComponent<SpriteRenderer>();
        Vector2 pivot = this.GetSpritePivot(spriteRenderer.sprite);

        this.gridSize.x = Mathf.CeilToInt(spriteRenderer.bounds.size.x / this.cellWidth);
        this.gridSize.y = Mathf.CeilToInt(spriteRenderer.bounds.size.x / this.cellWidth);
        this.halfGridWidth = ((this.gridSize.x * pivot.x) * this.cellWidth);
        this.halfGridHeight = ((this.gridSize.y * pivot.y) * this.cellWidth);

        this.CreateGrid();

        this.SetOriginCell();

        this.MoveCameraToCenter();
    }

    /// <summary>
    /// Move camera to grid center
    /// </summary>
    private void MoveCameraToCenter() {
        float x = this.grid[0].transform.position.x + ((this.gridSize.x * this.cellWidth) / 2f) - .5f;
        float y = this.grid[0].transform.position.y + ((this.gridSize.y * this.cellWidth) / 2f) - .5f;

        this.camera.transform.position = new Vector3(x, y, this.camera.transform.position.z);
    }

    /// <summary>
    /// Set origin cell with raycast to object position
    /// Set other cell position relative to origin cell
    /// </summary>
    private void SetOriginCell() {
        RaycastHit2D hit = Physics2D.Raycast(this.currentItemLoaded.transform.position, Vector2.zero, 1, (1 << 25));

        ItemColliderCell cell = hit.collider?.GetComponentInParent<ItemColliderCell>();

        if(cell) {
            cell.SetOrigin();
            this.cellOrigin = cell;

            foreach(ItemColliderCell cellGrid in this.grid) {
                if(cellGrid.GetPosition() != cell.GetPosition()) {
                    cellGrid.SetRelativePositionFrom(cell.GetPosition());
                }
            }

            cell.SetPosition(Vector2Int.zero);
        }
    }

    /// <summary>
    /// Clear grid and destroy loaded item
    /// </summary>
    private void Clear() {
        foreach(ItemColliderCell cell in this.grid) {
            Destroy(cell.gameObject);
        }

        if(this.currentItemLoaded) {
            Destroy(this.currentItemLoaded);
        }
    }

    /// <summary>
    /// Get sprite pivot as import settings view
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns></returns>
    private Vector2 GetSpritePivot(Sprite sprite) {
        Bounds bounds = sprite.bounds;
        float pivotX = -bounds.center.x / bounds.extents.x / 2f + 0.5f;
        float pivotY = -bounds.center.y / bounds.extents.y / 2f + 0.5f;

        return new Vector2(pivotX, pivotY);
    }

    /// <summary>
    /// Used to clean prefab object of useless components for this editor tool
    /// </summary>
    /// <param name="obj"></param>
    private void RemoveAllUselessComponents(GameObject obj) {
        foreach(Component component in obj.GetComponents<Component>()) {
            if(!component.GetType().ToString().Contains("UnityEngine")) {
                Destroy(component);
            }
        }
    }

    /// <summary>
    /// Generate grid and set cell positions
    /// </summary>
    private void CreateGrid() {
        this.grid = new ItemColliderCell[this.gridSize.x * this.gridSize.y];
        int counter = 0;


        for(int x = 0; x < this.gridSize.x; x++) {
            for(int y = 0; y < this.gridSize.y; y++) {
                GameObject obj = Instantiate(this.gridCellPrefab, this.transform.position + new Vector3((x * this.cellWidth) - this.halfGridWidth, (y * this.cellWidth) - this.halfGridHeight), Quaternion.identity, this.transform);
                ItemColliderCell cell = obj.GetComponent<ItemColliderCell>();
                cell.SetPosition(new Vector2Int(x, y));
                this.grid[counter] = cell;
                counter++;
            }
        }
    }
}
