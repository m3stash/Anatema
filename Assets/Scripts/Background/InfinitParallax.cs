using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitParallax : MonoBehaviour {
    [SerializeField] GameObject[] repeatItems;
    private GameObject player;
    private int widthGo;
    private int playerOldposX;
    // Start is called before the first frame update

    private void OnEnable() {
        SetPlayer();
    }

    void Start() {
        this.widthGo = (int)this.repeatItems[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void SetPlayer() {
        player = GameManager.instance.GetPlayer();
        playerOldposX = (int)player.transform.position.x;
    }

    void Update() {
        if (!player)
            return;
        var playerX = (int)player.transform.position.x;
        if (playerX == playerOldposX)
            return;
        if (player.transform.localScale.x > 0) {
            foreach (GameObject item in repeatItems) {
                var middleGo = (int)(item.transform.position.x + (this.widthGo / 2));
                if (playerX > middleGo && Mathf.Abs(playerX - middleGo) > this.widthGo) {
                    item.transform.position = new Vector3(item.transform.position.x + (this.widthGo * 3), item.transform.position.y, item.transform.position.z);
                }
            }
        }
        if (player.transform.localScale.x < 0) {
            foreach (GameObject item in repeatItems) {
                var middleGo = (int)(item.transform.position.x - (this.widthGo / 2));
                if (playerX < middleGo && Mathf.Abs(playerX - middleGo) > this.widthGo) {
                    item.transform.position = new Vector3(item.transform.position.x - (this.widthGo * 3), item.transform.position.y, item.transform.position.z);
                }
            }
        }
    }

}
