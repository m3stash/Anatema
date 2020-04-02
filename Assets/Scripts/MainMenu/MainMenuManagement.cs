using UnityEngine;

public class MainMenuManagement : MonoBehaviour {

    [SerializeField] private GameObject saveSlotPannel;
    [SerializeField] private GameObject buttonPannel;
    public static MainMenuManagement instance;
    private void Awake() {
        instance = this;
    }

    void Start() {
    }

    public void GoToMainMenu() {
        buttonPannel.SetActive(true);
        saveSlotPannel.SetActive(false);
    }

    public void NewGame() {
        GameMaster.instance.NewGame();
    }

    public void Continue() {
        buttonPannel.SetActive(false);
        saveSlotPannel.SetActive(true);
        // toDO voir a appeler le gameMaster et charger la scene correspondant a la map sur laquelle nous sommes!
    }

}