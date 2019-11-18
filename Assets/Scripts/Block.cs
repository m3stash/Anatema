using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Block : MonoBehaviour {

    private Player player;
    private Block block;
    [SerializeField]
    private string nameDisplay;
    [SerializeField]
    private int id;
    public int maxStacks;
    public Item_cfg config;

    void Start() {
        gameObject.GetComponent<SpriteRenderer>().sprite = config.sprite;
        nameDisplay = config.name;
        id = config.id;
        maxStacks = (int)Enum.Parse(typeof(Stacks), config.stacks.ToString());
    }

}
