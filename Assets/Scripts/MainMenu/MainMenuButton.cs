using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler {

    private Image image;
    [SerializeField] private MainMenuLayout layoutType;

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        SetColorHover(true);
    }

    public MainMenuLayout GetLayoutType() {
        return layoutType;
    }

    private void SetColorHover(bool hover) {
        image.color = new Color(200, 40, 40, 0.7f);
    }

}
