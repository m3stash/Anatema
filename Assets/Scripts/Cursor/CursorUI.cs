using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorUI : MonoBehaviour
{
    private Image image;
    private RectTransform rectTransform;

    private void Awake() {
        this.image = GetComponent<Image>();
        this.rectTransform = GetComponent<RectTransform>();
    }

    public void Display(bool value) {
        this.image.enabled = value;
    }

    public void SetPosition(Vector2 pos) {
        this.rectTransform.position = pos;
    }

    public void SetSprite(Sprite sprite) {
        if (sprite) {
            this.image.color = Color.white;
            this.image.sprite = sprite;
        } else {
            this.image.color = new Color(1, 1, 1, 0f);
        }
    }
}
