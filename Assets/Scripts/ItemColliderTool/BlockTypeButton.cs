using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlockTypeButton : MonoBehaviour {
    [Header("Fields completed in runtime")]
    [SerializeField] private Image checkImage;

    [Header("Don't touch it")]
    private Button button;
    private BlockType blockType;
    private bool selected;

    public void Setup(BlockType blockType) {
        this.button = GetComponent<Button>();
        this.blockType = blockType;
        this.selected = true; // Selected by default
        this.button.GetComponentInChildren<TextMeshProUGUI>().text = this.blockType.ToString();

        // Add a listener on click event to notify grid editor
        this.button.onClick.AddListener(() => {
            this.selected = !this.selected;
            this.checkImage.enabled = this.selected;
        });
    }

    public bool IsSelected() {
        return this.selected;
    }

    public BlockType GetBlockType() {
        return this.blockType;
    }

    private void OnDestroy() {
        this.button.onClick.RemoveAllListeners();
    }
}
