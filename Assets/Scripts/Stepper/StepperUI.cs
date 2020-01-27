using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StepperUI : MonoBehaviour, IPointerClickHandler {

    [Header("Only for debug")]
    [SerializeField] private bool selected;

    private Image image;

    public delegate void StepperSelected(StepperUI stepper);
    public static StepperSelected OnSelect;

    public delegate void StepperUnSelected(StepperUI stepper);
    public static StepperUnSelected OnUnSelect;

    private void Awake() {
        this.image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        this.Select();
    }

    public virtual void Select() {
        this.selected = true;
        this.RefreshUI();
        OnSelect?.Invoke(this);
    }

    public virtual void UnSelect() {
        this.selected = false;
        this.RefreshUI();
        OnUnSelect?.Invoke(this);
    }

    protected virtual void RefreshUI() {
        if(!this.image) {
            this.image = GetComponent<Image>();
        }

        this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, this.selected ? 1 : 0.5f);
    }
}
