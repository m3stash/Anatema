using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour {

    public static Loader instance;
    [SerializeField] public Image fill;
    [SerializeField] public GameObject titleGo;
    [SerializeField] public GameObject currentActionGo;
    private Text currentActionText;
    private Text titleText;
    private float progress;
    private void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        currentActionText = currentActionGo.GetComponent<Text>();
        titleText = titleGo.GetComponent<Text>();
    }

    private void Start() {
        progress = 0;
        fill.fillAmount = 0;
    }

    public void SetCurrentAction(string title, string currentAction) {
        titleText.text = title;
        currentActionText.text = currentAction;
    }

    void Update() {
        fill.fillAmount = Mathf.Lerp(fill.fillAmount, progress * 0.01f, Time.deltaTime * 5f);
    }

    public void SetLoaderValue(float percent) {
        progress = percent;
    }

}