using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryCell : MonoBehaviour
{
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

    public InventoryItem GetInventoryItem()
    {
        return this.inventoryItem;
    }

    private void OnDestroy()
    {
        this.button.onClick.RemoveAllListeners();
    }
}
