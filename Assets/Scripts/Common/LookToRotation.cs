using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;

    void Update()
    {
        this.transform.rotation = Quaternion.Euler(this.rotation);
    }
}
