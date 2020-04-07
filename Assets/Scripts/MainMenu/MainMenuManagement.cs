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
        GameMaster.instance.Continue(0); // toDo voir à récupérer ça selon le slot de save choisi !!
    }

}