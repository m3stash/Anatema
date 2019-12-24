using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    [Header("Consumable configuration")]
    [SerializeField] private GameObject destroyParticle; // This is just an example

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hi ! I'm a consumable");
    }

    public override void Use()
    {
        // Do some stuff in function of the configuration
        // Like decrease stacks and play animation then call this.Destroy()
    }

    public override void Destroy()
    {
        base.Destroy(); // Keep default behaviour to destroy it

        // But here I Can do some stuff like instantiate a particle
    }
}
