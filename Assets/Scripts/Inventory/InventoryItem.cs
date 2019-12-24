using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    private string nameDisplay;
    [SerializeField]
    private int id;
    public ItemConfig config;
    public int maxStacks;
    public int currentStack;
    public string type;
    public int width;
    public int height;
    public string furnitureType;
    public Text text;

    private void Awake() {
        text = gameObject.GetComponentInChildren<Text>();
    }
}

