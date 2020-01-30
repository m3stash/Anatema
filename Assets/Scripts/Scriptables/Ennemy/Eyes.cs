using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour {

    float ViewRange = 15;
    [SerializeField] public LayerMask layer;
    private EnnemyBrain brain;

    public void Setup(EnnemyBrain brain) {
        this.brain = brain;
        StartCoroutine(See());
    }

    private IEnumerator See() {
        while (true) {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, ViewRange);
            int i = 0;
            var oldSeePlayerVal = this.brain.GetSeePlayer();
            Vector2 playerPos = Vector2.zero;
            bool seePlayer = false;
            while (i < hits.Length) {
                if (hits[i].transform.CompareTag("Player")) {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, (hits[i].transform.position - transform.position), 10, layer);
                    Debug.DrawRay(transform.position, (hits[i].transform.position - transform.position), Color.red);
                    if (hit && hit.transform.CompareTag("Player")) {
                        seePlayer = true;
                        playerPos = new Vector2(hit.transform.position.x, hit.transform.position.x);
                    }
                }
                i++;
            }
            if (seePlayer != oldSeePlayerVal) { 
                this.brain.SetSeePlayer(seePlayer);
                this.brain.SetPlayerPosition(playerPos);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

}
