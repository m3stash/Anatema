using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryCell : MonoBehaviour
{
    [SerializeField] private GameObject slotItemPrefab;
    [SerializeField] private InventoryItem inventoryItem;

    private Button button;

    public delegate void OnClick(InventoryCell cell);
    public static event OnClick NotifyClickEvent;

    private void Awake()
    {
        this.button = GetComponent<Button>();
    }

    private void Start()
    {
        this.button.onClick.AddListener(() => this.NotifyClick());
    }

    private void NotifyClick()
    {
        // Notify if this cell contains an item and event handler have atleast one subscriber
        if(this.inventoryItem && NotifyClickEvent != null)
        {
            NotifyClickEvent(this);
        }
    }

    public void Refresh()
    {
        this.inventoryItem = GetComponentInChildren<InventoryItem>();
    }

    public void UpdateItem(InventoryItemData item) {
        if(item != null && item.GetConfig() != null) {
            if(!inventoryItem) {
                GameObject obj = Instantiate(this.slotItemPrefab, this.transform);
                this.inventoryItem = obj.GetComponent<InventoryItem>();
            }

            inventoryItem.Setup(item);
        } else if(((item != null && item.GetConfig() == null) || item == null) && inventoryItem) {
            Destroy(this.inventoryItem.gameObject);
        }
    }

    public InventoryItem GetInventoryItem()
    {
        return this.inventoryItem;
    }

    private void OnDestroy()
    {
        this.button.onClick.RemoveAllListeners();
    }
}
