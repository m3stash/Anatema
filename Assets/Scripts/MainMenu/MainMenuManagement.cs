using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManagement: MonoBehaviour {

    public Canvas canvas;

    public void NewGame() {
        SceneManager.LoadScene("Demo", LoadSceneMode.Single);
    }

    public void Continue() {
        
    }

    private void Start() {
        // canvas = GameObject.Find("Main_UI").GetComponent<Canvas>();
    }

}