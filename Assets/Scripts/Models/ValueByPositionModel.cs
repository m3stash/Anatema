using UnityEngine;

[System.Serializable]
public class ValueByPositionModel {
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] int value;

    public ValueByPositionModel(int x, int y, int value) {
        this.x = x;
        this.y = y;
        this.value = value;
    }
}