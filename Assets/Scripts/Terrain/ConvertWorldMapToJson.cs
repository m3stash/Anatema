using UnityEngine;

[System.Serializable]
public class ConvertWorldMapToJson {
    [SerializeField] public ValueByPositionModel[] world;
    [SerializeField] public ValueByPositionModel[] wall;

    public ConvertWorldMapToJson(int[,] world, int[,] wall) {
        this.world = new ValueByPositionModel[world.Length];
        this.wall = new ValueByPositionModel[wall.Length];
        var count = 0;
        for (var x = 0; x < world.GetUpperBound(0); x++) {
            for (var y = 0; y < world.GetUpperBound(1); y++) {
                this.world[count] = new ValueByPositionModel(x, y, world[x, y]);
                this.wall[count] = new ValueByPositionModel(x, y, wall[x, y]);
                count++;
            }
        }
    }
}
