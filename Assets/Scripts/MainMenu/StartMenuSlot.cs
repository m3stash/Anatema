﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StartMenuSlot : MonoBehaviour {
    [SerializeField] private GameObject top;
    [SerializeField] private GameObject center;
    [SerializeField] private GameObject bottom;
    [SerializeField] private GameObject slotButton;
    [SerializeField] private GameObject deleteButton;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite corrupteDataSprite;
    [SerializeField] Sprite noDataSprite;
    private bool isNewGame = false;
    private int slotNumber;
    private String[] cultureNames = { "en-US", "en-GB", "fr-FR" };  // toDO dynamiser ça plus tard avec le choix de la langue dans le menu option !
    private Button slot;
    private Button delete;
    private CultureInfo culture;
    public delegate void SlotSelect(StartMenuSlot slot, Button button);
    public static SlotSelect OnSelect;
    public delegate void SlotDelete(StartMenuSlot slot);
    public static SlotSelect OnDelete;

    private void Awake() {
        var culture = new CultureInfo("fr-FR");
        slot = slotButton.gameObject.GetComponent<Button>();
        delete = deleteButton.gameObject.GetComponent<Button>();
        SetButtonColors();
        SetDeleteButtonColors();
    }

    private void OnDestroy() {
        DisableListeners();
    }

    private void OnEnable() {
        EnableListeners();
    }

    private void OnDisable() {
        DisableListeners();
    }

    public Button GetSlotButton() {
        return slot;
    }

    private void SetButtonColors() {
        ColorBlock colorBlock = slot.colors;
        colorBlock.normalColor = new Color(0.2641509f, 0.2641509f, 0.2641509f, 0.9f);
        colorBlock.selectedColor = new Color(0.03921569f, 0.03921569f, 0.03921569f, 0.8f);
        colorBlock.highlightedColor = new Color(0.03921569f, 0.03921569f, 0.03921569f, 0.8f);
        slot.colors = colorBlock;
    }

    private void SetDeleteButtonColors() {
        delete.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        ColorBlock colorBlock = delete.colors;
        colorBlock.normalColor = new Color(0.1490196f, 0.1372549f, 0.1372549f, 0.8f);
        colorBlock.selectedColor = new Color(0.1490196f, 0.1372549f, 0.1372549f, 1);
        colorBlock.highlightedColor = new Color(0.1490196f, 0.1372549f, 0.1372549f, 1);
        colorBlock.disabledColor = new Color(0.1490196f, 0.1372549f, 0.1372549f, 0.5f);
        delete.colors = colorBlock;
    }

    private void EnableListeners() {
        
        slot.onClick.AddListener(() => {
            OnSelect?.Invoke(this, slot);
        });

        delete.onClick.AddListener(() => {
            OnDelete?.Invoke(this, delete);
        });

    }

    private void DisableListeners() {
        slot.onClick.RemoveAllListeners();
        delete.onClick.RemoveAllListeners();
    }

    public int GetSlotNumber() {
        return slotNumber;
    }

    public GameObject GetTop() {
        return top;
    }
    public bool IsNewGame() {
        return isNewGame;
    }

    public GameObject GetBottom() {
        return bottom;
    }

    public GameObject GetCenter() {
        return center;
    }

    public void SetValue(SaveData saveData, int slotNumber) {
        this.slotNumber = slotNumber;
        InitValue();
        //var mm = (Math.Floor(playTime / 60) % 60).ToString();
        //var hh = Math.Floor(playTime / 60 / 60).ToString();
        //fileExist.transform.Find("gameTime").GetComponent<UnityEngine.UI.Text>().text = hh + " hour " + mm + " min";
        // top.GetComponent<Text>().text = 
        if (saveData != null) {
            if (GenerateMapService.instance.VerifyAllFileExists(slotNumber, saveData.currentWorld)) {
                isNewGame = false;
                center.GetComponentInChildren<Image>().sprite = defaultSprite;
                top.GetComponentInChildren<Text>().text = "Partie (" + slotNumber + ")";
                bottom.GetComponentInChildren<Text>().text = saveData.dateLastSave.ToString(culture);
                delete.interactable = true;
            } else {
                InitValue();
                top.GetComponentInChildren<Text>().text = "Corrupte Data";
                center.GetComponentInChildren<Image>().sprite = corrupteDataSprite;
                delete.interactable = true;
            }
        }
    }


    public void InitValue() {
        isNewGame = true;
        center.GetComponentInChildren<Image>().sprite = noDataSprite;
        top.GetComponentInChildren<Text>().text = "Nouvelle Partie";
        bottom.GetComponentInChildren<Text>().text = "";
        delete.interactable = false;
    }

}
