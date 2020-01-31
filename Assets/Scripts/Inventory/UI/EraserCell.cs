using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EraserCell : InventoryCell, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;

    protected override void Awake() {
        base.Awake();
        this.animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Used to manage bin animation
    /// </summary>
    /// <param name="state"></param>
    public void SetOpenedState(bool state) {
        this.animator.SetBool("opened", state);

        // Make dragged item transparent if bin is opened
        if (InventoryItem.draggedObject) {
            InventoryItem.draggedObject.GetComponent<Image>().color = new Color(255, 255, 255, state ? 0 : 0.7f);
        }
    }

    /// <summary>
    /// Overriden to remove base process of inventory cell with sprite replacement
    /// </summary>
    protected override void RefreshUI() {
        // Do nothing
    }

    /// <summary>
    /// Callback when drag end
    /// </summary>
    /// <param name="item"></param>
    protected override void OnAnyItemDragEnd(InventoryItem item) {
        base.OnAnyItemDragEnd(item);

        this.SetOpenedState(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (InventoryItem.draggedObject) {
            this.SetOpenedState(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (InventoryItem.draggedObject) {
            this.SetOpenedState(false);
        }
    }
}
