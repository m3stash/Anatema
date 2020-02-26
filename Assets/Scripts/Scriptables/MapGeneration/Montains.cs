using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {
    public float seed;
    public float frequency;
    public float amplitude;
}

[CreateAssetMenu(fileName = "ProceduralGen", menuName = "Map/mountain")]
public class Montains : ScriptableObject {
    public Wave[] waves;
}
