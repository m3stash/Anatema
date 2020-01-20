using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StepperUI : MonoBehaviour, IPointerClickHandler {

    [SerializeField] private bool selected;

    private Image image;

    public delegate void StepperSelected(StepperUI stepper);
    public static StepperSelected OnSelect;

    private void Awake() {
        this.image = GetComponent<Image>();
    }

    private void OnEnable() {
        InventoryStepperUI.OnSelect += CheckSelectedState;

    }

    private void OnDisable() {
        InventoryStepperUI.OnSelect -= CheckSelectedState;
    }

    public void OnPointerClick(PointerEventData eventData) {
        this.Select();
    }

    public virtual void Select() {
        OnSelect?.Invoke(this);
    }

    protected virtual void RefreshUI() {
        if(this.selected) {
            this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1);
        } else {
            this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 0.5f);
        }
    }

    protected void CheckSelectedState(StepperUI stepper) {
        this.selected = (this == stepper);
        this.RefreshUI();
    }
}
