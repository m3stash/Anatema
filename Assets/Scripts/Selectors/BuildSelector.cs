using UnityEngine;
using UnityEngine.InputSystem;

public class BuildSelector : MonoBehaviour
{
    [Header("Fields to complete manually")] [SerializeField]
    private GameObject gridCellPrefab;

    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private GameObject selectorCellPrefab;
    [SerializeField] private GameObject target;
    [SerializeField] private bool showGrid;

    [Header("Don't touch it")] [SerializeField]
    private GameObject[] grid;

    [SerializeField] private float cellWidth;
    [SerializeField] private float halfGridWidth;
    [SerializeField] private float halfGridHeight;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject selector;

    [SerializeField] private SpriteRenderer previewItemRenderer;
    [SerializeField] private ItemConfig currentItemConfig;
    [SerializeField] private bool canPoseItem;

    private Ray ray;
    private int posX;
    private int posY;
    private Vector2 previousMoveDir;

    private int horipadX, horipadY;

    private void Awake()
    {
        this.selector = Instantiate(this.selectorCellPrefab, this.transform);
        this.selector.SetActive(false);

        this.CreateGrid();
    }

    private void OnEnable()
    {
        cam = Camera.main;

        InputManager.gameplayControls.TileSelector.ReleaseClick.performed += OnInteractButtonReleased;
        InputManager.gameplayControls.TileSelector.Navigate.performed += OnNavigate;
        InputManager.gameplayControls.TileSelector.HorizontalMove.performed += OnHorizontalMove;
        InputManager.gameplayControls.TileSelector.VerticalMove.performed += OnVerticalMove;
        ToolbarManager.OnSelectedItemChanged += OnCurrentSelectedItemChanged;

        if (Player.instance)
        {
            // Todo should be deleted when orchestration where refactored
            this.target = Player.instance.gameObject;
        }

        this.ManageGridDisplay();

        if (!InputManager.instance.IsMouseEnabled())
        {
            this.previousMoveDir = Vector2.zero;
            this.SetTileSelectorPosition(0, 0, true);
        }

        if (GameManager.instance.GetGameMode() == GameMode.BUILD)
        {
            this.OnCurrentSelectedItemChanged();
        }
    }

    private void OnDisable()
    {
        InputManager.gameplayControls.TileSelector.ReleaseClick.performed -= OnInteractButtonReleased;
        InputManager.gameplayControls.TileSelector.Navigate.performed -= OnNavigate;
        InputManager.gameplayControls.TileSelector.HorizontalMove.performed -= OnHorizontalMove;
        InputManager.gameplayControls.TileSelector.VerticalMove.performed -= OnVerticalMove;
        ToolbarManager.OnSelectedItemChanged -= OnCurrentSelectedItemChanged;

        this.DestroyPreviewItem();
    }

    public void SetShowGrid(bool state)
    {
        this.DestroyPreviewItem();

        this.showGrid = state;
        this.ManageGridDisplay();
    }

    private void OnInteractButtonReleased(InputAction.CallbackContext ctx)
    {
        if (canPoseItem)
        {
            this.AddItem(new Vector2Int(posX, posY));
        }
    }

    private void OnHorizontalMove(InputAction.CallbackContext ctx)
    {
        this.horipadX = (int) ctx.ReadValue<float>();

        Vector2 dir = new Vector2(this.horipadX, this.horipadY);

        if (dir != this.previousMoveDir)
        {
            this.DoNavigation(dir);
        }
    }

    private void OnVerticalMove(InputAction.CallbackContext ctx)
    {
        this.horipadY = (int) -ctx.ReadValue<float>();

        Vector2 dir = new Vector2(this.horipadX, this.horipadY);

        if (dir != this.previousMoveDir)
        {
            this.DoNavigation(dir);
        }
    }

    private void OnNavigate(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();

        if (dir != this.previousMoveDir)
        {
            this.DoNavigation(dir);
        }
    }

    private void DoNavigation(Vector2 dir)
    {
        this.previousMoveDir = dir;
        this.SetTileSelectorPosition((int) this.selector.transform.position.x + (int) dir.x, (int) this.selector.transform.position.y + (int) dir.y);
    }

    private void ManageGridDisplay()
    {
        foreach (GameObject cell in this.grid)
        {
            cell.SetActive(this.showGrid);
        }
    }

    private void MoveToTarget()
    {
        if (this.target && Vector3.Distance(this.target.transform.position, this.transform.position) > 0.5f)
        {
            this.transform.position = new Vector3((int) this.target.transform.position.x + 0.5f,
                (int) this.target.transform.position.y + 0.5f);

            if (this.previewItemRenderer)
            {
                this.CheckPreviewItemValidity((int) this.previewItemRenderer.transform.position.x,
                    (int) this.previewItemRenderer.transform.position.y);
            }
        }
    }

    private void OnCurrentSelectedItemChanged()
    {
        this.DestroyPreviewItem();
        this.CreatePreviewItem();
    }

    private void CreateGrid()
    {
        this.grid = new GameObject[this.gridSize.x * this.gridSize.y];
        int counter = 0;
        this.cellWidth = this.gridCellPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        this.halfGridWidth = ((this.gridSize.x / 2) * this.cellWidth);
        this.halfGridHeight = ((this.gridSize.y / 2) * this.cellWidth);

        for (int x = 0; x < this.gridSize.x; x++)
        {
            for (int y = 0; y < this.gridSize.y; y++)
            {
                GameObject gridCell = Instantiate(this.gridCellPrefab,
                    this.transform.position + new Vector3((x * this.cellWidth) - this.halfGridWidth,
                        (y * this.cellWidth) - this.halfGridHeight),
                    Quaternion.identity,
                    this.transform);

                this.grid[counter] = gridCell;
                counter++;
            }
        }
    }

    private void AddItem(Vector2Int pos)
    {
        InventoryItemData itemData = ToolbarManager.instance.UseSelectedItemData();

        if (itemData == null)
        {
            return;
        }

        if (itemData.GetConfig().GetItemType().Equals(ItemType.BLOCK))
        {
            WorldManager.instance.AddTile(pos.x, pos.y, itemData.GetConfig().GetId());
        }
        else
        {
            WorldManager.instance.AddItem(pos, itemData);
        }
    }

    private void CreatePreviewItem()
    {
        InventoryItemData itemData = ToolbarManager.instance.GetSelectedItemData();

        if (itemData == null)
        {
            return;
        }

        GameObject obj = Instantiate(itemData.GetConfig().GetPrefab(), this.selector.transform.position + new Vector3(-0.5f, -0.5f), Quaternion.identity);
        obj.transform.parent = this.selector.transform;

        foreach (Component component in obj.GetComponents<Component>())
        {
            if (component.GetType() != typeof(SpriteRenderer) && component.GetType() != typeof(Transform) &&
                component.GetType() != typeof(ItemRotation))
            {
                Destroy(component);
            }
        }

        this.currentItemConfig = itemData.GetConfig();
        this.previewItemRenderer = obj.GetComponent<SpriteRenderer>();
        this.CheckPreviewItemValidity((int) obj.transform.position.x, (int) obj.transform.position.y);
    }

    private void CheckPreviewItemValidity(int originX, int originY)
    {
        if (!this.previewItemRenderer)
        {
            this.canPoseItem = false;
            this.RefreshPreviewItemRenderer();
            return;
        }

        this.canPoseItem = MapFunctions.CheckCanPoseItem(this.currentItemConfig, originX, originY);
        this.RefreshPreviewItemRenderer();
    }

    private void RefreshPreviewItemRenderer()
    {
        if (this.previewItemRenderer)
        {
            this.previewItemRenderer.color = this.canPoseItem ? new Color(1, 1, 1, 0.5f) : new Color(1, 0, 0, 0.5f);
        }
    }

    private void DestroyPreviewItem()
    {
        if (this.previewItemRenderer)
        {
            Destroy(this.previewItemRenderer.gameObject);
        }
    }

    private void SetTileSelectorPosition(int x, int y, bool force = false)
    {
        if (!this.selector.activeSelf)
        {
            this.selector.SetActive(true);
        }

        if (force)
        {
            this.selector.SetActive(true);
            this.selector.transform.localPosition = new Vector2(x, y);
            this.CheckPreviewItemValidity(x, y);
            return;
        }

        if (this.selector.transform.position != new Vector3(x + 0.5f, y + 0.5f))
        {
            if (x <= (int) this.target.transform.position.x + 0.5f + this.halfGridWidth &&
                x >= (int) this.target.transform.position.x - 0.5f - this.halfGridWidth &&
                y >= (int) this.target.transform.position.y - 0.5f - this.halfGridHeight &&
                y <= (int) this.target.transform.position.y + 0.5f + this.halfGridHeight)
            {
                this.selector.transform.position = new Vector2(x + 0.5f, y + 0.5f);

                if (this.previewItemRenderer && this.previewItemRenderer.GetComponent<ItemRotation>())
                {
                    this.previewItemRenderer.GetComponent<ItemRotation>().RefreshUI();
                }

                this.CheckPreviewItemValidity(x, y);
            }
            else
            {
                this.canPoseItem = false;
                this.selector.SetActive(false);
            }
        }
    }


    private void Update()
    {
        this.MoveToTarget();

        // Manage selector tile for mouse
        if (InputManager.instance.IsMouseEnabled())
        {
            ray = cam.ScreenPointToRay(InputManager.mousePosition);

            posX = (int) ray.origin.x;
            posY = (int) ray.origin.y;
            this.SetTileSelectorPosition(posX, posY);
        }
        else
        {
            posX = (int) this.selector.transform.position.x;
            posY = (int) this.selector.transform.position.y;
        }
    }
}