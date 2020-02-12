using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shortcut : MonoBehaviour
{
    [Header("Fields to complete")]
    [SerializeField] private GameMode gameMode;
    [SerializeField] private float disableIntensity = 0.5f;

    private Image image;

    private void Awake() {
        this.image = GetComponent<Image>();
    }

    private void OnEnable() {
        GameManager.OnGameModeChanged += GameModeChanged;

        this.GameModeChanged(GameManager.instance.GetGameMode());
    }

    private void OnDisable() {
        GameManager.OnGameModeChanged -= GameModeChanged;
    }

    private void GameModeChanged(GameMode gameMode) {
        this.image.color = new Color(1,1,1, this.gameMode == gameMode ? 1 : disableIntensity);
    }
}
