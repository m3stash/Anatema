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
    public Item_cfg config;
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

    void Start() {
        gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0);
        gameObject.GetComponent<Image>().sprite = config.sprite;
        nameDisplay = config.name;
        id = config.id;
        width = config.width;
        height = config.height;
        type = config.type.ToString();
        furnitureType = config.furnitureType.ToString();
        maxStacks = (int)Enum.Parse(typeof(Stacks), config.stacks.ToString());
    }
}

