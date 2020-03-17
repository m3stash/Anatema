using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManagement: MonoBehaviour {

    public Canvas canvas;

    public void NewGame() {
        GameMaster.instance.CreateNewWorlds();
        SceneManager.LoadScene("Demo", LoadSceneMode.Single);
    }

    public void Continue() {
        // toDO voir a appeler le gameMaster et charger la scene correspondant a la map sur laquelle nous sommes!
    }

    private void Start() {
        // canvas = GameObject.Find("Main_UI").GetComponent<Canvas>();
    }

}