using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockItem : Item
{
    private void OnEnable() {
        StartCoroutine(this.DestroyIt());
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    private IEnumerator DestroyIt() {
        yield return new WaitForSeconds(3);
        Debug.Log("Destroy it");
        this.TransformToPickableItem();
    }
}
