using UnityEngine;
using UnityEngine.UI;
using System;
public class MainMenuManagement : MonoBehaviour {

    [SerializeField] private GameObject leftMenuUI;
    [SerializeField] private GameObject StartMenuUI;
    public static MainMenuManagement instance;
    private SaveData[] saves;

    private void Awake() {
        instance = this;
    }

    private void ActiveLeftMenu() {
        StartMenuUI.SetActive(false);
        InputMainMenuManager.instance.SetLayout(MainMenuLayout.LEFTMENU);
    }

    void Start() {
        ActiveLeftMenu();
    }

    public void MainMenu() {
        ActiveLeftMenu();
    }

    public void StartMenu() {
        InputMainMenuManager.instance.SetLayout(MainMenuLayout.STARTMENU);
        StartMenuUI.SetActive(true);
        if (saves == null) {
            saves = GameMaster.instance.GetSaves();
            for (var i = 0; i < saves.Length; i++) {
                StartMenuUI.transform.GetChild(i).GetComponent<StartMenuSlot>().SetValue(saves[i], i);
            }
        }
    }

    public void ShowDialogModal(bool showModal) {
        if (showModal) {
            InputMainMenuManager.instance.SetLayout(MainMenuLayout.DIALOGMODAL);
        } else {
            InputMainMenuManager.instance.SetLayout(InputMainMenuManager.instance.GetLastMenuLayout());
        }
    }

    public void Quit() {
        StartMenuUI.SetActive(false);
        // InputMainMenuManager.instance.SetLayout(MainMenuLayout.QUIT);
        Application.Quit();
    }

    public void Options() {
        StartMenuUI.SetActive(false);
        GameMaster.instance.DebugWorld();
        // InputMainMenuManager.instance.SetLayout(MainMenuLayout.OPTIONS);
    }

}