using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public static GameMaster Instance;
    public static GameObject itemCell;

    public static int GetToolbarInventory() {
        return 10;
    }

    void Awake() {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public static void Inventory() {

    }

}
