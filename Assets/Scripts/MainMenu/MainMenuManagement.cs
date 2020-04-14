using UnityEngine;
using UnityEngine.UI;
public class MainMenuManagement : MonoBehaviour {

    [SerializeField] private GameObject leftMenuUI;
    [SerializeField] private GameObject StartMenuUI;
    [SerializeField] private GameObject background;
    public static MainMenuManagement instance;
    public Image backgroundSprite;
    private SaveData[] saves;
    private void Awake() {
        instance = this;
    }

    void Start() {
        StartMenuUI.SetActive(false);
        leftMenuUI.SetActive(true);
        InputMainMenuManager.instance.SetLayout(MainMenuLayout.LEFTMENU);
        backgroundSprite = background.GetComponent<Image>();
    }

    public void GoToMainMenu() {
        SetBackgroundColor(false);
        leftMenuUI.SetActive(true);
        StartMenuUI.SetActive(false);
        InputMainMenuManager.instance.SetLayout(MainMenuLayout.LEFTMENU);
    }

    public void StartMenu() {
        SetBackgroundColor(true);
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
        Application.Quit();
    }

    public void Options() {

    }

    private void SetBackgroundColor(bool revert) {
        if (revert) {
            backgroundSprite.color = new Color(255, 255, 255, 0.5f);
        } else {
            backgroundSprite.color = new Color(255, 255, 255, 1f);
        }
    }
}