using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System;

public class StartMenuSlot : MonoBehaviour {
    [SerializeField] private GameObject top;
    [SerializeField] private GameObject center;
    [SerializeField] private GameObject bottom;
    [SerializeField] private GameObject slotButton;
    [SerializeField] private GameObject deleteButton;
    private int slotNumber;
    private String[] cultureNames = { "en-US", "en-GB", "fr-FR" };  // toDO dynamiser ça plus tard avec le choix de la langue dans le menu option !
    private Button slot;
    private Button delete;
    private CultureInfo culture;
    public delegate void SlotSelected(StartMenuSlot slot);
    public static SlotSelected OnSelect;
    public delegate void SlotDelete(StartMenuSlot slot);
    public static SlotSelected OnDelete;

    private void Awake() {
        var culture = new CultureInfo("fr-FR");
        slot = slotButton.gameObject.GetComponent<Button>();
        slot.onClick.AddListener(() => {
            OnSelect?.Invoke(this);
        });
        delete = deleteButton.gameObject.GetComponent<Button>();
        delete.onClick.AddListener(() => {
            OnDelete?.Invoke(this);
        });
    }

    private void Start() {

    }

    public int GetSlotNumber() {
        return slotNumber;
    }

    public GameObject GetTop() {
        return top;
    }

    public GameObject GetBottom() {
        return bottom;
    }

    public GameObject GetCenter() {
        return center;
    }

    public void SetValue(SaveData saveData, int slotNumber) {
        this.slotNumber = slotNumber;
        top.GetComponentInChildren<Text>().text = "Save (" + slotNumber + ")";
        //var mm = (Math.Floor(playTime / 60) % 60).ToString();
        //var hh = Math.Floor(playTime / 60 / 60).ToString();
        //fileExist.transform.Find("gameTime").GetComponent<UnityEngine.UI.Text>().text = hh + " hour " + mm + " min";
        // top.GetComponent<Text>().text = 
        if (saveData != null) {
            bottom.GetComponentInChildren<Text>().text = saveData.dateLastSave.ToString(culture);
        } else {
           Disable();
        }

    }

    public void Disable() {
        center.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        delete.interactable = false;
        slot.interactable = false;
    }
}
