using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSelector : MonoBehaviour
{
    [Header("Fields to complete manually")]
    [SerializeField] private Sprite[] orderedStateSprites;
    [SerializeField] private SpriteRenderer stateRenderer;

    [Header("Don't touch it")]
    [SerializeField] private float maxDurability;
    [SerializeField] private float currentDurability;
    [SerializeField] private float statePartitionSize;
    [SerializeField] private new SpriteRenderer renderer;

    private void Awake() {
        this.renderer = GetComponent<SpriteRenderer>();
    }

    private void OnDisable() {
        this.stateRenderer.enabled = false;
    }

    // Start is called before the first frame update
    public void Setup(float maxDurability, float currentDurability)
    {
        this.maxDurability = maxDurability;
        this.currentDurability = currentDurability > maxDurability ? maxDurability : currentDurability;
        this.statePartitionSize = this.maxDurability / (float)this.orderedStateSprites.Length;

        int rendererIdx = Mathf.FloorToInt(this.currentDurability / this.statePartitionSize);
        this.stateRenderer.sprite = this.orderedStateSprites[rendererIdx > this.orderedStateSprites.Length - 1 ? this.orderedStateSprites.Length - 1 : rendererIdx];
        this.stateRenderer.enabled = true;
    }

    public void SetErrorState() {
        this.renderer.color = Color.red;
        this.ResetSetup();
    }

    public void SetValidState() {
        this.renderer.color = Color.white;
    }

    public void ResetSetup() {
        this.maxDurability = 0f;
        this.currentDurability = 0f;
        this.stateRenderer.enabled = false;
        this.stateRenderer.sprite = this.orderedStateSprites[0];
    }
}
