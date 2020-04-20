using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonService {

    public static bool ArrayContains<T>(T[] unallowedBlockTypes, T typeToCompare) {
        for (int i = 0; i < unallowedBlockTypes.Length; i++) {
            if (unallowedBlockTypes[i].Equals(typeToCompare)) {
                return false;
            }
        }

        return true;
    }

    public static bool IsOutOfBound(int x, int y, int width, int height) {
        return (x < 0 || x > (width)) || (y < 0 || y > (height));
    }

    public static Selectable GetNeighboorSelectable(Vector2 direction, Button button) {
        if (!button)
            return null;
        Selectable neighbour = null;
        if (direction == new Vector2(1, 0)) {
            neighbour = button.FindSelectableOnRight();
        } else if (direction == new Vector2(-1, 0)) {
            neighbour = button.FindSelectableOnLeft();
        } else if (direction == new Vector2(0, 1)) {
            neighbour = button.FindSelectableOnUp();
        } else if (direction == new Vector2(0, -1)) {
            neighbour = button.FindSelectableOnDown();
        }
        return neighbour;
    }

    public static Selectable GetNeighboorSelectableEnable(Vector2 direction, Button button) {
        if (!button)
            return null;
        Selectable neighbour = null;
        if (direction == new Vector2(1, 0)) {
            neighbour = FindSelectableInteractable(button, DirectionEnum.RIGHT);
        } else if (direction == new Vector2(-1, 0)) {
            neighbour = FindSelectableInteractable(button, DirectionEnum.LEFT);
        } else if (direction == new Vector2(0, 1)) {
            neighbour = FindSelectableInteractable(button, DirectionEnum.UP);
        } else if (direction == new Vector2(0, -1)) {
            neighbour = FindSelectableInteractable(button, DirectionEnum.DOWN);
        }
        return neighbour;
    }

    private static Selectable FindSelectableInteractable(Button button, DirectionEnum dir) {
        Selectable neighbour = null;
        switch (dir) {
            case DirectionEnum.LEFT:
                neighbour = button.FindSelectableOnLeft();
                if (neighbour && !neighbour.interactable) {
                    return FindSelectableInteractable(neighbour.GetComponent<Button>(), DirectionEnum.LEFT);
                }
                break;
            case DirectionEnum.RIGHT:
                neighbour = button.FindSelectableOnRight();
                if (neighbour && !neighbour.interactable) {
                    return FindSelectableInteractable(neighbour.GetComponent<Button>(), DirectionEnum.RIGHT);
                }
                break;
            case DirectionEnum.UP:
                neighbour = button.FindSelectableOnUp();
                if (neighbour && !neighbour.interactable) {
                    return FindSelectableInteractable(neighbour.GetComponent<Button>(), DirectionEnum.UP);
                }
                break;
            case DirectionEnum.DOWN:
                neighbour = button.FindSelectableOnDown();
                if (neighbour && !neighbour.interactable) {
                    return FindSelectableInteractable(neighbour.GetComponent<Button>(), DirectionEnum.DOWN);
                }
                break;
        }
        return neighbour;
    }

}

public enum DirectionEnum {
    LEFT,
    RIGHT,
    UP,
    DOWN
}
