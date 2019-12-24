using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    [Header("Weapon configuration")]
    [SerializeField] private AudioClip slashSound; // This is just an example

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hi ! I'm a weapon");
    }

    public override void Use()
    {
        // Do some stuff in function of the configuration
        // Like equip weapon if in inventory state or play sound if it is active and equiped
    }

    public override void Destroy()
    {
        base.Destroy(); // Keep default behaviour to destroy 
    }
}
