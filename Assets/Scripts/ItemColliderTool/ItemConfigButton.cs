using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemConfigButton : MonoBehaviour
{
    [Header("Fields completed in runtime")]
    [SerializeField] private Button button;
    [SerializeField] private ItemConfig itemConfig;

    public delegate void Clicked(ItemConfig itemConfig);
    public static Clicked OnSelect;

    public void Setup(ItemConfig config) {
        this.button = GetComponent<Button>();
        this.itemConfig = config;
        this.button.GetComponentInChildren<TextMeshProUGUI>().text = this.itemConfig.GetId() + "_" + this.itemConfig.GetDisplayName();

        // Add a listener on click event to notify grid editor
        this.button.onClick.AddListener(() => OnSelect?.Invoke(this.itemConfig));
    }

    private void OnDestroy() {
        this.button.onClick.RemoveAllListeners();
    }
}
