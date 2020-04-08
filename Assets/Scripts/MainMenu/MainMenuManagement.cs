using UnityEngine;

public class MainMenuManagement : MonoBehaviour {

    [SerializeField] private GameObject leftMenuUI;
    [SerializeField] private GameObject StartMenuUI;
    public static MainMenuManagement instance;
    private SaveData[] saves;
    private void Awake() {
        instance = this;
    }

    void Start() {
        StartMenuUI.SetActive(false);
        leftMenuUI.SetActive(true);
        InputMainMenuManager.instance.SetLayout(MainMenuLayout.LEFTMENU);
    }

    public void GoToMainMenu() {
        leftMenuUI.SetActive(true);
        StartMenuUI.SetActive(false);
        InputMainMenuManager.instance.SetLayout(MainMenuLayout.LEFTMENU);
    }

    public void StartMenu() {
        InputMainMenuManager.instance.SetLayout(MainMenuLayout.STARTMENU);
        leftMenuUI.SetActive(false);
        StartMenuUI.SetActive(true);
        if (saves == null) {
            saves = GameMaster.instance.GetSaves();
            for (var i = 0; i < saves.Length; i++) {
                StartMenuUI.transform.GetChild(i).GetComponent<StartMenuSlot>().SetValue(saves[i], i);
            }
        }
    }

    public void Quit() {

    }

    public void Options() {

    }
}