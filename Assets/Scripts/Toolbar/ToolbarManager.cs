using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarManager : MonoBehaviour {

    [Header("Toolbars configs")]
    [SerializeField] private ToolbarConfig[] toolbarConfigs;
    [SerializeField] private ToolbarConfig currentToolbar;

    private Dictionary<ToolbarType, InventoryItemData[]> toolbars;

    public static ToolbarManager instance;

    public delegate void ToolbarChanged(ToolbarType config);
    public static event ToolbarChanged OnToolbarChanged;

    void Awake() {
        if(instance) {
            Destroy(this);
        } else {
            instance = this;

            this.toolbars = new Dictionary<ToolbarType, InventoryItemData[]>();

            // Init all toolbars
            foreach(ToolbarConfig config in this.toolbarConfigs) {
                this.toolbars.Add(config.GetToolbarType(), new InventoryItemData[config.GetSize()]);
            }
        }
    }

    private void OnEnable() {
        InventoryToolbarUI.OnItemDrop += DropItem;
        InventoryToolbarUI.OnItemDelete += DeleteItem;
        InventoryToolbarUI.OnItemReplace += ReplaceItem;
    }

    private void OnDestroy() {
        InventoryToolbarUI.OnItemDrop -= DropItem;
        InventoryToolbarUI.OnItemDelete -= DeleteItem;
        InventoryToolbarUI.OnItemReplace -= ReplaceItem;
    }

    public InventoryItemData[] GetToolbarItems(ToolbarType toolbarType) {
        return this.toolbars[toolbarType];
    }

    public ToolbarType GetCurrentToolbarType() {
        return this.currentToolbar.GetToolbarType();
    }

    public ToolbarConfig GetToolbarConfig(ItemType itemType) {
        foreach(ToolbarConfig config in this.toolbarConfigs) {
            foreach(ItemType type in config.GetAllowedItemTypes()) {
                if (type == itemType) {
                    return config;
                }
            }
        }

        Debug.LogErrorFormat("No toolbar config found with allowed item : {0}", itemType.ToString());

        return null;
    }

    /// <summary>
    /// Used to replace an item in toolbar
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public void ReplaceItem(InventoryItemData item, int targetIdx, ToolbarType toolbarType) {
        // Get reference to associated database of item type
        InventoryItemData[] itemDatabase = this.toolbars[toolbarType];

        itemDatabase[targetIdx] = item;

        // Notify item database changed to refresh UI
        OnToolbarChanged?.Invoke(toolbarType);
    }

    /// <summary>
    /// Used to delete item at specific index
    /// </summary>
    /// <param name="itemIdx"></param>
    /// <param name="itemType"></param>
    private void DeleteItem(int itemIdx, ToolbarType toolbarType) {
        // Get reference to associated database of item type
        InventoryItemData[] itemDatabase = this.toolbars[toolbarType];

        // Remove item from database
        itemDatabase[itemIdx] = null;

        OnToolbarChanged?.Invoke(toolbarType);
    }

    /// <summary>
    /// Used to drop an item of inventory in the world
    /// </summary>
    /// <param name="itemIdx"></param>
    private void DropItem(int itemIdx, ToolbarType toolbarType) {
        // Get reference to associated database of item type
        InventoryItemData[] itemDatabase = this.toolbars[toolbarType];
        InventoryItemData itemData = itemDatabase[itemIdx];

        // Create item in world
        Vector3 positionToSpawn = Player.instance.transform.position + new Vector3(Player.instance.transform.localScale.x * 1.3f, 0);
        Item item = ItemManager.instance.CreateItem(itemData.GetConfig().GetId(), ItemStatus.PICKABLE, positionToSpawn);
        item.SetStacks(itemData.GetStacks());

        // Delete from inventory and refresh ui
        itemDatabase[itemIdx] = null;

        OnToolbarChanged?.Invoke(toolbarType);
    }
}
