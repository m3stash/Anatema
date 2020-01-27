using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour {

    float ViewRange = 15;
    [SerializeField] public LayerMask layer;

    public void Setup() {
        StartCoroutine(See());
    }

    private IEnumerator See() {
        while (true) {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, ViewRange);
            int i = 0;
            while (i < hits.Length) {
                if (hits[i].transform.CompareTag("Player")) {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, (hits[i].transform.position - transform.position), 10, layer);
                    Debug.DrawRay(transform.position, (hits[i].transform.position - transform.position), Color.red);
                    if (hit && hit.transform.CompareTag("Player")) {
                        Debug.Log("SEE PLAYER");
                    }
                }
                i++;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

}
